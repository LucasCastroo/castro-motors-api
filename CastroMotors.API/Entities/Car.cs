namespace CastroMotors.API.Entities
{
    public class Car
    {
        public Guid CarId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public List<OrderItem> OrderItems{ get; set; }
        public byte[] RowVersion { get; set; }
        public string ImagePath { get; set; }

    }
}
