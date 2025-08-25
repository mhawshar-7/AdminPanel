using AdminPanel.Data.Interfaces;

namespace AdminPanel.Data.Entities
{
    public abstract class BaseEntity: ISoftDeletable
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
