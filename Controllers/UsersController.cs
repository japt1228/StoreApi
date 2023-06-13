using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Models;
using System.Collections.Generic;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StoreDbContext _context;

        public UsersController(StoreDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        // Recupera todos los usuarios de la base de datos.
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            // Recupera la lista de usuarios desde la base de datos.
            var users = _context.Users.ToList();
            if (users == null)
            {
                // Si la lista de usuarios es nula, devuelve una respuesta NotFound()
                // para indicar que no se encontraron usuarios en la base de datos.
                return NotFound();
            }

            // Si la lista de usuarios no es nula, devuelve una respuesta Ok(users).
            // Esto indica que se encontraron usuarios en la base de datos y se devuelve
            // la lista de usuarios como respuesta exitosa.
            return Ok(users);
        }

        // GET: api/Users/5
        // Recupera un usuario específico por su ID desde la base de datos.
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            // Busca el usuario en la base de datos por su ID.
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Actualiza un usuario existente con el ID proporcionado.
        [HttpPut("{id}")]
        public IActionResult PutUser(int id,[FromBody] User user)
        {
            // Busca el usuario existente en la base de datos por su ID.
            var usuario = _context.Users.Find(id);

            if (usuario == null)
            {
                // Devuelve un código de estado 400 si el usuario no existe.
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    code = "HTTP 400",
                    message = "No es posible editar los datos",
                    detail = ""
                });
            }

            try
            {
                // Actualiza las propiedades del usuario con los datos proporcionados.
                usuario.Name = user.Name;
                usuario.UserName = user.UserName;
                usuario.Password = user.Password;
                usuario.Role = user.Role;

                // Actualiza el usuario en la base de datos.
                _context.Users.Update(usuario);
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
                // Maneja cualquier excepción y devuelve un código de estado 200 con detalles del error.
                return StatusCode(StatusCodes.Status200OK, new
                {
                    code = "OK",
                    message = "Error al modificar",
                    detail = ex.Message
                });
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Crea un nuevo usuario y lo agrega a la base de datos.

        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (_context.Users == null)
            {
                // Devuelve un problema si el conjunto de entidades 'StoreDbContext.Users' es nulo.
                return Problem("Entity set 'StoreDbContext.Users' is null.");
            }

            // Agrega el usuario a la base de datos.
            _context.Users.Add(user);
            _context.SaveChanges();

            // Devuelve una respuesta de creación con el usuario creado.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        // Elimina un usuario con el ID especificado de la base de datos.
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                // Devuelve un código de estado 404 si el conjunto de entidades es nulo.
                return NotFound();
            }

            // Busca el usuario en la base de datos por su ID.
            var user = _context.Users.Find(id);
            if (user == null)
            {
                // Devuelve un código de estado 404 si el usuario no existe.
                return NotFound();
            }

            // Elimina el usuario de la base de datos.
            _context.Users.Remove(user);
            _context.SaveChanges();

            // Devuelve un código de estado sin contenido para indicar éxito en la eliminación.
            return NoContent();
        }

        private bool UserExists(int id)
        {
            // Verifica si un usuario existe en la base de datos por su ID.
            return _context.Users.Find(id) != null;
        }
    }
}
