using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trabalho_Pedagio.Models;
using Trabalho_Pedagio.View;

namespace Trabalho_Pedagio.DAL
{
    internal class FuncionariosDAL : IDAL
    {
        private readonly Dictionary<int, Funcionario> Funcionarios = new();
        public void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Central de Criacao de Funcionarios");
            Console.WriteLine("----------------------------------");

            int FuncionarioID = 1;
            foreach (var funcionarioExistente in Funcionarios)
            {
                while (FuncionarioID == funcionarioExistente.Key)
                {
                    FuncionarioID++;
                }
            }


            Console.WriteLine("\nDigite o Nome do funcionario: ");

            string Nome = Console.ReadLine()!;

            Funcionario Funcionario = new(Nome);

            Funcionarios.Add(FuncionarioID, Funcionario);

            Console.Clear();
            Console.WriteLine("Funcionario registrado com sucesso!");
            Thread.Sleep(2000);
            return;

        }
        public void Listar()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Listagem Completa dos funcionarios");
            Console.WriteLine("---------------------------------");
            if (Funcionarios.Count > 0)
            {

                foreach (var funcionario in Funcionarios)
                {
                    Console.WriteLine($"\nID: {funcionario.Key}\n Nome:{funcionario.Value.Nome}");
                }
                Thread.Sleep(4000);

            }
            else
            {
                ProtocoloValorNulo();
            }
        }
        public void Remover()
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("Central do RH");
            Console.WriteLine("-------------------");

            if (Funcionarios.Count > 0)
            {
                foreach (var funcionario in Funcionarios)
                {
                    Console.WriteLine($"ID: {funcionario.Key} - {funcionario.Value.Nome}");
                }

                Console.WriteLine("\nDigite o ID do funcionario que deseja remover: ");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int IdParaRemover))
                    {
                        if (Funcionarios.ContainsKey(IdParaRemover))
                        {
                            Funcionarios.Remove(IdParaRemover);

                            Console.Clear();
                            Console.WriteLine("Funcionario removido com sucesso!");
                            Thread.Sleep(2000);

                            return;
                        }
                        else
                        {
                            Console.WriteLine("FUncionario nao existente, digite outro para remover:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Valor necessita ser um numero, digite novamente:");
                    }
                }
            }
            else
            {
                ProtocoloValorNulo();
            }
        }
        public void Editar()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Central de Edicao de dados do funcionario");
            Console.WriteLine("----------------------------------------");

            if (Funcionarios.Count > 0)
            {
                foreach (var funcionario in Funcionarios)
                {
                    Console.WriteLine($"\nID: {funcionario.Key} - {funcionario.Value.Nome}");
                }

                Console.WriteLine("\nDigite o ID do funcionario que deseja editar: ");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int IdParaEditar))
                    {
                        if (Funcionarios.ContainsKey(IdParaEditar))
                        {
                            Funcionario funcionario = Funcionarios[IdParaEditar];
                            Console.Clear();
                            Console.WriteLine($"Central de Edicao do Funcionario: {funcionario.Nome}");
                            Console.WriteLine("\n1 - Nome");
                            Console.WriteLine("\nDigite o novo Nome:");

                            while (true)
                            {
                                string nomeNovo = Console.ReadLine();
                                if (!string.IsNullOrEmpty(nomeNovo))
                                {
                                    funcionario.Nome = nomeNovo;
                                    Console.Clear();
                                    Console.WriteLine("Dado atualizado com sucesso!!");
                                    Thread.Sleep(2000);
                                    Listar();
                                    return;
                                }
                            }


                        }
                        else
                        {
                            Console.WriteLine("Digite um ID valido:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opcao invalida, Digite um numero:");
                    }
                }
            }
            else
            {
                ProtocoloValorNulo();
            }
        }
        public void ProtocoloValorNulo()
        {
            Console.WriteLine("\nNenhuma informacao no sistema!");
            Thread.Sleep(1000);
            Console.WriteLine("\nVoltando ao Inicio...");
            Thread.Sleep(1000);
        }
    }
}
            
            
        
    

