using Microsoft.EntityFrameworkCore;

namespace Kursach.Models.Domain
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Company> Company { get; set; }  /*делаем таблицу в бд*/
    }
}
