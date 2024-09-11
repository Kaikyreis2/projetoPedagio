using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_Pedagio.DAL;

namespace Trabalho_Pedagio.View
{
    internal class Template
    {

        public readonly AssinantesDAL assinantesDAL = new();
        public readonly VeiculosDAL veiculosDAL = new();
        public readonly FuncionariosDAL funcionariosDAL = new();
        public void Inicio()
        {

            Console.Clear();
            Console.WriteLine("Bem-vindo ao sistema de pedágio sem-parar de Nova Friburgo!\n");
            Console.WriteLine("1 - Funcionarios");
            Console.WriteLine("2 - Assinantes");
            Console.WriteLine("3 - Veiculos");
            Console.WriteLine("\n0 - Sair");
            Console.WriteLine("\nDigite o numero da central que deseja operar: ");

            while (true)
            {

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    switch (escolha)
                    {
                        case 0:
                            return;
                        case 1:
                            OptionsTemplate.UserOptions(funcionariosDAL); return;
                        case 2:
                            OptionsTemplate.UserOptions(assinantesDAL); return;
                        case 3:
                            OptionsTemplate.UserOptions(veiculosDAL); return;
                        default: Console.WriteLine("Digite uma opcao valida:"); break;


                    }
                }
                else
                {
                    Console.WriteLine("Opcao invalida, digite novamente:");
                }
            }

        }
    }
}
