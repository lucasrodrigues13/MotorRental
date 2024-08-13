using MotorRental.Application.Common;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Constants;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Enums;
using MotorRental.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace MotorRental.Application.Services
{
    public class RentalService : BaseService<Rental>, IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliverDriverRepository _deliverDriverRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IMotorcyleRepository _motorcyleRepository;
        public RentalService(IRentalRepository rentalRepository,
            IDeliverDriverRepository deliverDriverRepository,
            IPlanRepository planRepository,
            IMotorcyleRepository motorcyleRepository) : base(rentalRepository)
        {
            _rentalRepository = rentalRepository;
            _deliverDriverRepository = deliverDriverRepository;
            _planRepository = planRepository;
            _motorcyleRepository = motorcyleRepository;
        }

        public async Task<ApiResponse> RentAMotorcycle(RentAMotorcycleDto rentAMotorcycleDto)
        {
            var motorcycle = await _motorcyleRepository.GetByIdAsync(rentAMotorcycleDto.MotorcycleId);
            var deliverDriver = await _deliverDriverRepository.GetByIdAsync(rentAMotorcycleDto.DeliverDriverId);
            var plan = await _planRepository.GetByIdAsync(rentAMotorcycleDto.PlanId);
            var errors = ValidRentAMotorcycle(rentAMotorcycleDto, deliverDriver, motorcycle, plan);

            if (errors.Any())
                return ApiResponse.BadRequest(errors);

            var rental = new Rental
            {
                DeliverDriverId = deliverDriver.Id,
                PlanId = plan.Id,
                MotorcycleId = motorcycle.Id,
                StartDate = rentAMotorcycleDto.StartDate,
                ExpectedEndDate = rentAMotorcycleDto.StartDate.AddDays(plan.NumberOfDays),
                ExpectedPrice = plan.NumberOfDays * plan.DailyPrice
            };
            await _rentalRepository.AddAsync(rental);

            return ApiResponse.Ok();
        }

        public async Task<ApiResponse> InformEndDateRental(InformEndDateRentalDto informEndDateRentalDto)
        {
            var rental = await _rentalRepository.GetByIdAsync(informEndDateRentalDto.RentalId);
            if (rental == null)
                return ApiResponse.BadRequest(new List<string> { ErrorMessagesConstants.RENTAL_DOESNT_EXIST });

            ApplyPriceInformEndDateRental(informEndDateRentalDto, rental);

            return ApiResponse.Ok(rental);
        }

        private List<string> ValidRentAMotorcycle(RentAMotorcycleDto rentAMotorcycleDto, DeliverDriver deliverDriver, Motorcycle motorcycle, Plan plan)
        {
            var errors = new List<string>();
            if (plan == null)
            {
                errors.Add(ErrorMessagesConstants.PLAN_NOT_REGISTERED);
                return errors;
            }

            if (rentAMotorcycleDto.StartDate < DateTime.Now.Date)
                errors.Add(ErrorMessagesConstants.START_DATE_REQUIRED_INVALID);

            if (deliverDriver == null)
            {
                errors.Add(ErrorMessagesConstants.DELIVER_DRIVER_NOT_REGISTERED);
                return errors;
            }

            if (deliverDriver.LicenseDriverType != LicenseDriverTypeEnum.A && deliverDriver.LicenseDriverType != LicenseDriverTypeEnum.AB)
                errors.Add(ErrorMessagesConstants.DELIVER_DRIVER_NOT_QUALIFIED);

            if (deliverDriver.Rental != null && (deliverDriver.Rental.EndDate == null || deliverDriver.Rental.EndDate >= DateTime.Now.Date))
                errors.Add(ErrorMessagesConstants.DELIVER_DRIVER_RENT_ACTIVE);

            if (motorcycle == null)
            {
                errors.Add(ErrorMessagesConstants.NO_MOTORCYCLE_AVAILABLE);
                return errors;
            }

            if (motorcycle.Rental != null && (motorcycle.Rental.EndDate == null || motorcycle.Rental.EndDate >= DateTime.Now.Date))
                errors.Add(ErrorMessagesConstants.MOTORCYCLE_RENT_ACTIVE);

            return errors;
        }

        private void ApplyPriceInformEndDateRental(InformEndDateRentalDto informEndDateRentalDto, Rental rental)
        {

            if (informEndDateRentalDto.EndDate < rental.ExpectedEndDate)
            {
                decimal twentyPercent = new decimal(0.2);
                decimal fourtyPercent = new decimal(0.4);
                var unusedDays = (rental.ExpectedEndDate - informEndDateRentalDto.EndDate).Value.Days;
                var usedDays = (DateTime.Now.Date - rental.StartDate).Days;
                var unusedDaysPrice = unusedDays * rental.Plan.DailyPrice;
                var usedDaysPrice = usedDays * rental.Plan.DailyPrice;

                if (rental.Plan.NumberOfDays <= 7)
                    rental.Price = (unusedDaysPrice * twentyPercent) + usedDaysPrice;
                else if (rental.Plan.NumberOfDays >= 15)
                    rental.Price = (unusedDaysPrice * fourtyPercent) + usedDaysPrice;
            }
            else if (informEndDateRentalDto.EndDate > rental.ExpectedEndDate)
            {
                var overUseTax = new decimal(50.0);
                var overUsedDays = (informEndDateRentalDto.EndDate - rental.ExpectedEndDate).Value.Days;

                rental.Price = (overUsedDays * (rental.Plan.DailyPrice + overUseTax)) + (rental.Plan.NumberOfDays * rental.Plan.DailyPrice);
            }
        }
    }
}
