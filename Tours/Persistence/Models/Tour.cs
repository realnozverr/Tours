namespace Tours.Persistence.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string  Country { get; set; } = null!;
    public string  City { get; set; } = null!;
    public int DurationDays { get; set; }
    public decimal Price { get; set; }
    public DateTime StartDate { get; set; }
    public string? Description { get; set; }
    // Навигационное свойство для ORM
    public List<Booking> Bookings { get; set; } = [];
}