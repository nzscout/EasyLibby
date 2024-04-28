using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.BookAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(b => b.ISBN).HasPrecision(13, 0);
        builder.HasData
        (
            SeedData.GetBooks()
        );
    }
}