using System;
using System.Collections.Generic;
using TechTree.Common;
using TechTree.NodeService.Database;

namespace TechTree.NodeService
{
    public class NodeMicroService : IMicroService
    {
        public string Name { get; } = "Node Service";

        public IEnumerable<Type> GetDbContextTypes()
        {
            yield return typeof(NodeContext);
        }
    }
}