﻿using System.Xml;
using CarWebAPI.CarManagement;

namespace CarWebAPI.DbContext;

public class InMemoryDbContext : IInMemoryDbContext
{
    private readonly List<Car> _entities = new List<Car>();

    public void Add(Car entity)
    {
        _entities.Add(entity);
    }

    public IEnumerable<Car> GetAll()
    {
        return _entities;
    }

    public Car GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }

    public void UpdateCar(int id, DateTime newMaintenanceDate)
    {
        var existingCar = GetById(id);
        if (existingCar != null)
        {
            existingCar.LastMaintenanceDate = newMaintenanceDate;
            existingCar.NextMaintenanceDate = newMaintenanceDate.AddMonths(6);
        }
    }

}