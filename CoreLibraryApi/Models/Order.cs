using CoreLibraryApi.Infrastructure;
using System;

namespace CoreLibraryApi.Models
{
    public class Order : BaseEntity
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public OrderStatusEnum Status { get; set; }
    }
}
