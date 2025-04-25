using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TShop.API.Data;
using TShop.API.DTOs.Responses;
using TShop.API.DTOs.Requests;
using TShop.API.Models;
using Microsoft.EntityFrameworkCore;
using TShop.API.Services;

namespace TShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;
        [HttpGet]
        public IActionResult GetAll() {
            var products = _productService.GetAll();
            return products is null ? NotFound() : Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var product = _productService.Get(p => p.Id == id);
            return product is null? NotFound() : Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromForm]ProductRequest productRequest)
        {
            var addedProduct = _productService.Add(productRequest);
            if (addedProduct is null)
                return BadRequest();
            else
                return CreatedAtAction(nameof(GetById), new { id = addedProduct.Id }, addedProduct.Adapt<ProductResponse>());

        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute]int id)
        {
            return _productService.Remove(id) ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id, [FromForm]ProductUpdateRequest productRequest)
        {
            return _productService.Edit(id, productRequest) ? NoContent() : NotFound();
        }


    }
}
