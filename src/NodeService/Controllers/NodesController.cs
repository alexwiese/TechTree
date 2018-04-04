using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTree.NodeService.Database;
using TechTree.NodeService.Models;

namespace TechTree.NodeService.Controllers
{
    [Route("api/[controller]")]
    public class NodesController : Controller
    {
        private readonly NodeContext _context;

        public NodesController(NodeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        public Task<List<Node>> Get()
        {
            return _context.Nodes.ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<Node> Put(int id, [FromBody]Node node)
        {
            var entry = _context.Nodes.Update(node);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        [HttpPost]
        public async Task<Node> Post([FromBody]Node node)
        {
            var entry = _context.Nodes.Add(node);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var node = await _context.Nodes.FindAsync(id);

            if (node == null)
            {
                return NotFound();
            }

            var entry = _context.Nodes.Remove(node);

            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }
    }
}