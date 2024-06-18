using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoEPI.Context;
using Projeto_DetectEPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmpresasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{empresaId}/detalhes")]
        public async Task<IActionResult> GetEmpresaDetalhes(int empresaId)
        {
            var empresa = await _context.Empresas
                .Include(e => e.CanteirosDeObra)
                    .ThenInclude(c => c.ReconhecimentosEPI)
                        .ThenInclude(r => r.Funcionario)
                .FirstOrDefaultAsync(e => e.ID == empresaId);

            if (empresa == null)
            {
                return NotFound();
            }

            var result = empresa.CanteirosDeObra.Select(c => new
            {
                CanteiroDeObra = c.Nome,
                Funcionarios = c.ReconhecimentosEPI.Select(r => new
                {
                    Funcionario = r.Funcionario.Nome,
                    UsoEPI = r.UsoEPI,
                    DataHora = r.DataHora
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        // Ultimos 10 reconhecimentos com filtro de Empresa.
        [HttpGet("{empresaId}/ultimos-reconhecimentos")]
        public async Task<IActionResult> ObterUltimosReconhecimentos(int empresaId)
        {
            var reconhecimentos = await _context.ReconhecimentosEPI
                .Include(r => r.Funcionario)
                .Where(r => r.Funcionario.CanteiroDeObra.EmpresaID == empresaId)
                .OrderByDescending(r => r.DataHora)
                .Take(10)
                .Select(r => new
                {
                    DataHora = r.DataHora,
                    Funcionario = r.Funcionario.Nome,
                    UsoEPI = r.UsoEPI
                })
                .ToListAsync();

            if (reconhecimentos == null || !reconhecimentos.Any())
            {
                return NotFound();
            }

            return Ok(reconhecimentos);
        }

        // Novo endpoint para obter detalhes dos funcionários
        [HttpGet("{empresaId}/funcionarios")]
        public async Task<IActionResult> ObterFuncionarios(int empresaId)
        {
            var funcionarios = await _context.Funcionarios
                .Include(f => f.CanteiroDeObra)
                .Where(f => f.CanteiroDeObra.EmpresaID == empresaId)
                .Select(f => new
                {
                    CanteiroDeObra = f.CanteiroDeObra.Nome,
                    Funcionario = f.Nome,
                    Setor = f.Cargo // Assumindo que o campo "Cargo" representa o setor do funcionário
                })
                .ToListAsync();

            if (funcionarios == null || !funcionarios.Any())
            {
                return NotFound();
            }

            return Ok(funcionarios);
        }
    }
}
