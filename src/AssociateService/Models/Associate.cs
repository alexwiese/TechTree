using System.Collections.Generic;
using TechTree.Common.Models;

namespace TechTree.AssociateService.Models
{
    public class Associate : Entity
    {
        public string Name { get; set; }
        public virtual List<Association> Associations { get; set; }
    }
}