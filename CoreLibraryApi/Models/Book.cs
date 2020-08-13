using CoreLibraryApi.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace CoreLibraryApi.Models
{
    public class Book : BaseEntity, IEntityWithAttachment
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public ICollection<BookGenre> BookGenres { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
