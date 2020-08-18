using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Dto
{
    public sealed class BookListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
        public List<GenreDto> Genres { get; set; }
        public List<AuthorDto> Authors { get; set; }
    }

    public sealed class GenreDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public sealed class AuthorDto
    {
        public int Id { get; set; }
        public string FIO { get; set; }
    }
}
