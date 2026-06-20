namespace FocusFlow.Api.Domain.Entities.Base
{
    public abstract class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAtUtc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
