namespace CastroMotors.API.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public String Username { get; set; }
        public String Email { get; set; }
        public List<Order> Orders { get; set; }
    }
}
