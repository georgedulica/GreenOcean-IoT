﻿using GreenOcean.Data.Entities;
using GreenOcean.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data.Repositories;

public class RegisteredEquipmentRepository : IRegisteredEquipmentRepository
{
    private readonly DataContext _dataContext;
    public RegisteredEquipmentRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<bool> AddRegisteredEquipment(RegisteredEquipment registeredEquipment)
    {
        try
        {
            var existingEquipment = await ExistsRegisteredEquipment(registeredEquipment);

            if (existingEquipment == true)
            {
                return false;
            }

            await _dataContext.AddAsync(registeredEquipment);
            await _dataContext.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"The equipment cannot be saved {ex}");
        }
    }

    private Task<bool> ExistsRegisteredEquipment(RegisteredEquipment registeredEquipment)
    {
        var existingEquipment = _dataContext.RegisteredEquipments.AnyAsync(e => string.Equals(e.Code, registeredEquipment.Code));
        return existingEquipment;
    }
}