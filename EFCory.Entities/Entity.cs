namespace EFCory.Entities
{
    public abstract class Entity<TKey>
    {
        public TKey Id { get; set; }
    }

}
