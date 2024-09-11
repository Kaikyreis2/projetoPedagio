using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabalho_Pedagio.Models
{
    internal class Veiculo(string placa, string tipo, int numeroEixos)
    {
        public string Placa = placa;
        public string Tipo = tipo;
        public int NumeroEixos = numeroEixos;
    }
}
