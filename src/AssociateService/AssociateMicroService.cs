using System;
using System.Collections.Generic;
using TechTree.AssociateService.Database;
using TechTree.Common;

namespace TechTree.AssociateService
{
    public class AssociateMicroService : IMicroService
    {
        public string Name { get; } = "Associate Service";

        public IEnumerable<Type> GetDbContextTypes()
        {
            yield return typeof(AssociateContext);
        }
    }
}