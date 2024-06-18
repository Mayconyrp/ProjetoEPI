namespace Projeto_DetectEPI.Models
{
    public class Adm
    {
            public int ID { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }

            public int EmpresaID { get; set; }  
            public Empresa Empresa { get; set; }

            //public ICollection<Notificacao> Notificacoes { get; set; }
        }

}
