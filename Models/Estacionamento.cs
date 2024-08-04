using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial;
        private decimal precoPorHora;
        private string[] vagas;
        private DateTime[] entradaCarros; // Armazena o horário de entrada dos veículos
        private Stack<int> pilhaEntrada; // Pilha para gerenciar a entrada dos veículos
        private Queue<int> filaSaida; // Fila para gerenciar a saída dos veículos
        private List<string> historicoVeiculos; // Histórico dos veículos que já estiveram estacionados

        public Estacionamento(decimal precoInicial, decimal precoPorHora, int capacidade)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
            this.vagas = new string[capacidade];
            this.entradaCarros = new DateTime[capacidade];
            this.pilhaEntrada = new Stack<int>(capacidade);
            this.filaSaida = new Queue<int>(capacidade);
            this.historicoVeiculos = new List<string>(); // Inicializa o histórico de veículos
        }

        public void AdicionarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();

            if (ValidaPlaca(placa))
            {
                int vaga = Array.FindIndex(vagas, v => v == null);
                if (vaga != -1)
                {
                    if (pilhaEntrada.Contains(vaga))
                    {
                        Console.WriteLine("Essa vaga já está ocupada.");
                        return;
                    }
                    vagas[vaga] = placa;
                    entradaCarros[vaga] = DateTime.Now;
                    pilhaEntrada.Push(vaga); // Adiciona a vaga à pilha de entrada
                    filaSaida.Enqueue(vaga); // Adiciona a vaga à fila de saída
                    historicoVeiculos.Add($"Entrada: {placa} na vaga {vaga} em {entradaCarros[vaga]}"); // Adiciona ao histórico
                    Console.WriteLine("Veículo estacionado com sucesso na vaga " + vaga);
                }
                else
                {
                    Console.WriteLine("Não há vagas disponíveis.");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida. A placa deve ter 7 caracteres.");
            }
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine();

            if (ValidaPlaca(placa))
            {
                int vaga = Array.FindIndex(vagas, v => v == placa);
                if (vaga != -1)
                {
                    var tempoEstacionado = DateTime.Now - entradaCarros[vaga];
                    decimal valorTotal = precoInicial + precoPorHora * (decimal)tempoEstacionado.TotalHours;
                    vagas[vaga] = null;
                    entradaCarros[vaga] = default(DateTime);
                    pilhaEntrada = new Stack<int>(pilhaEntrada.Where(v => v != vaga)); // Remove da pilha de entrada
                    filaSaida = new Queue<int>(filaSaida.Where(v => v != vaga)); // Remove da fila de saída
                    historicoVeiculos.Add($"Saída: {placa} da vaga {vaga} em {DateTime.Now} - Tempo: {tempoEstacionado.TotalHours:F2} horas - Valor: R$ {valorTotal:F2}"); // Adiciona ao histórico
                    Console.WriteLine($"O veículo {placa} foi removido. Tempo total: {tempoEstacionado.TotalHours:F2} horas. Valor total: R$ {valorTotal:F2}");
                }
                else
                {
                    Console.WriteLine("Veículo não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida. A placa deve ter 7 caracteres.");
            }
        }

        public void ListarVeiculos()
        {
            Console.WriteLine("Os veículos estacionados são:");
            for (int i = 0; i < vagas.Length; i++)
            {
                if (vagas[i] != null)
                {
                    Console.WriteLine($"Vaga {i}: {vagas[i]} - Entrada: {entradaCarros[i]}");
                }
            }
        }

        public void BuscarVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para buscar:");
            string placa = Console.ReadLine();

            if (ValidaPlaca(placa))
            {
                int vaga = Array.FindIndex(vagas, v => v == placa);
                if (vaga != -1)
                {
                    Console.WriteLine($"O veículo {placa} está na vaga {vaga}. Entrada: {entradaCarros[vaga]}");
                }
                else
                {
                    Console.WriteLine("Veículo não encontrado.");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida. A placa deve ter 7 caracteres.");
            }
        }

        public void ExportarRelatorio()
        {
            string relatorio = "Relatório de Veículos Estacionados\n";
            relatorio += "-------------------------------\n";
            foreach (var registro in historicoVeiculos)
            {
                relatorio += registro + "\n";
            }

            string caminhoArquivo = "RelatorioEstacionamento.txt";
            File.WriteAllText(caminhoArquivo, relatorio);
            Console.WriteLine($"Relatório exportado para {caminhoArquivo}");
        }

        private bool ValidaPlaca(string placa)
        {
            return placa.Length == 7; // Validação simples de comprimento
        }
    }
}
