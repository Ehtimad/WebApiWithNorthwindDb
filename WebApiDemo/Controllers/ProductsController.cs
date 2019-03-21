using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.DataAccess;
using WebApiDemo.Entities;

namespace WebApiDemo.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        IProductDal _productDal;
        public ProductsController(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _productDal.GetList();
                if (products == null)
                {
                    return NotFound("There is no product");
                }
                return Ok(products);
            }
            catch { }
            return BadRequest();
        }

        [HttpGet("{productId}")]
        public IActionResult Get(int productId)
        {
            try
            {
                var product = _productDal.Get(x => x.ProductId == productId);
                if (product == null)
                {
                    return NotFound($"There is no product with id = {productId}");
                }
                return Ok(product);
            }
            catch { }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound($"There is no added product with name = {product.ProductName}");
                }
                _productDal.Add(product);
                return Ok(product);
            }
            catch { }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Put([FromBody]Product product)
        {
            try
            {
                if (product == null)
                {
                    return NotFound($"There is no update product with name = {product.ProductId}");
                }
                _productDal.Update(product);
                return Ok(product);
            }
            catch { }
            return BadRequest();
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            try
            {
                if (productId == null)
                {
                    return NotFound($"There is no product with id = {productId}");
                }
                _productDal.Delete(new Product { ProductId = productId });
                return Ok($"Product Deleted ID: {productId}");
            }
            catch { }
            return BadRequest();
        }

        [HttpGet("GetProductDetails")]
        public IActionResult GetProductsWidthDetails()
        {
            try
            {
                var result = _productDal.GetProductsWitdhDetails();
                if (result == null)
                {
                    return NotFound("There is no product");
                }
                return Ok(result);
            }
            catch { }
            return BadRequest();
        }
    }
}
