﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Data.Configuration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(location => location.Initials)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(location => location.Description)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(location => location.Level)
                .HasDefaultValue(0)
                .IsRequired();

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Childrens)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.CollectionItems)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
        }
    }
}
