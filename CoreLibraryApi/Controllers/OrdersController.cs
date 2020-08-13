using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreLibraryApi.Infrastructure;
using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibraryApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public OrdersController(IOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("list")]
        [Authorize(Roles = "Administrator,Librarian")]
        public IActionResult GetList()
        {
            var data = _repository.GetAll(new string[] { "Book", "User" });
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator,Librarian")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("add-orders")]
        [Authorize(Roles = "Administrator,Librarian")]
        public async Task<IActionResult> AddOrders([FromBody] int[] orderIds)
        {
            await _repository.CreateOrders(orderIds);
            return Ok();
        }

        [HttpPost("place-orders")]
        public async Task<IActionResult> PlaceOrders(List<Order> orders)
        {
            await _repository.PlaceOrders(orders);
            return Ok();
        }

        [HttpGet("user-orders/{userId}")]
        public IActionResult GetUserOrders(int userId)
        {
            if (HttpContext.User != null
                && (
                //HttpContext.User.IsInRole(RoleEnum.Administrator.ToString())
                //|| HttpContext.User.IsInRole(RoleEnum.Librarian.ToString())
                //|| 
                HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) == userId.ToString()))
            {
                var data = _repository.GetUserOrders(userId);
                return Ok(data);
            }
            return Forbid();
        }

        [HttpPost("return-orders/{userId}")]
        public async Task<IActionResult> ReturnOrders(int userId, [FromBody] int[] orderIds)
        {
            if (HttpContext.User != null && HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) == userId.ToString())
            {
                await _repository.ReturnOrders(orderIds);
                return Ok();
            }
            return Forbid();
        }

        [HttpPost("close-orders")]
        [Authorize(Roles = "Administrator,Librarian")]
        public async Task<IActionResult> CloseOrders([FromBody] int[] orderIds)
        {
            await _repository.CloseOrders(orderIds);
            return Ok();
        }

        [HttpPost("extend-orders")]
        [Authorize(Roles = "Administrator,Librarian")]
        public async Task<IActionResult> ExtendOrders([FromBody] int[] orderIds)
        {
            await _repository.ExtendOrders(orderIds);
            return Ok();
        }
    }
}
