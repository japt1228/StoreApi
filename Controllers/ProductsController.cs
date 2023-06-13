using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ProductsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _context.Products.ToList();
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id,[FromBody] Product product)
        {
            var producto = _context.Products.Find(id);

            if (producto == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "HTTP 400",
                    message = "No es posible editar los datos",
                    detail = ""
                });
            }

            try
            {
                producto.Name = product.Name;
                producto.Code = product.Code;
                producto.Description = product.Description;
                producto.Stock = product.Stock;
                producto.PurchasePrice = product.PurchasePrice;
                producto.SalePrice = product.SalePrice;
                producto.ProviderId = product.ProviderId;
                producto.CategoryId = product.CategoryId;

                _context.Products.Update(producto);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, new
                {
                    code = "OK",
                    message = "Datos modificados exitosamente",
                    detail = ""
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al modificar",
                    detail = ex.Message
                });
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Product> PostProduct(Product product)
        {
            // Verificar si la categoría existe
            var category = _context.Categories.Find(product.CategoryId);
            if (category == null)
            {
                return BadRequest("Categoría no encontrada.");
            }

            // Verificar si el proveedor existe
            var provider = _context.Providers.Find(product.ProviderId);
            if (provider == null)
            {
                return BadRequest("Proveedor no encontrado.");
            }

            // Crear el nuevo producto
            var newProduct = new Product
            {
                Name = product.Name,
                Code = product.Code,
                Description = product.Description,
                Stock = product.Stock,
                PurchasePrice = product.PurchasePrice,
                SalePrice = product.SalePrice,
                CategoryId = product.Id,
                ProviderId = product.Id,
                Category = category,
                Provider = provider
            };

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return Ok("Producto creado exitosamente.");
        }
    

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Find(id) != null;
        }
    }
}
