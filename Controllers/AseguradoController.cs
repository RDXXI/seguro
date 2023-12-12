using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seguros.Interfaces;
using Seguros.Migrations;
using Seguros.Services;

namespace Seguros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICargaMasivaService _cargaMasivaService;

        public AseguradoController(ApplicationDbContext context, ICargaMasivaService cargaMasivaService)
        {
            _context = context;
            _cargaMasivaService = cargaMasivaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modelo.Asegurado>>> GetSeguros()
        {
            return await _context.Asegurados.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Modelo.Asegurado>> GetSeguro(int id)
        {
            var seguro = await _context.Asegurados.FindAsync(id);

            if (seguro == null)
            {
                return NotFound();
            }

            return seguro;
        }

        [HttpPost]
        public async Task<ActionResult<Modelo.Asegurado>> PostSeguro(Modelo.Asegurado asegurado)
        {
            _context.Asegurados.Add(asegurado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeguro", new { id = asegurado.Id }, asegurado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeguro(int id, Modelo.Asegurado asegurado)
        {
            if (id != asegurado.Id)
            {
                return BadRequest();
            }

            _context.Entry(asegurado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeguroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeguro(int id)
        {
            var seguro = await _context.Asegurados.FindAsync(id);
            if (seguro == null)
            {
                return NotFound();
            }

            _context.Asegurados.Remove(seguro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SeguroExists(int id)
        {
            return _context.Seguros.Any(e => e.Id == id);
        }

        [HttpPost("cargamasiva")]
        public async Task<IActionResult> CargarAseguradosDesdeArchivo(/*[FromForm]*/ IFormFile archivo)
        {
            try
            {
                var aseguradosCargados = await _cargaMasivaService.CargarAseguradosDesdeArchivoAsync(archivo);
                return Ok(aseguradosCargados);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al cargar asegurados: {ex.Message}");
            }
        }
    }
}