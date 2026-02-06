using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tours.Persistence.Models;

namespace Tours.Persistence.Configurations;

public class ClientConfiguration  : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        
        // Первичный ключ, автоинкремент
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        // Имя ограничение 255 символов
        builder.Property(x => x.FirstName)
            .HasMaxLength(255)
            .HasColumnName("first_name");
        
        // Фамилия ограничение 255 символов
        builder.Property(x => x.LastName)
            .HasMaxLength(255)
            .HasColumnName("last_name");
        
        // паспорт ограничение 255 символов
        builder.Property(x => x.Passport)
            .HasMaxLength(255)
            .HasColumnName("passport");
        
        // почта ограничение 255 символов
        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .HasColumnName("email");

        // телефон ограничение 255 символов
        builder.Property(x => x.Phone)
            .HasMaxLength(255)
            .HasColumnName("phone");

        // связь один ко многим
        builder.HasMany(x => x.Bookings)
            .WithOne(x => x.Client)
            .HasForeignKey("client_id");
    }
}