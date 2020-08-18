using CoreLibraryApi.Infrastructure.Dto;
using CoreLibraryApi.Models;
using System.Linq;

namespace CoreLibraryApi.Infrastructure.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        public IQueryable<BookListItemDto> GetAllWithDapper();
    }
}
