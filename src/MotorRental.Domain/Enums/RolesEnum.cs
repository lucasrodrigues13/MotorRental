using System.ComponentModel;

namespace MotorRental.Domain.Enums
{
    public enum RolesEnum
    {
        [Description("Admin")]
        Admin = 0,
        [Description("DeliverDriver")]
        DeliverDriver = 1
    }
}
