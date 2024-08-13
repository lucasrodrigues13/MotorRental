using MotorRental.Application.Common;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;

namespace MotorRental.Application.Interfaces
{
    public interface IRentalService : IBaseService<Rental>
    {
        Task<ApiResponse> RentAMotorcycle(RentAMotorcycleDto rentAMotorcycleDto);
        Task<ApiResponse> InformEndDateRental(InformEndDateRentalDto informEndDateRentalDto);
    }
}
