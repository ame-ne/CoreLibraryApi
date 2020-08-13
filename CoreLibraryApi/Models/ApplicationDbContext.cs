using Microsoft.EntityFrameworkCore;

namespace CoreLibraryApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blob> Blobs { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var booksTable = modelBuilder.Entity<Book>()
                .ToTable("Books");
            booksTable
                .HasMany(b => b.Attachments)
                .WithOne(a => (Book)a.Entity)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                .ToTable("Authors");
            modelBuilder.Entity<Genre>()
                .ToTable("Genres");

            var bookAuthorRelTable = modelBuilder.Entity<BookAuthor>()
                .ToTable("BookAuthorRelationships");
            bookAuthorRelTable.Ignore(x => x.Id);
            bookAuthorRelTable
                .HasKey(key => new { key.BookId, key.AuthorId });
            bookAuthorRelTable
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(k => k.BookId);
            bookAuthorRelTable
                 .HasOne(ba => ba.Author)
                 .WithMany(a => a.BookAuthors)
                 .HasForeignKey(k => k.AuthorId);

            var bookGenreRelTable = modelBuilder.Entity<BookGenre>()
                .ToTable("BookGenreRelationships");
            bookGenreRelTable.Ignore(x => x.Id);
            bookGenreRelTable
                .HasKey(key => new { key.BookId, key.GenreId });
            bookGenreRelTable
                 .HasOne(bg => bg.Book)
                 .WithMany(b => b.BookGenres)
                 .HasForeignKey(k => k.BookId);
            bookGenreRelTable
                 .HasOne(bg => bg.Genre)
                 .WithMany(g => g.BookGenres)
                 .HasForeignKey(k => k.GenreId);

            var attachmentsTable = modelBuilder.Entity<Attachment>()
                .ToTable("Attachments");
            attachmentsTable
                .HasOne(a => a.Blob)
                .WithOne(b => b.Attachment)
                .HasForeignKey<Attachment>(k => k.BlobId);

            modelBuilder.Entity<Blob>()
                .ToTable("Blobs");

            var usersTable = modelBuilder.Entity<User>()
                .ToTable("Users");
            usersTable
                .HasIndex(i => i.Login)
                .IsUnique();

            var ordersTable = modelBuilder.Entity<Order>()
                .ToTable("Orders");
            ordersTable
                .HasKey(x => x.Id);
            ordersTable
                .HasIndex(i => new { i.BookId, i.UserId })
                .IsUnique();
            ordersTable
                .HasOne(o => o.Book)
                .WithMany(b => b.Orders)
                .HasForeignKey(k => k.BookId);
            ordersTable
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(k => k.UserId);

            modelBuilder.Entity<Log>().ToTable("Logs");
        }
    }
}
