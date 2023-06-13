using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public ClientsController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            var clients = _context.Clients.ToList();
            if (clients == null)
            {
                return NotFound();
            }

            return Ok(clients);
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutClient(int id,[FromBody] Client client)
        {
            var cliente = _context.Clients.Find(id);

            if (cliente == null)
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
                cliente.Name = client.Name;
                cliente.Document = client.Document;
                cliente.Email = client.Email;
                cliente.Phone = client.Phone;
                cliente.Address = client.Address;
                cliente.Dob = client.Dob;

                _context.Clients.Update(cliente);
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Client> PostClient(Client client)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'StoreDbContext.Clients' is null.");
            }

            _context.Clients.Add(client);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }

            var client = _context.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Find(id) != null;
        }
    }
}
