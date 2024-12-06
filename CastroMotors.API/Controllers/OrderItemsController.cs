using CastroMotors.API.Entities;
using CastroMotors.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CastroMotors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public OrderItemsController()
        {
            _context = new CastroMotorsDbContext();
        }

        /// <summary>
        /// Obter todos os itens de pedidos.
        /// </summary>
        /// <remarks>
        /// Retorna a lista completa de itens de pedidos.
        /// </remarks>
        /// <response code="200">Retorna a lista de itens de pedidos.</response>
        /// <response code="400">Se ocorrer um erro ao processar a requisição.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<OrderItem>> GetAllOrderItems()
        {
            return Ok(_context.OrderItems);
        }

        /// <summary>
        /// Obter um item de pedido específico por ID.
        /// </summary>
        /// <param name="id">ID do item do pedido</param>
        /// <returns>Retorna os detalhes de um item de pedido específico</returns>
        /// <response code="200">Retorna o item de pedido encontrado.</response>
        /// <response code="404">Se o item de pedido não for encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<OrderItem> GetOrderItemById(Guid id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        /// <summary>
        /// Criar um novo item de pedido.
        /// </summary>
        /// <param name="orderItem">Dados do item de pedido a ser criado</param>
        /// <returns>Retorna um código de status Created (201) com o objeto do item de pedido criado</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<OrderItem> CreateOrderItem(OrderItem orderItem)
        {
            orderItem.OrderItemId = Guid.NewGuid();
            _context.OrderItems.Add(orderItem);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, orderItem);
        }

        /// <summary>
        /// Atualizar os dados de um item de pedido existente.
        /// </summary>
        /// <param name="id">ID do item de pedido</param>
        /// <param name="updatedOrderItem">Dados do item de pedido a ser atualizado</param>
        /// <returns>Retorna um código de status NoContent (204) se a atualização for bem-sucedida.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateOrderItem(Guid id, OrderItem updatedOrderItem)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.OrderId = updatedOrderItem.OrderId;
            orderItem.CarId = updatedOrderItem.CarId;

            return NoContent();
        }

        /// <summary>
        /// Excluir um item de pedido existente por ID.
        /// </summary>
        /// <param name="id">ID do item de pedido a ser excluído</param>
        /// <returns>Retorna um código de status NoContent (204) em caso de sucesso.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrderItem(Guid id)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            return NoContent();
        }
    }
}
