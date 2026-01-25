using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tours.Persistence.Models;

namespace Tours.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(x => x.BookingDate)
            .IsRequired()
            .HasColumnName("booking_date");
        
        builder.Property(x => x.PersonsCount)
            .IsRequired()
            .HasColumnName("persons_count");
        
        builder.Property(x => x.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasColumnName("total_price");
        
        builder.Property(x => x.Status)
            .IsRequired()
            .HasColumnName("status");
        
        builder.HasOne(x => x.Tour)
            .WithMany(x => x.Bookings)
            .HasForeignKey("tour_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(x => x.Client)
            .WithMany(x => x.Bookings)
            .HasForeignKey("client_id");
    }
}