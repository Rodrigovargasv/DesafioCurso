﻿
using DesafioCurso.Domain.Entities;
using DesafioCurso.Infra.Data.Context;

namespace DesafioCurso.Infra.Data.Repository
{
    public class UnitRepository : RepositoryBase<Unit>
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitRepository(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

    }
}
