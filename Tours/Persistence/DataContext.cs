using Microsoft.EntityFrameworkCore;
using Tours.Persistence.Configurations;
using Tours.Persistence.Models;

namespace Tours.Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Tour> Tours { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { // Конфигурации для базы данных
      modelBuilder.ApplyConfiguration(new TourConfiguration());
      modelBuilder.ApplyConfiguration(new BookingConfiguration()); 
      modelBuilder.ApplyConfiguration(new ClientConfiguration()); 
    }
}