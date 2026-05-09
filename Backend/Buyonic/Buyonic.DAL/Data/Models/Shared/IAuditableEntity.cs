namespace Buyonic.DAL
{
    public interface IAuditableEntity
    {
        DateTime createdAt { get; set; }
        DateTime? updatedAt { get; set; }
        bool isDeleted { get; set; }
    }
}
