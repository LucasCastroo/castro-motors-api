using CastroMotors.API.Entities;
using CastroMotors.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CastroMotors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public CarsController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todos os carros.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de carros disponíveis.
        /// </remarks>
        /// <response code="200">Retorna a lista de carros.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Car>> GetAllCars()
        {
            return Ok(_context.Cars);
        }

        /// <summary>
        /// Obter um carro específico por ID.
        /// </summary>
        /// <param name="id">ID do carro</param>
        /// <returns>Retorna os detalhes de um carro específico</returns>
        /// <response code="200">Retorna o carro encontrado.</response>
        /// <response code="404">Se o carro não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Car> GetCarById(Guid id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        /// <summary>
        /// Criar um novo carro.
        /// </summary>
        /// <param name="car">Dados do carro a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do carro criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Car> CreateCar(Car car)
        {
            car.CarId = Guid.NewGuid();
            _context.Cars.Add(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car.CarId }, car);
        }

        /// <summary>
        /// Atualizar os dados de um carro existente.
        /// </summary>
        /// <param name="id">ID do carro</param>
        /// <param name="updatedCar">Dados do carro a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCar(Guid id, Car updatedCar)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            car.Model = updatedCar.Model;
            car.Year = updatedCar.Year;
            car.Color = updatedCar.Color;
            car.Price = updatedCar.Price;
            car.Description = updatedCar.Description;
            car.BrandId = updatedCar.BrandId;
            car.CategoryId = updatedCar.CategoryId;
            car.ImagePath = updatedCar.ImagePath;

            return NoContent();
        }

        /// <summary>
        /// Excluir um carro existente por ID.
        /// </summary>
        /// <param name="id">ID do carro a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCar(Guid id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            return NoContent();
        }
    }
}
