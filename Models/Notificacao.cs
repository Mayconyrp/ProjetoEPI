using System.Runtime.Intrinsics.Arm;

namespace Projeto_DetectEPI.Models
{
    public class Notificacao
    {
        public int ID { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }

        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }

        public int AdministradorID { get; set; }
        public Adm Administrador { get; set; }

        public int ReconhecimentoFacialID { get; set; }
        public ReconhecimentoEPI ReconhecimentoFacial { get; set; }
    }
}
