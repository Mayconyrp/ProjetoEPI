using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEPI.Context;
using Projeto_DetectEPI.Models;

namespace ProjetoEPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdmsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdmsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adm>>> GetAdms()
        {
            var adms = await _context.Administradores
                .Include(a => a.Empresa)
                .ToListAsync();
            return Ok(adms);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Adm>> GetAdm(int id)
        {
            var adm = await _context.Administradores
                .Include(a => a.Empresa)
                .FirstOrDefaultAsync(a => a.ID == id);

            if (adm == null)
            {
                return NotFound();
            }

            return Ok(adm);
        }

        [HttpPost]
        public async Task<ActionResult<Adm>> CreateAdm([FromBody] Adm adm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Administradores.Add(adm);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdm), new { id = adm.ID }, adm);
        }

    }
}
