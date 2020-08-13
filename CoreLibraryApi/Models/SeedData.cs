using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace CoreLibraryApi.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Books.Any())
            {
                context.Books.AddRange(
                    new Book
                    {
                        Title = "Harry Potter 1",
                        Count = 2
                    },
                    new Book
                    {
                        Title = "Harry Potter 2",
                        Count = 3
                    },
                    new Book
                    {
                        Title = "Harry Potter 3",
                        Count = 3
                    }
                );
                context.SaveChanges();

                var addedAuthor = context.Authors.Add(new Author { FIO = "Роулинг" });
                context.SaveChanges();

                foreach (var book in context.Books)
                {
                    context.BookAuthors.Add(new BookAuthor { BookId = book.Id, AuthorId = addedAuthor.Entity.Id });
                }
                context.SaveChanges();

                addedAuthor = context.Authors.Add(new Author { FIO = "Толкин" });
                context.SaveChanges();

                var addedBook = context.Books.Add(new Book
                {
                    Title = "LOTR",
                    Count = 1
                });
                context.SaveChanges();

                context.BookAuthors.Add(new BookAuthor { BookId = addedBook.Entity.Id, AuthorId = addedAuthor.Entity.Id });
                context.SaveChanges();

                var genre1 = context.Genres.Add(new Genre { Title = "Фэнтези" });
                var genre2 = context.Genres.Add(new Genre { Title = "Приключения" });
                context.SaveChanges();

                foreach (var book in context.Books)
                {
                    context.BookGenres.AddRange(
                        new BookGenre { BookId = book.Id, GenreId = genre1.Entity.Id },
                        new BookGenre { BookId = book.Id, GenreId = genre2.Entity.Id }
                    );
                }
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new User
                {
                    Login = "admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.ru",
                    Role = Infrastructure.RoleEnum.Administrator,
                    PasswordHash = "12345"
                },
                new User
                {
                    Login = "librarian",
                    FirstName = "Библиотекарь",
                    LastName = "Библиотекарь",
                    Email = "librarian@librarian.ru",
                    Role = Infrastructure.RoleEnum.Librarian,
                    PasswordHash = "12345"
                },
                new User
                {
                    Login = "storekeeper",
                    FirstName = "Кладовщик",
                    LastName = "Кладовщик",
                    Email = "storekeeper@storekeeper.ru",
                    Role = Infrastructure.RoleEnum.Storekeeper,
                    PasswordHash = "12345"
                },
                new User
                {
                    Login = "reader",
                    FirstName = "Читатель",
                    LastName = "Читатель",
                    Email = "reader@reader.ru",
                    Role = Infrastructure.RoleEnum.Reader,
                    PasswordHash = "12345"
                });
                context.SaveChanges();
            }
        }
    }
}
