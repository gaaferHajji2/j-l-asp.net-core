using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Models;
using RedisCacheExample.Services;
using RedisCacheExample.Services.Cache;

namespace RedisCacheExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly ICarCache _cache;

    public CarsController(ICarService carService, ICarCache carCache)
    {
        _carService = carService;
        _cache = carCache;
    }

    // GET: api/cars
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
    {
        var cars = _cache.GetData<IEnumerable<Car>>("cars");
        if(cars is not null)
        {
            Console.WriteLine("Getting The Cars From The Cache");
            return Ok(cars);
        }

        cars = await _carService.GetAllCarsAsync();
        _cache.SetData("cars", cars);
        return Ok(cars);
    }

    // GET: api/cars/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        var car = _cache.GetData<Car>($"car_{id}");
        if(car is not null)
        {
            Console.WriteLine("Getting The Car From The Cache");
            return Ok(car);
        }

        car = await _carService.GetCarByIdAsync(id);
        if (car == null)
            return NotFound(new { message = $"Car with ID {id} not found" });

        _cache.SetData($"car_{id}", car);
        return Ok(car);
    }

    // POST: api/cars
    [HttpPost]
    public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        _cache.InvalidateCache("");
        var createdCar = await _carService.CreateCarAsync(car);
        return CreatedAtAction(nameof(GetCarById), new { id = createdCar.Id }, createdCar);
    }

    // PUT: api/cars/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Car>> UpdateCar(int id, [FromBody] Car car)
    {
        if (id != car.Id)
            return BadRequest(new { message = "ID mismatch" });

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedCar = await _carService.UpdateCarAsync(id, car);

        if (updatedCar == null)
            return NotFound(new { message = $"Car with ID {id} not found" });

        _cache.InvalidateCache($"car_{id}");

        return Ok(updatedCar);
    }

    // DELETE: api/cars/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCar(int id)
    {
        var result = await _carService.DeleteCarAsync(id);

        if (!result)
            return NotFound(new { message = $"Car with ID {id} not found" });

        _cache.InvalidateCache($"car_{id}");

        return NoContent();
    }
}