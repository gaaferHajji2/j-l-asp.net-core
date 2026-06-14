using RedisCacheExample.Models;

namespace RedisCacheExample.Services;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAllCarsAsync();
    Task<Car?> GetCarByIdAsync(int id);
    Task<Car> CreateCarAsync(Car car);
    Task<Car?> UpdateCarAsync(int id, Car car);
    Task<bool> DeleteCarAsync(int id);
}