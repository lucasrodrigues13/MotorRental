using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class Rental : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? ExpectedPrice { get; set; }

        public int DeliverDriverId { get; set; }
        public int PlanId { get; set; }
        public int MotorcycleId { get; set; }


        public virtual DeliverDriver DeliverDriver { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }
    }
}
