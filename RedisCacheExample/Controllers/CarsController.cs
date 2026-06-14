using Microsoft.AspNetCore.Mvc;
using RedisCacheExample.Models;
using RedisCacheExample.Services;

namespace RedisCacheExample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    // GET: api/cars
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }

    // GET: api/cars/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetCarById(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);

        if (car == null)
            return NotFound(new { message = $"Car with ID {id} not found" });

        return Ok(car);
    }

    // POST: api/cars
    [HttpPost]
    public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

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

        return Ok(updatedCar);
    }

    // DELETE: api/cars/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCar(int id)
    {
        var result = await _carService.DeleteCarAsync(id);

        if (!result)
            return NotFound(new { message = $"Car with ID {id} not found" });

        return NoContent();
    }
}