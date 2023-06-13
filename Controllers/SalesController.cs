using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public SalesController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public ActionResult<IEnumerable<Sale>> GetSales()
        {
            var sales = _context.Sales.ToList();
            if (sales == null)
            {
                return NotFound();
            }

            return Ok(sales);
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public ActionResult<Sale> GetSale(int id)
        {
            var sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(sale);
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutSale(int id,[FromBody] Sale sale)
        {
            var venta = _context.Sales.Find(id);

            if (venta == null)
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
                venta.Code = sale.Code;
                venta.Total = sale.Total;
                venta.SaleDate = sale.SaleDate;
                venta.UserId = sale.UserId;
                venta.ClientId = sale.ClientId;

                _context.Sales.Update(venta);
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Sale> PostSale(Sale sale)
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'StoreDbContext.Sales' is null.");
            }

            _context.Sales.Add(sale);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetSale), new { id = sale.Id }, sale);
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            if (_context.Sales == null)
            {
                return NotFound();
            }

            var sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            _context.SaveChanges();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Find(id) != null;
        }
    }
}
