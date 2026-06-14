using RedisCacheExample.Models;

namespace RedisCacheExample.Services;

public class CarService : ICarService
{
    // In-memory storage for demo (replace with DbContext in production)
    private static readonly List<Car> _cars = new List<Car>();
    private static int _nextId = 1;

    public Task<IEnumerable<Car>> GetAllCarsAsync()
    {
        return Task.FromResult<IEnumerable<Car>>(_cars.AsReadOnly());
    }

    public Task<Car?> GetCarByIdAsync(int id)
    {
        var car = _cars.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(car);
    }

    public Task<Car> CreateCarAsync(Car car)
    {
        car.Id = _nextId++;
        car.CreatedAt = DateTime.UtcNow;
        _cars.Add(car);
        return Task.FromResult(car);
    }

    public Task<Car?> UpdateCarAsync(int id, Car car)
    {
        var existingCar = _cars.FirstOrDefault(c => c.Id == id);
        if (existingCar == null)
            return Task.FromResult<Car?>(null);

        existingCar.Make = car.Make;
        existingCar.Model = car.Model;
        existingCar.Year = car.Year;
        existingCar.Price = car.Price;
        existingCar.Color = car.Color;

        return Task.FromResult<Car?>(existingCar);
    }

    public Task<bool> DeleteCarAsync(int id)
    {
        var car = _cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
            return Task.FromResult(false);

        _cars.Remove(car);
        return Task.FromResult(true);
    }
}