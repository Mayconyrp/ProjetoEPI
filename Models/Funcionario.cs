namespace Projeto_DetectEPI.Models
{
    public class Funcionario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }

        public int CanteiroDeObraID { get; set; } // Alterado
        public CanteiroDeObra? CanteiroDeObra { get; set; } // Alterado

        public ICollection<ReconhecimentoEPI> ReconhecimentosEPI { get; set; }
    }
}
