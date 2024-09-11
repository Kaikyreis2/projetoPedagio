using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trabalho_Pedagio.Models;
using Trabalho_Pedagio.View;

namespace Trabalho_Pedagio.DAL
{
    internal class AssinantesDAL : IDAL
    {
        public Dictionary<int, Assinante> Assinantes = new();

        public void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Central de Criação de Assinante");
            Console.WriteLine("--------------------------------");

            int AssinanteID = 1;

            while (Assinantes.ContainsKey(AssinanteID))
            {
                AssinanteID++;
            }

            Console.WriteLine("\nDigite o Nome do assinante: ");
            string Nome = Console.ReadLine()!;

            while (true)
            {
                Console.WriteLine("\nDigite o CPF do assinante: ");
                string inputCpf = Console.ReadLine()!;
                string CpfFormatado = Regex.Replace(inputCpf, @"\D", "");

                if (ValidadorCPF(CpfFormatado))
                {
                    long Cpf = long.Parse(CpfFormatado);
                    Assinante assinante = new(Nome, Cpf);
                    Assinantes.Add(AssinanteID, assinante);

                    Console.Clear();
                    Console.WriteLine("Assinante registrado com sucesso!");
                    Thread.Sleep(2000);

                    return;
                }
                else
                {
                    Console.WriteLine("Por favor, digite um CPF válido.");
                }
            }
        }
        public void Listar()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Listagem Completa dos assinantes");
            Console.WriteLine("---------------------------------");
            if (Assinantes.Count > 0)
            {

                foreach (var assinante in Assinantes)
                {
                    Console.WriteLine($"\nID: {assinante.Key}\nNome: {assinante.Value.Nome}\nCPF: {assinante.Value.Cpf}\nNivel: {assinante.Value.GrauAssinatura}");
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
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Central de cancelamento de assinatura");
            Console.WriteLine("--------------------------------------");

            if (Assinantes.Count > 0)
            {
                foreach (var assinante in Assinantes)
                {
                    Console.WriteLine($"ID: {assinante.Key} - {assinante.Value.Nome}");
                }

                Console.WriteLine("\nDigite o ID do assinante que deseja remover: ");

                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int IdParaRemover))
                    {
                        if (Assinantes.ContainsKey(IdParaRemover))
                        {
                            Assinantes.Remove(IdParaRemover);

                            Console.Clear();
                            Console.WriteLine("Assinante removido com sucesso!");
                            Thread.Sleep(2000);
                            
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Assinante nao existente, digite outro para remover:");
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
            Console.WriteLine("Central de Edicao de dados do assinante");
            Console.WriteLine("----------------------------------------");

            if (Assinantes.Count > 0)
            {
                foreach (var assinante in Assinantes)
                {
                    Console.WriteLine($"\nID: {assinante.Key} - {assinante.Value.Nome}");
                }

                Console.WriteLine("\nDigite o ID do assinante que deseja editar: ");
                while (true)
                {
                    if (int.TryParse(Console.ReadLine(), out int IdParaEditar))
                    {
                        if (Assinantes.ContainsKey(IdParaEditar))
                        {
                            Assinante assinante = Assinantes[IdParaEditar];
                            Console.Clear();
                            Console.WriteLine($"Central de Edicao do Assinante: {assinante.Nome}");
                            Console.WriteLine("\n1 - Nome");
                            Console.WriteLine("2 - CPF");
                            Console.WriteLine("3 - Grau de assinatura");
                            Console.WriteLine("\nDigite o numero da opcao que deseja mudar:");


                            while (true)
                            {
                                if (int.TryParse(Console.ReadLine(), out int opcaoEscolhida))
                                {
                                    if (opcaoEscolhida >= 1 && opcaoEscolhida <= 3)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Digite o novo Dado: ");

                                        var novoValor = Console.ReadLine();

                                        switch (opcaoEscolhida)
                                        {
                                            case 1:
                                                if (!string.IsNullOrEmpty(novoValor))
                                                {
                                                    assinante.Nome = novoValor;
                                                    Console.Clear();
                                                    Console.WriteLine("Dado atualizado com sucesso!!");
                                                    Thread.Sleep(2000);
                                                    Listar();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("O nome não pode ser vazio.");
                                                }
                                                return;

                                            case 2:
                                                string cpfFormated = Regex.Replace(novoValor, @"\D", "");
                                                if (ValidadorCPF(cpfFormated))
                                                {

                                                    assinante.Cpf = long.Parse(cpfFormated) ;
                                                    Console.Clear();
                                                    Console.WriteLine("Dado atualizado com sucesso!!");
                                                    Thread.Sleep(2000);
                                                    Listar();

                                                }
                                                else
                                                {
                                                    Console.WriteLine("CPF invalido!");
                                                }
                                                return;
                                            case 3:
                                                if (!string.IsNullOrEmpty(novoValor))
                                                {
                                                    assinante.GrauAssinatura = novoValor;
                                                    Console.Clear();
                                                    Console.WriteLine("Dado atualizado com sucesso!!");
                                                    Thread.Sleep(2000);
                                                    Listar();
                                                }
                                                else
                                                {
                                                    Console.WriteLine("O grau de assinatura não pode ser vazio.");
                                                }
                                                return;

                                            default:
                                                Console.WriteLine("Opção inválida.");
                                                break;
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("Digite um valor existente:");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Digite um valor valido:");
                                }

                            }
                        }
                        else
                        {
                            Console.WriteLine("Digite um ID existente:");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Digite um ID valido:");
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
        public Assinante GetAssinante(int id)
        {
            if (Assinantes.ContainsKey(id))
            {
                return Assinantes[id];
            }

            return null;
        }
        bool ValidadorCPF(string cpf)
        {
            bool IsValid = true;

            if (cpf.Length == 11 && long.TryParse(cpf, out long Cpf))
            {

                foreach (var assinante in Assinantes.Values)
                {
                    if (assinante.Cpf.Equals(Cpf))
                    {
                        Console.WriteLine("\nCPF já existente!");
                        IsValid = false;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\nCPF incorreto!");
                IsValid = false;
            }

            return IsValid;
        }



    }
}