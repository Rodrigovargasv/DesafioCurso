﻿using DesafioCurso.Application.Commands.Request;
using DesafioCurso.Application.Commands.Response;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using FluentValidation;
using Mapster;
using MediatR;
using System;
using Unit = DesafioCurso.Domain.Entities.Unit;

namespace DesafioCurso.Application.Handlers
{
    public class UpdateUnitHandler : IRequestHandler<UpdateUnitRequest, UpdateUnitResponse>
    {

        private readonly IUnitRepository _context;
        private readonly IUnitOfWork _uow;
        private readonly UnitValidation _unitValidation;

        public UpdateUnitHandler(IUnitRepository context, IUnitOfWork uow, UnitValidation unitValidation)
        {
            _context = context;
            _uow = uow;
            _unitValidation = unitValidation;
        }

        public async Task<UpdateUnitResponse> Handle(UpdateUnitRequest request, CancellationToken cancellationToken)
        {
            var unit = request.Adapt<Unit>();

            var unitId = await _context.GetById(unit.Id);

            // Verifica se a unidade existe
            if (unitId is null)
                throw new NotFoundException("Unidade não encontrada");

            // Atualiza as propriedades da unidade com os dados da requisição
            unitId.Decription = unit.Decription;

            var unitValidation = await _unitValidation.ValidateAsync(unitId);

            // Se a unidade não for válida, lança uma exceção de validação
            if (!unitValidation.IsValid)
                if (!unitValidation.IsValid)
                throw new ValidationException(unitValidation.Errors);

            // Atualiza a entidade no contexto
            _context.Update(unit);

            // Commit das alterações no banco de dados
            await _uow.Commit();
      

            return unit.Adapt<UpdateUnitResponse>();

        }
    }
}
