using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Pergunta a quantidade de peixes no lago
        Console.Write("Quantos peixes há no lago? ");
        int numPeixes = int.Parse(Console.ReadLine());

        // Pergunta quantos jogadores vão jogar
        Console.Write("Quantos jogadores vão jogar? ");
        int numJogadores = int.Parse(Console.ReadLine());

        // Cria uma lista para armazenar os jogadores
        List<Jogador> jogadores = new List<Jogador>();

        // Pergunta o nome dos jogadores e quantas tentativas eles terão
        for (int i = 0; i < numJogadores; i++)
        {
            Console.Write($"Qual o nome do jogador {i + 1}? ");
            string nome = Console.ReadLine();

            Console.Write($"Quantas tentativas o {nome} terá? ");
            int tentativas = int.Parse(Console.ReadLine());

            jogadores.Add(new Jogador(nome, tentativas));
        }

        // Cria os peixes no lago
        List<Peixe> lago = GerarPeixes(numPeixes);

        // Faz os jogadores pescarem
        for (int i = 0; i < numJogadores; i++)
        {
            Jogador jogador = jogadores[i];
            Console.WriteLine($"\n{jogador.Nome} começa a pescar!");

            for (int j = 0; j < jogador.Tentativas; j++)
            {
                Console.WriteLine($"Tentativa {j + 1}:");
                Console.Write("Escolha a posição para lançar a isca (0-49): ");
                int posicao = int.Parse(Console.ReadLine());

                // Verifica se existe peixe na posição escolhida
                Peixe peixe = lago.Find(p => p.Posicao == posicao);
                if (peixe != null) // Se houver um peixe na posição
                {
                    lago.Remove(peixe);  // Remove o peixe do lago
                    jogador.PescarPeixe(peixe); // O jogador pesca o peixe
                    Console.WriteLine($"Você pescou um {peixe.Tipo} de {peixe.Peso}kg!");
                }
                else
                {
                    Console.WriteLine("Não há peixe nesta posição.");
                }
            }
        }

        // Determina o vencedor baseado no peso total de peixes pescados
        Jogador vencedor = jogadores[0];
        foreach (Jogador jogador in jogadores)
        {
            if (jogador.PesoTotal > vencedor.PesoTotal)
                vencedor = jogador;
        }

        Console.WriteLine($"\nO vencedor é {vencedor.Nome} com {vencedor.PesoTotal}kg de peixes!");
    }

    // Função para gerar os peixes no lago
    static List<Peixe> GerarPeixes(int numPeixes)
    {
        Random rand = new Random();
        List<Peixe> peixes = new List<Peixe>();
        HashSet<int> posicoes = new HashSet<int>();

        // Gera peixes e coloca eles em posições aleatórias no lago
        for (int i = 0; i < numPeixes; i++)
        {
            int posicao;
            do
            {
                posicao = rand.Next(0, 50); // Posições de 0 a 49
            } while (posicoes.Contains(posicao)); // Garante que não haja peixes na mesma posição

            posicoes.Add(posicao);

            // Gera um peixe aleatório
            Peixe peixe = GerarPeixeAleatorio();
            peixe.Posicao = posicao;
            peixes.Add(peixe);
        }

        return peixes;
    }

    // Função para gerar um peixe aleatório
    static Peixe GerarPeixeAleatorio()
    {
        Random rand = new Random();
        int tipoPeixe = rand.Next(1, 4); // 1 = Tilápia, 2 = Pacu, 3 = Tambaqui
        double peso = 0;

        switch (tipoPeixe)
        {
            case 1: // Tilápia (1kg a 2kg)
                peso = rand.NextDouble() + 1.0;
                return new Peixe("Tilápia", peso);
            case 2: // Pacu (2kg a 4kg)
                peso = rand.NextDouble() * 2 + 2.0;
                return new Peixe("Pacu", peso);
            case 3: // Tambaqui (3kg a 5kg)
                peso = rand.NextDouble() * 2 + 3.0;
                return new Peixe("Tambaqui", peso);
            default:
                return null;
        }
    }
}

// Classe para armazenar as informações do peixe
class Peixe
{
    public string Tipo { get; set; }
    public double Peso { get; set; }
    public int Posicao { get; set; }

    public Peixe(string tipo, double peso)
    {
        Tipo = tipo;
        Peso = peso;
    }
}

// Classe para armazenar as informações do jogador
class Jogador
{
    public string Nome { get; set; }
    public int Tentativas { get; set; }
    public double PesoTotal { get; private set; }

    public Jogador(string nome, int tentativas)
    {
        Nome = nome;
        Tentativas = tentativas;
        PesoTotal = 0; // Inicialmente, o jogador não pescou nenhum peixe
    }

    // Método para aumentar a quantidade de peso total dos peixes pescados
    public void PescarPeixe(Peixe peixe)
    {
        PesoTotal += peixe.Peso;
    }
}
