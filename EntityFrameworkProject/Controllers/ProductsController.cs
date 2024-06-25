using EntityFrameworkProject.Data;
using EntityFrameworkProject.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product) 
        {
            var isProductExist = _appDbContext.Products.Any(existingProduct => existingProduct.Name == product.Name);

            if (isProductExist)
            {
                return Conflict($"The product with name: {product.Name} exists in the DB already");
            }

            _appDbContext.Products.Add(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id) 
        {
            var productToDelete = _appDbContext.Products.FirstOrDefault(product => product.Id == id);

            if (productToDelete is null)
            {
                return NotFound($"No products with ID: {id} have been found");
            }

            _appDbContext.Products.Remove(productToDelete);
            await _appDbContext.SaveChangesAsync();

            return Ok(productToDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product) 
        {
            var productToUpdate = _appDbContext.Products.SingleOrDefault(existingProduct => existingProduct.Name == product.Name);

            if (productToUpdate is null)
            {
                return NotFound($"No products with name: {product.Name} have been found");
            }

            productToUpdate.Description = product.Description;

            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
