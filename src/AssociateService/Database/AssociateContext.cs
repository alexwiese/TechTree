using Microsoft.EntityFrameworkCore;
using TechTree.AssociateService.Models;

namespace TechTree.AssociateService.Database
{
    public class AssociateContext : DbContext
    {
        public AssociateContext(DbContextOptions<AssociateContext> options) 
            : base(options)
        {
            
        }

        public AssociateContext()
        {
        }

        public DbSet<Associate> Associates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Association>()
                .HasKey(a => new {a.AssociateId, a.NodeId});
        }
    }
}