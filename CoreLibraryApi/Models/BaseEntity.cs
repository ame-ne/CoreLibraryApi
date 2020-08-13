using CoreLibraryApi.Infrastructure.Interfaces;

namespace CoreLibraryApi.Models
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
