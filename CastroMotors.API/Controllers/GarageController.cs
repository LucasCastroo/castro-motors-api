using CastroMotors.API.Entities;
using CastroMotors.API.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace CastroMotors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly CastroMotorsDbContext _context;

        public GarageController(CastroMotorsDbContext context)
        {
            _context = context;
        }

        // GET: /garage
        /// <summary>
        /// Retorna o pedido atual do usuário, incluindo os itens na garagem.
        /// Se não houver pedido, cria um novo pedido para o usuário.
        /// </summary>
        [HttpGet]
        public ActionResult<Order> GetGarage()
        {
            var userId = GetCurrentUserId();  // Método para obter o ID do usuário (sem autenticação)

            // Buscando o pedido atual do usuário (não finalizado)
            var order = _context.Orders
                                .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            // Se não houver pedido, cria um novo pedido
            if (order == null)
            {
                order = new Order { UserId = userId, IsFinalized = false, OrderItems = new List<OrderItem>() };
                _context.Orders.Add(order); // Adiciona o pedido ao contexto
            }

            return Ok(order); // Retorna o pedido
        }

        // POST: /garage/add-to-garage
        /// <summary>
        /// Adiciona um carro à garagem do usuário.
        /// Se o carro já estiver na garagem, não faz alterações.
        /// </summary>
        [HttpPost("add-to-garage")]
        public ActionResult AddToGarage([FromBody] Guid carId)
        {
            var userId = GetCurrentUserId();  // Método para obter o ID do usuário (sem autenticação)

            // Buscando o pedido atual do usuário (não finalizado)
            var order = _context.Orders
                                .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            // Se não houver pedido, cria um novo
            if (order == null)
            {
                order = new Order { UserId = userId, OrderItems = new List<OrderItem>() };
                _context.Orders.Add(order); // Adiciona o pedido ao contexto
            }

            // Verifica se o carro já está na garagem (na lista de OrderItems)
            if (!order.OrderItems.Any(oi => oi.CarId == carId)) // Corrigido: adicionado predicado de comparação
            {
                order.OrderItems.Add(new OrderItem { CarId = carId });
            }

            return Ok(order); // Retorna o pedido atualizado
        }

        // POST: /garage/remove-from-garage
        /// <summary>
        /// Remove um carro da garagem do usuário.
        /// </summary>
        [HttpPost("remove-from-garage")]
        public ActionResult RemoveFromGarage([FromBody] Guid orderItemId)
        {
            // Busca o OrderItem pela chave primária (ID)
            var orderItem = _context.OrderItems.FirstOrDefault(oi => oi.OrderItemId == orderItemId); // Usando FirstOrDefault com o predicado correto
            if (orderItem == null)
            {
                return NotFound("Item de pedido não encontrado.");
            }

            // Remove o item da garagem
            _context.OrderItems.Remove(orderItem); // Remove o item da garagem

            return Ok("Carro removido da garagem.");
        }

        // POST: /garage/checkout
        /// <summary>
        /// Finaliza o pedido da garagem, marcando-o como finalizado.
        /// </summary>
        [HttpPost("checkout")]
        public ActionResult Checkout()
        {
            var userId = GetCurrentUserId();  // Método para obter o ID do usuário (sem autenticação)

            var order = _context.Orders
                                .FirstOrDefault(o => o.UserId == userId && !o.IsFinalized);

            if (order != null && order.OrderItems.Any()) // Verifica se há itens no pedido
            {
                order.IsFinalized = true; // Marca o pedido como finalizado
            }

            return Ok("Pedido finalizado com sucesso.");
        }

        private Guid GetCurrentUserId()
        {
            // Método fictício para obter o ID do usuário sem autenticação
            // No seu caso, pode ser ajustado para buscar o ID de forma adequada.
            return Guid.NewGuid();  // Retorna um Guid de exemplo
        }
    }

    // DTOs (Data Transfer Objects)

    /// <summary>
    /// DTO para adicionar um carro à garagem do usuário.
    /// </summary>
    public class AddCarToGarageDto
    {
        /// <summary>
        /// ID do carro a ser adicionado à garagem.
        /// </summary>
        public Guid CarId { get; set; }
    }

    /// <summary>
    /// DTO para remover um carro da garagem do usuário.
    /// </summary>
    public class RemoveCarFromGarageDto
    {
        /// <summary>
        /// ID do item do pedido a ser removido.
        /// </summary>
        public Guid OrderItemId { get; set; }
    }
}

