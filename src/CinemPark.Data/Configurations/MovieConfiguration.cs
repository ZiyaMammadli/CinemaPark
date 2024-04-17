using CinemaPark.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPark.Data.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.Property(m => m.Name).IsRequired().HasMaxLength(40);
        builder.Property(m=>m.Desc).IsRequired().HasMaxLength(250);
        builder.Property(m => m.CostPrice).IsRequired();
        builder.Property(m => m.SalePrice).IsRequired();
        builder.Property(m => m.IsDeleted).IsRequired();

        builder.HasOne(x=>x.Genre).WithMany(x=>x.Movies).HasForeignKey(m=>m.GenreId).OnDelete(DeleteBehavior.Cascade);
    }
}
