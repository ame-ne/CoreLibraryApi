using CoreLibraryApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IQueryable<Order> GetUserOrders(int userId);
        Task ReturnOrders(int[] orderIds);
        Task CloseOrders(int[] orderIds);
        Task ExtendOrders(int[] orderIds);
        Task CreateOrders(int[] orderIds);
        Task PlaceOrders(List<Order> orders);
    }
}
