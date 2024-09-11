using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Pedagio.Models
{
    internal class Assinante
    {
        public string Nome;
        public long Cpf;
        public string GrauAssinatura;
        public List<Veiculo> Veiculos;
        public Assinante(string nome, long cpf)
        {
            Nome = nome;
            Cpf = cpf;
            GrauAssinatura = "Bronze";
            Veiculos = new();
        }
    }
}
