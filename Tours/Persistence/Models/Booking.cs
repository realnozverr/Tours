namespace Tours.Persistence.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime BookingDate { get; set; }
    public int PersonsCount { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Confirmed";
    public Tour Tour { get; set; } = null!;
    public Client Client { get; set; } = null!;
}