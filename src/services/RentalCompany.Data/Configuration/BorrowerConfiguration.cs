﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCompany.Domain.Entities;

namespace RentalCompany.Data.Configuration
{
    public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
    {
        public void Configure(EntityTypeBuilder<Borrower> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();

            builder.OwnsOne(x => x.Email, emailBuilder =>
            {
                emailBuilder.WithOwner();

                emailBuilder.Property(email => email.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(256)
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Address, addressBuilder =>
            {
                addressBuilder.WithOwner();

                addressBuilder.Property(address => address.Street)
                    .HasColumnName("Street")
                    .HasMaxLength(256)
                    .IsRequired();

                addressBuilder.Property(address => address.PostalCode)
                    .HasColumnName("PostalCode")
                    .HasMaxLength(8)
                    .IsRequired();

                addressBuilder.Property(address => address.City)
                   .HasColumnName("City")
                   .HasMaxLength(256)
                   .IsRequired();

                addressBuilder.Property(address => address.Number)
                    .HasColumnName("Number")
                    .HasMaxLength(256)
                    .IsRequired();
            });

            builder.Property(x => x.Phone).IsRequired();

            builder.Ignore(x => x.FullName);

            builder.Property(x => x.CreatedAt);
            builder.Property(x => x.UpdatedAt);
        }
    }
}
