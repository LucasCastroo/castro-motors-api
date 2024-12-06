using CastroMotors.API.Entities;

namespace CastroMotors.API.Persistence
{
    
    public class CastroMotorsDbContext
    {
        // Simula tabelas com listas
        public List<User> Users { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Car> Cars { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }

        // Inicializa as tabelas em memória
        public CastroMotorsDbContext()
        {
            Users = new List<User>();
            Orders = new List<Order>();
            OrderItems = new List<OrderItem>();
            Cars = new List<Car>();
            Categories = new List<Category>();
            Brands = new List<Brand>();
        }
    }

}
