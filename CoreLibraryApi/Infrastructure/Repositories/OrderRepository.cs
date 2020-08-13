using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLibraryApi.Infrastructure.Repositories
{
    //TODO транзакции
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppSettings _settings;
        private readonly IGenericRepository<Book> _bookRepository;

        public OrderRepository(ApplicationDbContext context, IGenericRepository<Book> bookRepository, IOptions<AppSettings> settings) : base(context)
        {
            _settings = settings.Value;
            _bookRepository = bookRepository;
        }

        public async Task CloseOrders(int[] orderIds)
        {
            foreach (var id in orderIds)
            {
                var order = await GetByIdAsync(id);
                await DeleteAsync(id);
                var book = await _bookRepository.GetByIdAsync(order.BookId);
                book.Count++;
                await _bookRepository.UpdateAsync(book);
            }
        }

        public async Task CreateOrders(int[] orderIds)
        {
            foreach (var id in orderIds)
            {
                var order = await GetByIdAsync(id);
                var book = await _bookRepository.GetByIdAsync(order.BookId);

                if (book.Count <= 0)
                {
                    throw new ApplicationException("Нет доступных для выдачи книг");
                }

                order.Status = OrderStatusEnum.Accepted;
                order.DateFrom = DateTime.UtcNow;
                order.DateTo = DateTime.UtcNow.AddDays(_settings.OrderDuration);
                await UpdateAsync(order);
                book.Count--;
                await _bookRepository.UpdateAsync(book);
            }
        }

        public async Task ExtendOrders(int[] orderIds)
        {
            foreach (var id in orderIds)
            {
                var order = await GetByIdAsync(id);
                order.DateTo = order.DateTo.HasValue
                    ? order.DateTo.Value.AddDays(_settings.OrderDuration)
                    : DateTime.UtcNow.AddDays(_settings.OrderDuration);
                await UpdateAsync(order);
            }
        }

        public IQueryable<Order> GetUserOrders(int userId)
        {
            return GetAll(new string[] { "Book" }).Where(x => x.UserId == userId);
        }

        public async Task PlaceOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.Status = OrderStatusEnum.Waiting;
                await CreateAsync(order);
            }
        }

        public async Task ReturnOrders(int[] orderIds)
        {
            foreach (var id in orderIds)
            {
                var order = await GetByIdAsync(id);
                order.Status = OrderStatusEnum.Returned;
                order.DateTo = null;
                await UpdateAsync(order);
            }
        }
    }
}
