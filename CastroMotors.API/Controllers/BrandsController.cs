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
    public class BrandsController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public BrandsController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todas as marcas de carros.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de marcas de carros disponíveis.
        /// </remarks>
        /// <response code="200">Retorna a lista de marcas.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Brand>> GetAllBrands()
        {
            return Ok(_context.Brands);
        }

        /// <summary>
        /// Obter uma marca específica por ID.
        /// </summary>
        /// <param name="id">ID da marca</param>
        /// <returns>Retorna os detalhes de uma marca específica</returns>
        /// <response code="200">Retorna a marca encontrada.</response>
        /// <response code="404">Se a marca não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Brand> GetBrandById(Guid id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }
            return Ok(brand);
        }

        /// <summary>
        /// Criar uma nova marca.
        /// </summary>
        /// <param name="brand">Dados da marca a ser criada</param>
        /// <returns>Retorna um código de status Created (201) com o objeto da marca criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Brand> CreateBrand(Brand brand)
        {
            brand.BrandId = Guid.NewGuid();
            _context.Brands.Add(brand);
            return CreatedAtAction(nameof(GetBrandById), new { id = brand.BrandId }, brand);
        }

        /// <summary>
        /// Atualizar os dados de uma marca existente.
        /// </summary>
        /// <param name="id">ID da marca</param>
        /// <param name="updatedBrand">Dados da marca a ser atualizada</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateBrand(Guid id, Brand updatedBrand)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Name = updatedBrand.Name;
            brand.CountryOfOrigin = updatedBrand.CountryOfOrigin;
            brand.FoundedYear = updatedBrand.FoundedYear;
            brand.Cars = updatedBrand.Cars;

            return NoContent();
        }

        /// <summary>
        /// Excluir uma marca existente por ID.
        /// </summary>
        /// <param name="id">ID da marca a ser excluída</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBrand(Guid id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            return NoContent();
        }
    }
}
