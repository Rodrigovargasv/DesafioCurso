﻿using DesafioCurso.Application.Commands.Request.Unit;
using DesafioCurso.Application.Commands.Response.Unit;
using DesafioCurso.Application.Interfaces;
using DesafioCurso.Domain.Common.Exceptions;
using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Domain.Validations;
using DesafioCurso.Infra.Data.Context;
using FluentValidation;
using Mapster;
using MediatR;
using Unit = DesafioCurso.Domain.Entities.Unit;

namespace DesafioCurso.Application.Handlers.UnitHandler
{
    public class CreateUnitHandler : IRequestHandler<CreateUnitRequest, CreateUnitResponse>
    {
        private readonly IUnitRepository _context;
        private readonly IUnitOfWork<ApplicationDbContext> _uow;
        private readonly UnitValidation _unitValidation;
        private readonly IShortIdGeneratorService _shortIdGeneratorService;

        public CreateUnitHandler(IUnitRepository context, IUnitOfWork<ApplicationDbContext> uow,
            UnitValidation validations, IShortIdGeneratorService shortIdGenerator)
        {
            _context = context;
            _uow = uow;
            _unitValidation = validations;
            _shortIdGeneratorService = shortIdGenerator;
        }

        public async Task<CreateUnitResponse> Handle(CreateUnitRequest request, CancellationToken cancellationToken)
        {

            var unit = request.Adapt<Unit>();
            // Salvar a propriedade sigla sempre em maiúsculo no banco de dados.
            unit.Acronym = unit.Acronym.ToUpper();

            // Gerar identificador unico para cada unidade criada.
            unit.Identifier = _shortIdGeneratorService.GenerateShortId();

            await _context.Create(unit);
            await _uow.Commit();

            return unit.Adapt<CreateUnitResponse>();
        }
    }
}