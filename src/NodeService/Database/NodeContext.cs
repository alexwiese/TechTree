using Microsoft.EntityFrameworkCore;
using TechTree.NodeService.Models;

namespace TechTree.NodeService.Database
{
    public class NodeContext : DbContext
    {
        public NodeContext(DbContextOptions<NodeContext> options) 
            : base(options)
        {
        }

        public NodeContext()
        {
        }

        public DbSet<Node> Nodes { get; set; }
    }
}