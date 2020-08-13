using System.Threading.Tasks;
using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibraryApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;

        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var books = _repository.GetAll(new string[] { "BookAuthors.Author", "BookGenres.Genre", "Attachments" });
            return Ok(books);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            Book book = await _repository.GetByIdAsync(id, new string[] { "BookAuthors.Author", "BookGenres.Genre", "Attachments" });
            if (book != null)
            {
                return Ok(book);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Post(Book book)
        {
            var id = await _repository.CreateAsync(book);
            return Ok(id);
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Put(Book book)
        {
            await _repository.UpdateAsync(book);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return Ok();
        }
    }
}
