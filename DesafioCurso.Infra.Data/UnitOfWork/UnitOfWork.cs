using DesafioCurso.Domain.Interfaces;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Implementação do unit of work
        public async Task<bool> Commit()
          =>   await _context.SaveChangesAsync() > 0;
        

        public void Rollback()
        {
            // não faz nada;
            
        }
    }
}
