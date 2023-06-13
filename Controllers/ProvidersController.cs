using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ProvidersController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Providers
        [HttpGet]
        public ActionResult<IEnumerable<Provider>> GetProviders()
        {
            var providers = _context.Providers.ToList();
            if (providers == null)
            {
                return NotFound();
            }

            return Ok(providers);
        }

        // GET: api/Providers/5
        [HttpGet("{id}")]
        public ActionResult<Provider> GetProvider(int id)
        {
            var provider = _context.Providers.Find(id);
            if (provider == null)
            {
                return NotFound();
            }

            return Ok(provider);
        }

        // PUT: api/Providers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutProvider(int id,[FromBody] Provider provider)
        {
            var Prov = _context.Providers.Find(id);

            if (Prov == null)
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
                Prov.Provider1 = provider.Provider1;

                _context.Providers.Update(Prov);
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

        // POST: api/Providers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Provider> PostProvider(Provider provider)
        {
            if (_context.Providers == null)
            {
                return Problem("Entity set 'StoreDbContext.Providers' is null.");
            }

            _context.Providers.Add(provider);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProvider), new { id = provider.Id }, provider);
        }

        // DELETE: api/Providers/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProvider(int id)
        {
            if (_context.Providers == null)
            {
                return NotFound();
            }

            var provider = _context.Providers.Find(id);
            if (provider == null)
            {
                return NotFound();
            }

            _context.Providers.Remove(provider);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Find(id) != null;
        }
    }
}
