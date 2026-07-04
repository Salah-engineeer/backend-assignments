using BooksApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAll()
        {
            return Ok(_booksService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var book = _booksService.GetById(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
    }
}