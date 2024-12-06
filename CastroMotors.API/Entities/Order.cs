namespace CastroMotors.API.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public bool IsFinalized { get; set; }
        public List<OrderItem> OrderItems { get; set; }

    }
}
