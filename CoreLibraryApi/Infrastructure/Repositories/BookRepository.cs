using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task UpdateAsync(Book entity)
        {
            using (var transaction = BeginTransaction())
            {
                try
                {
                    var dbEntity = await GetByIdAsync(entity.Id, new string[] { "BookAuthors.Author", "BookGenres.Genre", "Attachments" });
                    if (dbEntity == null)
                    {
                        throw new ApplicationException($"Entity of type {typeof(Book)} with the Id={entity.Id} not found");
                    }
                    dbEntity.Title = entity.Title;
                    dbEntity.Count = entity.Count;

                    var bookAuthors = entity.BookAuthors.Select(x => new BookAuthor
                    {
                        BookId = entity.Id,
                        AuthorId = x.AuthorId
                    }).ToList();

                    if (dbEntity.BookAuthors != null && dbEntity.BookAuthors.Any())
                    {
                        dbEntity.BookAuthors.Clear();
                    }
                    else
                    {
                        dbEntity.BookAuthors = new List<BookAuthor>();
                    }
                    bookAuthors.ForEach(x => dbEntity.BookAuthors.Add(x));

                    var bookGenres = entity.BookGenres.Select(x => new BookGenre
                    {
                        BookId = entity.Id,
                        GenreId = x.GenreId
                    }).ToList();

                    if (dbEntity.BookGenres != null && dbEntity.BookGenres.Any())
                    {
                        dbEntity.BookGenres.Clear();
                    }
                    else
                    {
                        dbEntity.BookGenres = new List<BookGenre>();
                    }
                    bookGenres.ForEach(x => dbEntity.BookGenres.Add(x));

                    var attachments = entity.Attachments.Select(x => new Attachment
                    {
                        Name = x.Name,
                        IsMain = x.IsMain,
                        IsPreview = x.IsPreview,
                        Type = x.Type,
                        BlobId = x.BlobId,
                        EntityId = entity.Id
                    });

                    if (dbEntity.Attachments != null && dbEntity.Attachments.Any())
                    {
                        dbEntity.Attachments.Clear();
                    }
                    else
                    {
                        dbEntity.Attachments = new List<Attachment>();
                    }
                    attachments.ToList().ForEach(x => dbEntity.Attachments.Add(x));

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
