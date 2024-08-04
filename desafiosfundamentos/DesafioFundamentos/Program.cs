using System;
using DesafioFundamentos.Models;

namespace DesafioFundamentos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configura o encoding para UTF8 para suportar acentuação
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Declaração de variáveis para preços e capacidade do estacionamento
            decimal precoInicial = 0;
            decimal precoPorHora = 0;
            int capacidade = 5; // Defina a capacidade do estacionamento (número de vagas)

            // Solicita e valida o preço inicial
            Console.WriteLine("Seja bem-vindo ao sistema de estacionamento!\n" +
                              "Digite o preço inicial:");
            while (!decimal.TryParse(Console.ReadLine(), out precoInicial) || precoInicial < 0)
            {
                Console.WriteLine("Valor inválido. Digite um preço inicial válido (não negativo):");
            }

            // Solicita e valida o preço por hora
            Console.WriteLine("Agora digite o preço por hora:");
            while (!decimal.TryParse(Console.ReadLine(), out precoPorHora) || precoPorHora < 0)
            {
                Console.WriteLine("Valor inválido. Digite um preço por hora válido (não negativo):");
            }

            // Instancia a classe Estacionamento com os valores e capacidade definidos
            Estacionamento es = new Estacionamento(precoInicial, precoPorHora, capacidade);

            string opcao;
            bool exibirMenu = true;

            // Loop do menu
            while (exibirMenu)
            {
                Console.Clear();
                Console.WriteLine("Digite a sua opção:");
                Console.WriteLine("1 - Cadastrar veículo");
                Console.WriteLine("2 - Remover veículo");
                Console.WriteLine("3 - Listar veículos");
                Console.WriteLine("4 - Buscar veículo");
                Console.WriteLine("5 - Exportar relatório");
                Console.WriteLine("6 - Encerrar");

                switch (Console.ReadLine())
                {
                    case "1":
                        es.AdicionarVeiculo();
                        break;

                    case "2":
                        es.RemoverVeiculo();
                        break;

                    case "3":
                        es.ListarVeiculos();
                        break;

                    case "4":
                        es.BuscarVeiculo();
                        break;

                    case "5":
                        es.ExportarRelatorio();
                        break;

                    case "6":
                        exibirMenu = false;
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

                Console.WriteLine("Pressione uma tecla para continuar");
                Console.ReadLine();
            }

            Console.WriteLine("O programa se encerrou");
        }
    }
}
