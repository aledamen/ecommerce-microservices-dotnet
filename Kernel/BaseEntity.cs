namespace Kernel
{
    public abstract class BaseEntity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

        public void MarkUpdated() => UpdatedAt = DateTime.UtcNow;
    }
}
