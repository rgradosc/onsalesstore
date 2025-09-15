﻿using OnSalesStore.ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnSalesStore.ECommerce.Persistence.Configurations
{
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(t => t.Description)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(t => t.Percent)
                .HasPrecision(9, 2)
                .IsRequired();
        }
    }
}
