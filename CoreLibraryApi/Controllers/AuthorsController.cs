using System.Threading.Tasks;
using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibraryApi.Controllers
{
    [Route("api/authors")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IGenericRepository<Author> _repository;

        public AuthorsController(IGenericRepository<Author> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var authors = _repository.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            Author author = await _repository.GetByIdAsync(id);
            if (author != null)
            {
                return Ok(author);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Post(Author author)
        {
            await _repository.CreateAsync(author);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Put(Author author)
        {
            await _repository.UpdateAsync(author);
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
