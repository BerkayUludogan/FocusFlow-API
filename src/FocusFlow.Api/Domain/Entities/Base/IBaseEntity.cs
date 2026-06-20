namespace FocusFlow.Api.Domain.Entities.Base
{
    public interface IBaseEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAtUtc { get; set; }
        DateTime? ModifiedAtUtc { get; set; }
        bool IsDeleted { get; set; }
    }
}
