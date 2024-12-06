using CastroMotors.API.Entities;
using CastroMotors.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastroMotors.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public OrdersController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todos os pedidos.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de pedidos registrados.
        /// </remarks>
        /// <response code="200">Retorna a lista de pedidos.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return Ok(_context.Orders);
        }

        /// <summary>
        /// Obter um pedido específico por ID.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <returns>Retorna os detalhes de um pedido específico</returns>
        /// <response code="200">Retorna o pedido encontrado.</response>
        /// <response code="404">Se o pedido não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Order> GetOrderById(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Criar um novo pedido.
        /// </summary>
        /// <param name="order">Dados do pedido a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do pedido criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Order> CreateOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            _context.Orders.Add(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        /// <summary>
        /// Atualizar os dados de um pedido existente.
        /// </summary>
        /// <param name="id">ID do pedido</param>
        /// <param name="updatedOrder">Dados do pedido a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateOrder(Guid id, Order updatedOrder)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            order.UserId = updatedOrder.UserId;
            order.IsFinalized = updatedOrder.IsFinalized;
            order.OrderItems = updatedOrder.OrderItems;

            return NoContent();
        }

        /// <summary>
        /// Excluir um pedido existente por ID.
        /// </summary>
        /// <param name="id">ID do pedido a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrder(Guid id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            return NoContent();
        }
    }
}
