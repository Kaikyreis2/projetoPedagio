using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using Trabalho_Pedagio.Models;
using Trabalho_Pedagio.View;

namespace Trabalho_Pedagio.DAL
{
    internal class VeiculosDAL : IDAL
    {
        private readonly AssinantesDAL assinantesDAL = new() ;

        public VeiculosDAL()
        {
            assinantesDAL.Assinantes = new Dictionary<int, Assinante>
            {
                { 1, new Assinante("Lucas", 13223223232) },
                { 2, new Assinante("Gabriel", 23232211223) },
                { 3, new Assinante("Joao", 29232393242) }
            };

        }
        public void Adicionar()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Central de Criacao de veiculo");
            Console.WriteLine("--------------------------------");


            foreach (var assinante in assinantesDAL.Assinantes)
            {
                Console.WriteLine($"{assinante.Key} - {assinante.Value.Nome}");
            }

            Console.WriteLine("Digite o numero do ID do assinante que deseja adicionar o veiculo: ");

            var veiculos = assinantesDAL.Assinantes
                            .SelectMany(a => a.Value.Veiculos)
                            .ToList();

            while (true)
            {
                if(int.TryParse(Console.ReadLine(), out int assinanteTargetId))
                {
                    Assinante assinanteTarget = assinantesDAL.GetAssinante(assinanteTargetId);

                    if (assinanteTarget != null)
                    {
                        string veiculoTipo = GetVeiculoType();

                        int veiculoEixo = GetVeiculosEixo(veiculoTipo);

                        Console.Clear();
                        Console.WriteLine("Digite a Placa do Veiculo");


                        while (true)
                        {
                            string veiculoPlaca = Console.ReadLine()!;

                            if (ValidadorPlaca(veiculoPlaca, assinantesDAL))
                            {
                                Veiculo veiculo = new(veiculoPlaca, veiculoTipo, veiculoEixo);
                                assinanteTarget.Veiculos.Add(veiculo);

                                Console.Clear();
                                Console.WriteLine("Veiculo adicionado com sucesso!!");
                                Thread.Sleep(2000);
                                return;
                            }
                        }  
                    }
                    else
                    {
                        Console.WriteLine("Digite o ID de um Assinante existente");
                    }

                }
                else
                {
                    Console.WriteLine("Opcao invalida, digite um numero:");
                }
                

            }
           
        }
        bool ValidadorPlaca(string placa, AssinantesDAL assinantesDAL)
        {
            string placaParameterFormated = placa.ToUpper().Trim();

            
            var veiculos = assinantesDAL.Assinantes
                         .SelectMany(a => a.Value.Veiculos)
                         .ToList();

            if (placaParameterFormated.Length == 7)
            {
                
                foreach (var veiculo in veiculos)
                {
                    string placaVeiculoFormated = veiculo.Placa.ToUpper().Trim();

                    if (placaVeiculoFormated.Equals(placaParameterFormated))
                    {
                        Console.WriteLine("Placa já existente");
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Placa incorreta, sistema de referência: MERCOSUL");
                return false;
            }

            return true;
        }

        public void Listar()
        {
            Console.Clear();
            Console.WriteLine("------------------------");
            Console.WriteLine("Listagem dos Veiculos");
            Console.WriteLine("------------------------");

            var veiculos = assinantesDAL.Assinantes
                            .SelectMany(a => a.Value.Veiculos)
                            .ToList();

            if(veiculos.Count > 0)
            {
                foreach (var assinante in assinantesDAL.Assinantes)
                {
                    
                    foreach (var veiculo in assinante.Value.Veiculos)
                    {
                        Console.WriteLine($"Proprietário: {assinante.Value.Nome}");
                        Console.WriteLine($"Tipo: {veiculo.Tipo}");
                        Console.WriteLine($"Eixos: {veiculo.NumeroEixos}");
                        Console.WriteLine($"Placa: {veiculo.Placa}");
                        Console.WriteLine("----------------------------------");
                    }
                }
            }
            else
            {
                ProtocoloValorNulo();
            }
        }
        public void Remover()
        {
            Console.Clear ();
            Console.WriteLine("------------------------------");
            Console.WriteLine("Central de remocao de veiculos");
            Console.WriteLine("------------------------------");

            var veiculos = assinantesDAL.Assinantes
                           .SelectMany(a => a.Value.Veiculos)
                           .ToList();

            if (veiculos.Count > 0)
            {
                foreach (var assinante in assinantesDAL.Assinantes)
                {

                    foreach (var veiculo in assinante.Value.Veiculos)
                    {
                        Console.WriteLine($"Proprietário: {assinante.Value.Nome}");
                        Console.WriteLine($"Tipo: {veiculo.Tipo}");
                        Console.WriteLine($"Eixos: {veiculo.NumeroEixos}");
                        Console.WriteLine($"Placa: {veiculo.Placa}");
                        Console.WriteLine("----------------------------------");
                    }
                }

                Console.WriteLine("Digite a placa do veiculo que deseja remover: ");
                while (true)
                {
                    string placa = Console.ReadLine();
                    Veiculo veiculo = VeiculoPorPlaca(placa, assinantesDAL);

                    if (veiculo != null)
                    {
                       
                        foreach (var assinante in assinantesDAL.Assinantes)
                        {
                            if (assinante.Value.Veiculos.Contains(veiculo))
                            {
                                assinante.Value.Veiculos.Remove(veiculo);
                                Console.WriteLine("Veículo removido com sucesso!");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Placa não encontrada, digite outro valor: ");
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
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Central de edicao de dados do veiculo");
            Console.WriteLine("--------------------------------------");

            var veiculos = assinantesDAL.Assinantes
                            .SelectMany(a => a.Value.Veiculos)
                            .ToList();

           
            if (veiculos.Count > 0)
            {
                foreach (var assinante in assinantesDAL.Assinantes)
                {

                    foreach (var veiculo in assinante.Value.Veiculos)
                    {
                        Console.WriteLine($"Proprietário: {assinante.Value.Nome}");
                        Console.WriteLine($"Tipo: {veiculo.Tipo}");
                        Console.WriteLine($"Eixos: {veiculo.NumeroEixos}");
                        Console.WriteLine($"Placa: {veiculo.Placa}");
                        Console.WriteLine("----------------------------------");
                    }
                }

                Console.WriteLine("\nDigite a placa do veiculo que deseja remover: ");

                while (true)
                {
                    string placa = Console.ReadLine();
                    Veiculo veiculo = VeiculoPorPlaca(placa, assinantesDAL);

                    if (veiculo != null)
                    {
                        Console.Clear();
                        Console.WriteLine("1 - Tipo");
                        Console.WriteLine("2 - Eixo");
                        Console.WriteLine("3 - Placa");
                        Console.WriteLine("Digite a opcao que deseja editar: ");

                        while (true)
                        {
                            if (int.TryParse(Console.ReadLine(), out var escolha))
                            {
                                switch (escolha)
                                {
                                    case 1:
                                        Console.Clear();
                                        Console.WriteLine("Escolha do Tipo do veiculo");
                                        string veiculoNovoTipo = GetVeiculoType();
                                        veiculo.Tipo = veiculoNovoTipo;
                                        return;
                                    case 2:
                                        Console.Clear();
                                        Console.WriteLine("Escolha do Eixo do veiculo");
                                        if(int.TryParse(Console.ReadLine(), out int eixoNovo))
                                        {
                                            veiculo.NumeroEixos = eixoNovo;
                                        }
                                        else
                                        {
                                            Console.WriteLine("O Eixo precisa ser um numero");
                                            Thread.Sleep(2000);
                                        }
                                  
                                        return;
                                    case 3:
                                        Console.Clear();
                                        Console.WriteLine("Escolha a Placa do veiculo");
                                        var possivelPlaca =  Console.ReadLine();
                                        if(ValidadorPlaca(possivelPlaca, assinantesDAL))
                                        {
                                            veiculo.Placa = possivelPlaca;
                                        }
                                        return;
                                    default:
                                        Console.WriteLine("Opcao nao encontrada, digite novamente:");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Opcao invalida, digite um numero: ");
                            }
                        }

                        
                    }
                    else
                    {
                        Console.WriteLine("Placa não encontrada, digite outro valor: ");
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
        static string GetVeiculoType()
        {
            Console.Clear();
            Dictionary<int, string> veiculosTipo = new()
            {
                {1, "Moto"},
                {2, "Carro" },
                {3, "Trator" },
                {4, "Onibus" },
                {5, "Reboque" },
                {6, "Triciclo" },
                {7, "Caminhao" },
                {8, "Quadriciclo" },
                {9, "Semi-Reboque" },
            };

            foreach (var veiculoTipo in veiculosTipo)
            {
                Console.WriteLine($"{veiculoTipo.Key} - {veiculoTipo.Value}");
            }
            Console.WriteLine("Digite o numero do tipo do veiculo: ");

            while (true)
            {
                if(int.TryParse(Console.ReadLine(), out int escolha) && veiculosTipo.ContainsKey(escolha))
                { 

                    if (escolha == 0)
                    {
                        Console.WriteLine("Digite o nome do outro tipo de veiculo: ");
                        string outroTipo = Console.ReadLine()!;
                        return outroTipo;
                    }
                    return veiculosTipo[escolha];
                }
                else
                {
                    Console.WriteLine("Digite um valor valido: ");
                }
            }

        }
        static int GetVeiculosEixo(string veiculoType)
        {
            static int SolicitarEixo()
            {
                Console.WriteLine("Digite a quantidade de eixos do veiculo: ");
                while (true)
                {

                    if (int.TryParse(Console.ReadLine(), out int eixoNovoValor) && eixoNovoValor > 0)
                    {
                        return eixoNovoValor;
                    }
                    else
                    {
                        Console.WriteLine("Digite um valor valido: ");
                    }
                }
            }

            string veiculoTypeFormated = veiculoType.Trim().ToLower();

            Dictionary<string, Func<int>> veiculosEixo = new()
            {
                {"moto", () => 1},
                {"carro", () => 2},
                {"trator", () => 2},
                {"onibus", () => SolicitarEixo()},
                {"reboque",() => SolicitarEixo()},
                {"triciclo", () => 1 },
                {"caminhao", () => SolicitarEixo()},
                {"quadriciclo", () => 2 },
                {"semi-reboque", () => SolicitarEixo()},
            };

            int veiculoEixo = veiculosEixo[veiculoTypeFormated]();

            
            if(!veiculosEixo.ContainsKey(veiculoTypeFormated))
            {
                veiculoEixo = SolicitarEixo();
                return veiculoEixo;
            }
            
                return veiculoEixo;

            
        }
        public Veiculo VeiculoPorPlaca(string placa, AssinantesDAL assinanteDAL)
        {
            var assinantes = assinanteDAL.Assinantes;

            foreach (var assinante in assinantes)
            {
               
                var veiculo = assinante.Value.Veiculos.FirstOrDefault(v => v.Placa == placa);

                if (veiculo != null)
                {
                    return veiculo;
                }
            }

            return null;
        }

    }
}
