using Microsoft.AspNetCore.Mvc;

using AppCrud.Data;
using AppCrud.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AppCrud.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : Controller
    {
         private readonly AppDBContext _appDbContext;
         public ProductController(AppDBContext appDBContext)
         {
             _appDbContext = appDBContext;
         }

        [HttpGet("Categories")]
        public async Task<ActionResult<IEnumerable<Feature>>> GetFeatures()
        {
            /* var categories = await _appDbContext.Features.Include(c => c.Products).ToListAsync();

             var jsonSerializerOptions = new JsonSerializerOptions
             {
                 ReferenceHandler = ReferenceHandler.Preserve
             };

             var categoriasJson = JsonSerializer.Serialize(categories, jsonSerializerOptions);

             return Content(categoriasJson, "application/json");*/
            return await _appDbContext.Features.ToListAsync();
        }

        // Obtener todos los productos
        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        // Obtener un producto por ID
        [HttpGet("Products/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var producto = await _appDbContext.Products.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;

        }

        [HttpPost("{productId}/wishes")]
public async Task<IActionResult> AddWishes(int productId, [FromBody] AddWishRequest request)
{
    if (request == null)
    {
        return BadRequest("La solicitud es inválida.");
    }

    var producto = await _appDbContext.Products.FindAsync(productId);
    var usuario = await _appDbContext.Users.FindAsync(request.UserId);

    if (producto == null || usuario == null)
    {
        return NotFound();
    }

    // Verificar si el producto ya está en la lista de deseos
    var productoDeseadoExistente = await _appDbContext.WishProducts
        .FirstOrDefaultAsync(pd => pd.IdProduct == productId && pd.IdUser == request.UserId);

    if (productoDeseadoExistente != null)
    {
        return BadRequest("El producto ya está en la lista de deseos.");
    }

    var productoDeseado = new WishProduct
    {
        Product = producto,
        User = usuario
    };

    _appDbContext.WishProducts.Add(productoDeseado);
    await _appDbContext.SaveChangesAsync();

    return Ok();
}


        [HttpDelete("{productoId}/wishes")]
        public async Task<IActionResult> DeleteWishes(int productoId, int usuarioId)
        {
            var productoDeseado = await _appDbContext.WishProducts
                .FirstOrDefaultAsync(pd => pd.IdProduct == productoId && pd.IdUser == usuarioId);

            if (productoDeseado == null)
            {
                return NotFound("El producto no está en la lista de deseos.");
            }

            _appDbContext.WishProducts.Remove(productoDeseado);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }

        // Obtener los productos deseados de un usuario
        [HttpGet("{usuarioId}/wishes")]
        public async Task<ActionResult<IEnumerable<Product>>> GetWishes(int usuarioId)
        {
            var usuario = await _appDbContext.Users
                .Include(u => u.WishProducts)
                .ThenInclude(pd => pd.Product)
                .FirstOrDefaultAsync(u => u.IdUser == usuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario.WishProducts.Select(pd => pd.Product).ToList();
        }
    }

    public class AddWishRequest
    {
        public int UserId { get; set; }
    }
}
