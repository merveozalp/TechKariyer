using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
           var result= _productService.GetList();
            return Ok(result);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id) 
        { 
            var result =_productService.GetById(id);

            return Ok(result);
        }
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.Add(product); return Ok(product);
        }
        [HttpDelete]
        //public IActionResult DeleteById(int id) 
        //{
        //    _productService.Delete(_productService.GetById(id)); return Ok();
        //}
        [HttpPut]
        public IActionResult Update(Product product )
        {
            _productService.Update(product); 
            return Ok();
        }
    }
}
