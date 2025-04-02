using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TShop.API.DTOs.Requests;
using TShop.API.DTOs.Responses;
using TShop.API.Services;
using Mapster;
namespace TShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;

        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _brandService.GetAll();
            return brands is null ? NotFound() : Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _brandService.Get(p => p.Id == id);
            return brand is null ? NotFound() : Ok(brand.Adapt<BrandResponse>());
        }

        [HttpPost("")]
        public IActionResult Create([FromForm] BrandRequest brandRequest)
        {
            var addedBrand = _brandService.Add(brandRequest);
            if (addedBrand is null)
                return BadRequest();
            else
                return CreatedAtAction(nameof(GetById), new { id = addedBrand.Id }, addedBrand.Adapt<BrandResponse>());

        }

        [HttpDelete("{id}")]
        public IActionResult Remove([FromRoute] int id)
        {
            return _brandService.Remove(id) ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromForm] BrandRequest brandRequest)
        {
            return _brandService.Edit(id, brandRequest) ? NoContent() : BadRequest();
        }

    }
}
