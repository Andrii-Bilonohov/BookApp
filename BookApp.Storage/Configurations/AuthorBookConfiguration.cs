using BookApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookApp.Storage.Configurations
{
    public class AuthorBookConfiguration : IEntityTypeConfiguration<AuthorBook>
    {
        public void Configure(EntityTypeBuilder<AuthorBook> builder)
        {
            builder.HasKey(ab => new { ab.AuthorId, ab.BookId });

            builder.HasOne(ab => ab.Author)
                   .WithMany(a => a.BookAuthors)
                   .HasForeignKey(ab => ab.AuthorId);

            builder.HasOne(ab => ab.Book)
                   .WithMany(b => b.BookAuthors)
                   .HasForeignKey(ab => ab.BookId);
        }
    }
}
