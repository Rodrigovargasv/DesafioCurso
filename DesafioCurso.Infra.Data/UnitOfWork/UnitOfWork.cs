using DesafioCurso.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.UnitOfWork
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public UnitOfWork(TDbContext context)
        {
            _context = context;
        }

        // Implementação do unit of work com o método saveChanges
        public async Task<bool> Commit()
          => await _context.SaveChangesAsync() > 0;

        public void Rollback()
        {
            // não faz nada;
        }
    }
}