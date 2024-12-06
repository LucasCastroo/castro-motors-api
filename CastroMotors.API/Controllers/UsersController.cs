using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CastroMotors.API.Entities;
using CastroMotors.API.Persistence;

namespace CastroMotors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public UsersController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todos os usuários registrados.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de usuários registrados.
        /// </remarks>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return Ok(_context.Users);
        }

        /// <summary>
        /// Obter um usuário específico por ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Retorna os detalhes de um usuário específico</returns>
        /// <response code="200">Retorna o usuário encontrado.</response>
        /// <response code="404">Se o usuário não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetUserById(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Criar um novo usuário.
        /// </summary>
        /// <param name="user">Dados do usuário a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do usuário criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<User> CreateUser(User user)
        {
            user.UserId = Guid.NewGuid();
            _context.Users.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        /// <summary>
        /// Atualizar os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="updatedUser">Dados do usuário a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(Guid id, User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = updatedUser.Email;
            user.Username = updatedUser.Username;
            return NoContent();
        }

        /// <summary>
        /// Excluir um usuário existente por ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            return NoContent();
        }
    }
}