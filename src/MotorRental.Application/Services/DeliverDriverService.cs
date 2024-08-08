﻿using MotorRental.Application.Interfaces;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Application.Services
{
    public class DeliverDriverService(IDeliverDriverRepository repository) : BaseService<DeliverDriver>(repository), IDeliverDriverService
    {
    }
}
