using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tours.Persistence.Models;

namespace Tours.Persistence.Configurations;

public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.ToTable("tours");
        
        // Первичный ключ, автоинкремент
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        // Имя ограничение 255 символов
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("name");
        
        // Страна ограничение 255 символов
        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("country");
            
        // Город ограничение 255 символов
        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("city");
            
        builder.Property(x => x.DurationDays)
            .HasColumnName("duration_days");
        
        // Стоимость ограничение 2 символа после запятой
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .HasColumnType("decimal(18,2)");
        
        // Дата начала
        builder.Property(x => x.StartDate)
            .IsRequired()
            .HasColumnName("start_date");
            
        // Описание ограничение 255 символов
        builder.Property(x => x.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        // связь 1 ко многим, каскадное удаление
        builder.HasMany<Booking>(t => t.Bookings)
            .WithOne(b => b.Tour)
            .HasForeignKey("tour_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}