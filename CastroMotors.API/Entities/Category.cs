namespace CastroMotors.API.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public List<Car> Cars { get; set; }
    }
}
