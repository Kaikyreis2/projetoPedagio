using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_Pedagio.View;

namespace Trabalho_Pedagio.DAL
{
    public interface IDAL
    {
        void Adicionar();
        void Listar();
        void Remover();
        void Editar();
    }
}
