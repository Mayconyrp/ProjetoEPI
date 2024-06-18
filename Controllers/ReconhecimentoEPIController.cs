using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_DetectEPI.Models;
using ProjetoEPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_DetectEPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReconhecimentoEPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReconhecimentoEPIController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("total-reconhecimentos-por-dia-semana/{empresaId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalReconhecimentosPorDiaSemana(int empresaId)
        {
            DateTime dataInicio = DateTime.Now.AddDays(-7); // Data de hoje menos 7 dias
            DateTime dataFim = DateTime.Now; // Data de hoje

            var reconhecimentosPorDia = await _context.ReconhecimentosEPI
                .Where(r => r.Funcionario.CanteiroDeObra.EmpresaID == empresaId &&
                            r.DataHora >= dataInicio && r.DataHora <= dataFim)
                .GroupBy(r => new { Dia = r.DataHora.Date })
                .Select(g => new
                {
                    Data = g.Key.Dia,
                    TotalReconhecimentos = g.Count()
                })
                .ToListAsync();

            var empresa = await _context.Empresas
                .Where(e => e.ID == empresaId)
                .Select(e => new { Nome = e.Nome })
                .FirstOrDefaultAsync();

            if (empresa == null)
            {
                return NotFound();
            }

            var result = new
            {
                Empresa = empresa.Nome,
                TotalReconhecimentosPorDia = reconhecimentosPorDia
            };

            return Ok(result);
        }

        [HttpGet("total-reconhecimentos-por-dia-semana/{empresaId}/{canteiroId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalReconhecimentosPorDiaSemana(int empresaId, int canteiroId)
        {
            DateTime dataInicio = DateTime.Now.AddDays(-7); // Data de hoje menos 7 dias
            DateTime dataFim = DateTime.Now; // Data de hoje

            var reconhecimentosPorDia = await _context.ReconhecimentosEPI
                .Where(r => r.Funcionario.CanteiroDeObra.EmpresaID == empresaId &&
                            r.CanteiroID == canteiroId &&
                            r.DataHora >= dataInicio && r.DataHora <= dataFim)
                .GroupBy(r => new { Dia = r.DataHora.Date })
                .Select(g => new
                {
                    Data = g.Key.Dia,
                    TotalReconhecimentos = g.Count()
                })
                .ToListAsync();

            var canteiro = await _context.CanteirosDeObra
                .Where(c => c.ID == canteiroId)
                .Select(c => new { Nome = c.Nome })
                .FirstOrDefaultAsync();

            if (canteiro == null)
            {
                return NotFound();
            }

            var result = new
            {
                Canteiro = canteiro.Nome,
                TotalReconhecimentosPorDia = reconhecimentosPorDia
            };

            return Ok(result);
        }
    }
}
