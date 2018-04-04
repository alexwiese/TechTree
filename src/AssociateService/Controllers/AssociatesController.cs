using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTree.AssociateService.Database;
using TechTree.AssociateService.Models;

namespace TechTree.AssociateService.Controllers
{
    [Route("api/[controller]")]
    public class AssociatesController : Controller
    {
        private readonly AssociateContext _context;

        public AssociatesController(AssociateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        public Task<List<Associate>> Get()
        {
            return _context.Associates.ToListAsync();
        }

        [HttpGet("/api/nodes/{nodeId}/associations")]
        public async Task<object> GetAssociationsForNode(int nodeId)
        {
            return await _context.Associates
                    .SelectMany(a => a.Associations)
                    .Include(a => a.Associate)
                    .Where(a => a.NodeId == nodeId)
                    .Select(a => new
                    {
                        AssociateId = a.Associate.Id,
                        a.Associate.Name,
                        AssociatedNodeIds = a.Associate.Associations.Select(ass => ass.NodeId),
                        a.AssociationType
                    })
                    .ToListAsync();
        }

        [HttpPut("{id}")]
        public async Task<Associate> Put(int id, [FromBody]Associate associate)
        {
            var entry = _context.Associates.Update(associate);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        [HttpPost]
        public async Task<Associate> Post([FromBody]Associate associate)
        {
            var entry = _context.Associates.Add(associate);

            await _context.SaveChangesAsync();

            return entry.Entity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var associate = await _context.Associates.FindAsync(id);

            if (associate == null)
            {
                return NotFound();
            }

            var entry = _context.Associates.Remove(associate);

            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }
    }
}