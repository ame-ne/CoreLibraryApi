using System.Collections.Generic;

namespace CoreLibraryApi.Models
{
    public class Genre : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
    }
}
