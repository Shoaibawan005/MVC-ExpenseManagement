using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class ExpensesDbContext : DbContext
    {

        
        DbSet<Expenses> Expenses { get; set; }

        public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
        {
            
        }
    }
}
