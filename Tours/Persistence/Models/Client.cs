namespace Tours.Persistence.Models;

public class Client
{
   public int Id { get; set; }
   public string? FirstName { get; set; }
   public string? LastName { get; set; }
   public string Passport { get; set; } = null!;
   public string Phone { get; set; } = null!;
   public string Email { get; set; }  = null!;
   // Навигационное свойство для ORM
   public List<Booking> Bookings { get; set; } = [];
}