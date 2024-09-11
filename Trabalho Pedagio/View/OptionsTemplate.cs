using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_Pedagio.DAL;


namespace Trabalho_Pedagio.View
{
    internal class OptionsTemplate
    {
        static public void UserOptions(IDAL dal)
        {
            Template template = new Template();
            while (true)
            {
                Console.Clear();
                
                Console.WriteLine("-------------------------");
                Console.WriteLine("Central de Gerenciamento");
                Console.WriteLine("-------------------------\n");

                Console.WriteLine("1 - Adicionar");
                Console.WriteLine("2 - Remover");
                Console.WriteLine("3 - Editar");
                Console.WriteLine("4 - Listar");
                Console.WriteLine("\n5 - Menu Principal");
                Console.WriteLine("0 - Encerrar");
                Console.WriteLine("\nDigite a opcao desejada: ");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    switch (escolha)
                    {
                        case 0:
                            return;
                        case 1:
                            dal.Adicionar();
                            break;
                        case 2:
                            dal.Remover();
                            break;
                        case 3:
                            dal.Editar();
                            break;
                        case 4:
                            dal.Listar();
                            break;
                        case 5:
                            template.Inicio();
                            break;
                        default:
                            Console.WriteLine("Opcao Inexistente!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Entrada inválida!");
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey(); 
            }
        }

    }
}
