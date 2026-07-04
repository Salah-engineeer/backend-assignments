using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("")]
        public ActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult GetById(int id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        public ActionResult Add(Product product)
        {
            var newProduct = _productService.Add(product);

            return StatusCode(201, newProduct);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Product product)
        {
            var updatedProduct = _productService.Update(id, product);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = _productService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}