using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tours.Persistence.Models;

namespace Tours.Persistence.Configurations;

public class ClientConfiguration  : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(x => x.FirstName)
            .HasMaxLength(255)
            .HasColumnName("first_name");
            
        builder.Property(x => x.LastName)
            .HasMaxLength(255)
            .HasColumnName("last_name");
        
        builder.Property(x => x.Passport)
            .HasMaxLength(255)
            .HasColumnName("passport");
        
        builder.Property(x => x.Email)
            .HasMaxLength(255)
            .HasColumnName("email");

        builder.Property(x => x.Phone)
            .HasMaxLength(255)
            .HasColumnName("phone");

        builder.HasMany(x => x.Bookings)
            .WithOne(x => x.Client)
            .HasForeignKey("client_id");
    }
}