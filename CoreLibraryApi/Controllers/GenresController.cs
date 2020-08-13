using System.Threading.Tasks;
using CoreLibraryApi.Infrastructure.Interfaces;
using CoreLibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLibraryApi.Controllers
{
    [Route("api/genres")]
    [ApiController]
    [Authorize]
    public class GenresController : ControllerBase
    {
        private readonly IGenericRepository<Genre> _repository;

        public GenresController(IGenericRepository<Genre> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var genres = _repository.GetAll();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            Genre genre = await _repository.GetByIdAsync(id);
            if (genre != null)
            {
                return Ok(genre);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Post(Genre genre)
        {
            await _repository.CreateAsync(genre);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Administrator,Storekeeper")]
        public async Task<IActionResult> Put(Genre genre)
        {
            await _repository.UpdateAsync(genre);
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
