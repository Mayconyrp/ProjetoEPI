using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_DetectEPI.Models;
using ProjetoEPI.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoEPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CanteirosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CanteirosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{canteiroId}/ultimos-reconhecimentos")]
        public async Task<ActionResult<IEnumerable<object>>> GetUltimosReconhecimentosCanteiro(int canteiroId)
        {
            var reconhecimentos = await _context.ReconhecimentosEPI
                .Include(r => r.Funcionario)
                .Where(r => r.CanteiroID == canteiroId)
                .OrderByDescending(r => r.DataHora)
                .Take(10)
                .Select(r => new
                {
                    FuncionarioNome = r.Funcionario.Nome,
                    UsoEPI = r.UsoEPI,
                    DataReconhecimento = r.DataHora
                })
                .ToListAsync();

            if (reconhecimentos == null || reconhecimentos.Count == 0)
            {
                return NotFound();
            }

            return Ok(reconhecimentos);
        }
        [HttpGet("{empresaId}/{canteiroId}/funcionarios-sem-epi")]
        public async Task<ActionResult<IEnumerable<object>>> GetFuncionariosSemEPI(int empresaId, int canteiroId)
        {
            var funcionariosSemEPI = await _context.ReconhecimentosEPI
                .Where(r => r.Funcionario.CanteiroDeObra.EmpresaID == empresaId &&
                            r.Funcionario.CanteiroDeObraID == canteiroId &&
                            !r.UsoEPI)
                .OrderByDescending(r => r.DataHora)
                .Select(r => new
                {
                    NomeCanteiro = r.Funcionario.CanteiroDeObra.Nome,
                    NomeFuncionario = r.Funcionario.Nome,
                    UsoEPI = r.UsoEPI,
                    DataHoraReconhecimento = r.DataHora
                })
                .ToListAsync();

            if (funcionariosSemEPI == null || funcionariosSemEPI.Count == 0)
            {
                return NotFound("Não foram encontrados funcionários sem EPI para o canteiro especificado.");
            }

            return Ok(funcionariosSemEPI);
        }

    }
}