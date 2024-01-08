using Microsoft.EntityFrameworkCore;

namespace DesafioCurso.Infra.Data.Context
{
    public class DataBaseInMemory : DbContext
    {
        public DataBaseInMemory(DbContextOptions<DataBaseInMemory> options) : base(options)
        {
        }
    }
}