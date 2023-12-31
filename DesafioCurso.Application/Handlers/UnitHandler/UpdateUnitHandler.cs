﻿using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class UpdateUnitHandler : IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>
    {
        private readonly IUnitRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly UnitValidation _unitValidation;

        public UpdateUnitHandler(IUnitRepository context, IUnitOfWork<ApplicationDbContext> uow, UnitValidation unitValidation)
        {
            _context = context;
            _uow = uow;
            _unitValidation = unitValidation;
        }

        public async Task<UpdateUnitResponse> Handle(UpdateUnitRequest request, CancellationToken cancellationToken)
        {
            var unitId = await _context.GetById(request.Id);

            // Verifica se a pessoa existe
            if (unitId is null)
                throw new NotFoundException("Unidade não encontrada");

            // Atualiza as propriedades da pessoa com os dados da requisição
            unitId.Decription = request.Decription;

            var unitValidation = await _unitValidation.ValidateAsync(unitId);

            // Se a pessoa não for válida, lança uma exceção de validação
            if (!unitValidation.IsValid)
                throw new ValidationException(unitValidation.Errors);

            // Atualiza a pessoa no contexto
            _context.Update(unitId);

            // Commit das alterações no banco de dados
            await _uow.Commit();

            return unitId.Adapt<UpdateUnitResponse>();
        }
    }
}