using System;
using System.Collections.Generic;
using System.Linq;

// Classe para representar um jogador
class Jogador  // SELSO PACHECO - AP2
{
    public string Nome { get; set; } // Nome completo do jogador
    public string Nickname { get; set; } // nick do jogador
    public int Pontos { get; private set; } // Pontuação do jogador

    // Construtor para criar um jogador com nome e apelido
    public Jogador(string nome, string nickname)
    {
        Nome = nome;
        Nickname = nickname;
        Pontos = 0; // Inicializa a pontuação em zero
    }

    // Método para simular a atuação do jogador em uma partida
    public void Jogar()
    {
        Random random = new Random();
        int pontosGanhos = random.Next(1, 101); // Gera uma pontuação aleatória entre 1 e 100
        Pontos += pontosGanhos; // Adiciona os pontos à pontuação total do jogador
    }
}

// Classe para representar uma equipe
class Equipe
{
    public string NomeEquipe { get; set; } // Nome da equipe
    public List<Jogador> Jogadores { get; private set; } // Lista de jogadores na equipe

    // Construtor para criar uma equipe com um nome
    public Equipe(string nomeEquipe)
    {
        NomeEquipe = nomeEquipe;
        Jogadores = new List<Jogador>();
    }

    // Método para calcular a pontuação total da equipe somando as pontuações de seus jogadores
    public int PontosTotal()
    {
        return Jogadores.Sum(j => j.Pontos);
    }

    // Método para adicionar um jogador à equipe (limite de 5 jogadores por equipe)
    public void AdicionarJogador(Jogador jogador)
    {
        if (Jogadores.Count < 5)
        {
            Jogadores.Add(jogador);
        }
        else
        {
            Console.WriteLine("A equipe já possui 5 jogadores. Não é possível adicionar mais jogadores.");
        }
    }
}

// Classe para representar um campeonato
class Campeonato
{
    public string NomeCampeonato { get; set; } // Nome do campeonato
    public List<Equipe> EquipesParticipantes { get; private set; } // Lista de equipes no campeonato

    // Construtor para criar um campeonato com um nome
    public Campeonato(string nomeCampeonato)
    {
        NomeCampeonato = nomeCampeonato;
        EquipesParticipantes = new List<Equipe>();
    }

    // Método para iniciar uma partida entre duas equipes
    public void IniciarPartida(Equipe e1, Equipe e2)
    {
        if (e1.Jogadores.Count != 5 || e2.Jogadores.Count != 5)
        {
            Console.WriteLine("Cada equipe deve ter exatamente 5 jogadores para iniciar a partida.");
            return;
        }

        foreach (var jogador in e1.Jogadores)
        {
            jogador.Jogar();
        }

        foreach (var jogador in e2.Jogadores)
        {
            jogador.Jogar();
        }
        Console.WriteLine("Partida simulada com sucesso!");
    }

