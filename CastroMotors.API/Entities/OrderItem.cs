namespace CastroMotors.API.Entities
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CarId { get; set; }
    }
}
