using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

namespace Projeto_DetectEPI.Models
{
    public class Empresa
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }

        public ICollection<Adm> Administradores { get; set; }
        public ICollection<CanteiroDeObra> CanteirosDeObra { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; }
        public ICollection<Notificacao> Notificacoes { get; set; }
    }
}
