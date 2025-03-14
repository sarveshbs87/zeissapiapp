using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ZeissAPIApp.Data;
using ZeissAPIApp.Models;

namespace ZeissAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly ProductsDBContext _productsDBContext;

        public ProductsController(ProductsDBContext productsDBContext)
        {
            this._productsDBContext = productsDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductDetails()
        {
            var result = await _productsDBContext.products.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductDetails([FromBody] Products product)
        {
            product.Id = Products.GenerateUniqueId();
            await _productsDBContext.products.AddAsync(product);
            await _productsDBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductDetailsById), new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProductDetails([FromBody] Products product, string id)
        {
            var result = await _productsDBContext.products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            result.Name = product.Name;
            result.Price = product.Price;
            result.StockAvailable = product.StockAvailable;

            await _productsDBContext.SaveChangesAsync();
            return Ok();

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProductDetails(string id)
        {
            var result = await _productsDBContext.products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _productsDBContext.products.Remove(result);
            await _productsDBContext.SaveChangesAsync();
            return Ok();

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductDetailsById(string id)
        {
            var result = await _productsDBContext.products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }


            return Ok(result);

        }
        //test
        [HttpPut]
        [Route("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(string id, int quantity)
        {
            var result = await _productsDBContext.products.FindAsync(id);
            if (result == null || result.StockAvailable < quantity)
            {
                return BadRequest();
            }

            result.StockAvailable -= quantity;
            await _productsDBContext.SaveChangesAsync();
            return Ok();

        }

        [HttpPut]
        [Route("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(string id, int quantity)
        {
            var result = await _productsDBContext.products.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            result.StockAvailable += quantity;
            await _productsDBContext.SaveChangesAsync();
            return Ok();
        }
    }

}
