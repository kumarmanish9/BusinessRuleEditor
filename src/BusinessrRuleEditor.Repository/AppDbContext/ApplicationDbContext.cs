using BusinessRuleEditor.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessRuleEditor.Repository.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<WorkflowCategory> WorkflowCategories { get; set; }



    }
}
