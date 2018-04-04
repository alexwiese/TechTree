using System;
using System.Collections.Generic;

namespace TechTree.Common
{
    public interface IMicroService
    {
        string Name { get; }

        IEnumerable<Type> GetDbContextTypes();
    }
}