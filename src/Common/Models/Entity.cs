namespace TechTree.Common.Models
{
    public abstract class Entity : Entity<int>
    {
    }

    public abstract class Entity<TKey>
        where TKey : struct
    {
        public TKey Id { get; set; }
    }
}