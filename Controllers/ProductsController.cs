using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashDeals.Data;
using FlashDeals.Data.Entities;

namespace FlashDeals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ProductsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var products = await _dataContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAsync(Products product)
        {
            var currentDate = DateTime.Now;
            int year = currentDate.Year;
            int month = currentDate.Month;
            int day = currentDate.Day;

            product.CreatedAt = DateTime.UtcNow;
            product.ExpireTime = new DateTime(year, month, day, 12, 0, 0);
            _dataContext.Products.Add(product);
            await _dataContext.SaveChangesAsync();
            return Created($"/getProductById?id={product.ProductId}", product);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> PutAsync(Products product)
        {
            var singleProduct = await _dataContext.Products.FindAsync(product.ProductId);
            if(singleProduct == null)
            {
                return Ok("This product does not exist");
            }
            var currentDate = DateTime.Now;
            int year = currentDate.Year;
            int month = currentDate.Month;
            int day = currentDate.Day;

            product.UpdatedAt = DateTime.UtcNow;
            product.ExpireTime = new DateTime(year, month, day, 12, 0, 0);
            _dataContext.Entry(singleProduct).CurrentValues.SetValues(product);
            await _dataContext.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        [Route("{ProductId}")]
        public async Task<IActionResult> DeleteAsync(int ProductId)
        {
            var productToDelete = await _dataContext.Products.FindAsync(ProductId);
            if (productToDelete == null)
            {
                return Ok("Product does not exist");
            }
            _dataContext.Products.Remove(productToDelete);
            await _dataContext.SaveChangesAsync();
            return Ok("Product deleted successfully");
        }
    }
}
