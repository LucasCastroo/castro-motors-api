namespace CastroMotors.API.Entities
{
    public class Brand
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int FoundedYear { get; set; }
        public List<Car> Cars { get; set; }
    }
}
