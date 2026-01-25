using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tours.Persistence.Models;

namespace Tours.Persistence.Configurations;

public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.ToTable("tours");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("name");
        
        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("country");
            
        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("city");
            
        builder.Property(x => x.DurationDays)
            .HasColumnName("duration_days");
            
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18,2)");
            
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnName("start_date");
            
        builder.Property(x => x.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        builder.HasMany<Booking>(t => t.Bookings)
            .WithOne(b => b.Tour)
            .HasForeignKey("tour_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}