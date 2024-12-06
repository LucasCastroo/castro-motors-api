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
    public class CategoriesController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public CategoriesController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todas as categorias de carros.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de categorias de carros disponíveis.
        /// </remarks>
        /// <response code="200">Retorna a lista de categorias.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            return Ok(_context.Categories);
        }

        /// <summary>
        /// Obter uma categoria específica por ID.
        /// </summary>
        /// <param name="id">ID da categoria</param>
        /// <returns>Retorna os detalhes de uma categoria específica</returns>
        /// <response code="200">Retorna a categoria encontrada.</response>
        /// <response code="404">Se a categoria não for encontrada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Category> GetCategoryById(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Criar uma nova categoria.
        /// </summary>
        /// <param name="category">Dados da categoria a ser criada</param>
        /// <returns>Retorna um código de status Created (201) com o objeto da categoria criada</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Category> CreateCategory(Category category)
        {
            category.CategoryId = Guid.NewGuid();
            _context.Categories.Add(category);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, category);
        }

        /// <summary>
        /// Atualizar os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">ID da categoria</param>
        /// <param name="updatedCategory">Dados da categoria a ser atualizada</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCategory(Guid id, Category updatedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = updatedCategory.Name;
            category.Description = updatedCategory.Description;
            category.Code = updatedCategory.Code;
            category.Cars = updatedCategory.Cars;

            return NoContent();
        }

        /// <summary>
        /// Excluir uma categoria existente por ID.
        /// </summary>
        /// <param name="id">ID da categoria a ser excluída</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteCategory(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            return NoContent();
        }
    }
}
