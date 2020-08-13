using CoreLibraryApi.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace CoreLibraryApi.Models
{
    public class Author : BaseEntity, IEntityWithAttachment
    {
        public string FIO { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
