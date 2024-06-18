namespace Projeto_DetectEPI.Models
{
    public class CanteiroDeObra
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Localizacao { get; set; }

        public int EmpresaID { get; set; }
        public Empresa Empresa { get; set; }

        public ICollection<ReconhecimentoEPI> ReconhecimentosEPI { get; set; }
        public ICollection<Funcionario> Funcionarios { get; set; } 
    }
}
