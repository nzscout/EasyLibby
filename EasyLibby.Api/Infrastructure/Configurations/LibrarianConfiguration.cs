using EasyLibby.Api.Data;
using EasyLibby.Api.Entities.LibrarianAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Api.Data.Configurations;

public class LibrarianConfiguration : IEntityTypeConfiguration<Librarian>
{
    public void Configure(EntityTypeBuilder<Librarian> builder)
    {
        builder.HasData
        (
            SeedData.GetLibrarians()
        );
    }
}