using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Created = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }
        public DateTime Created { get; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
