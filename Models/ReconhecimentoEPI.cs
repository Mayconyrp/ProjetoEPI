namespace Projeto_DetectEPI.Models
{
    public class ReconhecimentoEPI
    {
        public int ID { get; set; }
        public DateTime DataHora { get; set; }

        public int FuncionarioID { get; set; }
        public Funcionario Funcionario { get; set; }
        public bool UsoEPI { get; set; }

        public int CanteiroID { get; set; } 
        public CanteiroDeObra Canteiro { get; set; }

    }
}
