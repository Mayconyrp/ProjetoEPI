using Microsoft.EntityFrameworkCore;
using Projeto_DetectEPI.Models;
using System.Security.Cryptography;

namespace ProjetoEPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Adm> Administradores { get; set; }
        public DbSet<CanteiroDeObra> CanteirosDeObra { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<ReconhecimentoEPI> ReconhecimentosEPI { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }


    }
}