    // Método para mostrar a classificação das equipes no campeonato, incluindo as pontuações individuais dos jogadores
    public void Classificacao()
    {
        EquipesParticipantes.Sort((e1, e2) => e2.PontosTotal().CompareTo(e1.PontosTotal()));

        Console.WriteLine("Classificação das equipes no campeonato:");
        int posicao = 1;
        foreach (var equipe in EquipesParticipantes)
        {
            Console.WriteLine($"{posicao}. {equipe.NomeEquipe} - Total de Pontos: {equipe.PontosTotal()}");
            Console.WriteLine("Pontuações dos jogadores:");
            foreach (var jogador in equipe.Jogadores)
            {
                Console.WriteLine($"- {jogador.Nome} ({jogador.Nickname}): {jogador.Pontos}");
            }
            posicao++;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Jogador> jogadores = new List<Jogador>();
        List<Equipe> equipes = new List<Equipe>();
        List<Campeonato> campeonatos = new List<Campeonato>();

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Criar Jogador");
            Console.WriteLine("2. Criar Equipe");
            Console.WriteLine("3. Adicionar Jogador a uma Equipe");
            Console.WriteLine("4. Criar Campeonato");
            Console.WriteLine("5. Adicionar Equipe ao Campeonato");
            Console.WriteLine("6. Iniciar Partida");
            Console.WriteLine("7. Ver Classificação");
            Console.WriteLine("8. Sair");

            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:         // Opção para criar um jogador
                    Console.Write("Nome do jogador: ");
                    string nomeJogador = Console.ReadLine();
                    Console.Write("Nickname do jogador: ");
                    string nicknameJogador = Console.ReadLine();
                    Jogador jogador = new Jogador(nomeJogador, nicknameJogador);
                    jogadores.Add(jogador);
                    Console.WriteLine("Jogador criado com sucesso!");
                    break;

                case 2:         // Opção para criar uma equipe
                    Console.Write("Nome da equipe: ");
                    string nomeEquipe = Console.ReadLine();
                    Equipe equipe = new Equipe(nomeEquipe);
                    equipes.Add(equipe);
                    Console.WriteLine("Equipe criada com sucesso!");
                    break;

                case 3:         // Opção para adicionar um jogador a uma equipe
                    if (jogadores.Count == 0 || equipes.Count == 0)
                    {
                        Console.WriteLine("Crie jogadores e equipes antes de adicionar jogadores a equipes.");
                        break;
                    }

                    Console.Write("Selecione o jogador (pelo índice) que deseja adicionar à equipe: ");
                    for (int i = 0; i < jogadores.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {jogadores[i].Nome} ({jogadores[i].Nickname})");
                    }
                    int jogadorIndex = int.Parse(Console.ReadLine()) - 1;

                    if (jogadorIndex >= 0 && jogadorIndex < jogadores.Count)
                    {
                        Console.Write("Selecione a equipe (pelo índice) à qual deseja adicionar o jogador: ");
                        for (int i = 0; i < equipes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {equipes[i].NomeEquipe}");
                        }
                        int equipeIndex = int.Parse(Console.ReadLine()) - 1;

                        if (equipeIndex >= 0 && equipeIndex < equipes.Count)
                        {
                            equipes[equipeIndex].AdicionarJogador(jogadores[jogadorIndex]);
                            Console.WriteLine("Jogador adicionado à equipe com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Índice de equipe inválido.");
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Índice de jogador inválido.");
                    }
                    break;

                case 4:         // Opção para criar um campeonato
                    Console.Write("Nome do campeonato: ");
                    string nomeCampeonato = Console.ReadLine();
                    Campeonato campeonato = new Campeonato(nomeCampeonato);
                    campeonatos.Add(campeonato);
                    Console.WriteLine("Campeonato criado com sucesso!");
                    break;

                case 5:         // Opção para adicionar uma equipe a um campeonato
                    if (campeonatos.Count == 0 || equipes.Count == 0)
                    {
                        Console.WriteLine("Crie campeonatos e equipes antes de adicionar equipes aos campeonatos.");
                        break;
                    }

                    Console.Write("Selecione a equipe (pelo índice) que deseja adicionar ao campeonato: ");
                    for (int i = 0; i < equipes.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {equipes[i].NomeEquipe}");
                    }
                    int equipeIndexToAdd = int.Parse(Console.ReadLine()) - 1;

                    if (equipeIndexToAdd >= 0 && equipeIndexToAdd < equipes.Count)
                    {
                        Console.Write("Selecione o campeonato (pelo índice) ao qual deseja adicionar a equipe: ");
                        for (int i = 0; i < campeonatos.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {campeonatos[i].NomeCampeonato}");
                        }
                        int campeonatoIndexToAdd = int.Parse(Console.ReadLine()) - 1;

                        if (campeonatoIndexToAdd >= 0 && campeonatoIndexToAdd < campeonatos.Count)
                        {
                            campeonatos[campeonatoIndexToAdd].EquipesParticipantes.Add(equipes[equipeIndexToAdd]);
                            Console.WriteLine("Equipe adicionada ao campeonato com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Índice de campeonato inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Índice de equipe inválido.");
                    }
                    break;

                case 6:         // Opção para iniciar uma partida
                    if (campeonatos.Count < 1)
                    {
                        Console.WriteLine("Crie pelo menos um campeonato antes de iniciar uma partida.");
                        break;
                    }

                    Console.Write("Selecione o campeonato (pelo índice) para iniciar a partida: ");
                    for (int i = 0; i < campeonatos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {campeonatos[i].NomeCampeonato}");
                    }
                    int campeonatoIndexToStartMatch = int.Parse(Console.ReadLine()) - 1;

                    if (campeonatoIndexToStartMatch >= 0 && campeonatoIndexToStartMatch < campeonatos.Count)
                    {
                        if (campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes.Count < 2)
                        {
                            Console.WriteLine("É necessário pelo menos 2 equipes para iniciar uma partida.");
                            break;
                        }

                        Console.Write("Selecione a primeira equipe (pelo índice) para a partida: ");
                        for (int i = 0; i < campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes[i].NomeEquipe}");
                        }
                        int equipeIndex1ToStartMatch = int.Parse(Console.ReadLine()) - 1;

                        Console.Write("Selecione a segunda equipe (pelo índice) para a partida: ");
                        for (int i = 0; i < campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes[i].NomeEquipe}");
                        }
                        int equipeIndex2ToStartMatch = int.Parse(Console.ReadLine()) - 1;

                        if (equipeIndex1ToStartMatch >= 0 && equipeIndex1ToStartMatch < campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes.Count &&
                            equipeIndex2ToStartMatch >= 0 && equipeIndex2ToStartMatch < campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes.Count &&
                            equipeIndex1ToStartMatch != equipeIndex2ToStartMatch)
                        {
                            campeonatos[campeonatoIndexToStartMatch].IniciarPartida(
                                campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes[equipeIndex1ToStartMatch],
                                campeonatos[campeonatoIndexToStartMatch].EquipesParticipantes[equipeIndex2ToStartMatch]
                            );
                        }
                        else
                        {
                            Console.WriteLine("Índices de equipe inválidos ou as equipes selecionadas são as mesmas.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Índice de campeonato inválido.");
                    }
                    break;

                case 7:         // Opção para ver a classificação das equipes
                    if (campeonatos.Count < 1)
                    {
                        Console.WriteLine("Crie pelo menos um campeonato antes de ver a classificação.");
                        break;
                    }

                    Console.Write("Selecione o campeonato (pelo índice) para ver a classificação: ");
                    for (int i = 0; i < campeonatos.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {campeonatos[i].NomeCampeonato}");
                    }
                    int campeonatoIndexForClassificacao = int.Parse(Console.ReadLine()) - 1;

                    if (campeonatoIndexForClassificacao >= 0 && campeonatoIndexForClassificacao < campeonatos.Count)
                    {
                        campeonatos[campeonatoIndexForClassificacao].Classificacao();
                    }
                    else
                    {
                        Console.WriteLine("Índice de campeonato inválido.");
                    }
                    break;

                case 8:
                    Environment.Exit(0);         // Opção para sair do programa
                    break;

                default:         // Opção inválida
                    Console.WriteLine("Opção inválida."); 
                    break;
            }
        }
    }
}