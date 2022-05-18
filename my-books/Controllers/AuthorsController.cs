using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorsService _authorService;
        public AuthorsController(AuthorsService authorsService)
        {
            _authorService = authorsService;
        }



        [HttpPost("add-author")]
        public IActionResult Post([FromBody] AuthorVM author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _authorService.AddAuthor(author);
            return Ok();
        }
        [HttpGet("get-author-with-books/{id}")]
        public IActionResult GetAuthorWithBooks(int id)
        {
            var response = _authorService.GetAuthorWithBooks(id);
            return Ok(response);
        }
    }
}
