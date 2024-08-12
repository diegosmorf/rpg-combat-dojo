
using CodingDojo.Combat.Domain;
using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.ConsoleApp
{
    public static class Program
    {
        private const string gameName = "CodingDojo RPG Battle Game";
        private const int maxTurns = 100;

        public static void Main()
        {
            PrintWelcomeGame();
            PrintGame();
            PrintEndGame();
        }

        private static void PrintGame()
        {
            var game = new Game();

            while (UserAcceptPlayBattle())
            {
                var player1 = PrintSelectPlayer(ConsoleColor.Green, 1);
                var player2 = PrintSelectPlayer(ConsoleColor.Yellow, 2);

                var players = new[] { player1, player2 };
                var battle = game.SetupBattle(players, maxTurns);
                battle.RunAutomatic();

                for (int i = 0; i < battle.LogTurns.Count; i++)
                {
                    var turn = battle.LogTurns[i];
                    PrintTurn(i, turn);
                }

                PrintBattle(battle);
            }
        }

        private static void PrintWelcomeGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            PrintLine();
            Console.WriteLine($"Welcome to {gameName}");
        }

        private static void PrintLine()
        {
            Console.WriteLine($"---------------------------------------------------------------------------------------------");
        }

        private static ICharacter PrintSelectPlayer(ConsoleColor color, int player)
        {
            var defaultName = $"Player {player}";
            var retry = 1;
            var maxRetries = 3;

            while (retry <= maxRetries)
            {
                try
                {
                    retry++;
                    Console.ForegroundColor = color;
                    Console.Clear();
                    PrintLine();
                    Console.WriteLine($"PLAYER {player} -  Select your Character Job:");
                    PrintLine();

                    PrintCharacter("1", new Soldier(), true);
                    PrintCharacter("2", new Knight(), true);
                    PrintCharacter("3", new Wizard(), true);
                    PrintCharacter("4", new Archer(), true);

                    Console.WriteLine($"Please select a valid option (retry: {retry} ... ");
                    var option = Console.ReadLine() ?? "1";
                    Console.WriteLine($"You selected: {option}");

                    PrintLine();
                    Console.WriteLine($"Now, you can type a Name:");
                    var name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                        name = defaultName;

                    return CreatePlayer(option, name);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return CreatePlayer("1", defaultName);
        }

        private static ICharacter CreatePlayer(string option, string name) => option switch
        {
            "1" => new Soldier(name),
            "2" => new Knight(name),
            "3" => new Wizard(name),
            "4" => new Archer(name),
            _ => new Soldier(name)
        };
        private static bool UserAcceptPlayBattle()
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintLine();
            Console.WriteLine($"Would you like to play a new RPG Battle ? ");
            PrintLine();
            Console.WriteLine($"1- Yes");
            Console.WriteLine($"2- No");

            Console.WriteLine($"Please select a valid option ... ");
            var option = Console.ReadLine();
            Console.WriteLine($"You selected: {option}");

            return option switch
            {
                "1" => true,
                _ => false,
            };
        }

        private static void PrintBattle(IBattle battle)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PrintLine();
            Console.WriteLine($"Battle Finished: {battle.HasFinished}");
            Console.WriteLine($"Processed Turns : {battle.ProcessedTurns}");
            PrintLine();
            PrintCharacter("Winner", battle.Winner, true);
            PrintCharacter("Looser", battle.Looser, true);
        }

        private static void PrintEndGame()
        {
            Console.WriteLine();
            PrintLine();
            Console.WriteLine($"Goodbye. Thanks for play {gameName} ");
            PrintLine();
            Console.ReadLine();
        }

        private static void PrintTurn(int number, ITurnLogInfo log)
        {
            Console.ForegroundColor = (number % 2) == 0 ? ConsoleColor.Yellow : ConsoleColor.Green;

            PrintLine();
            Console.WriteLine($"Turn: {number + 1}");
            PrintCharacter($"Actor {log.Actor.Name}", log.Actor);
            PrintCharacter($"Target {log.Target.Name}", log.Target);
            Console.WriteLine($"Action: {log.ActionType} | Damage: {log.Damage} | Heal: {log.HealthToIncrease}");
        }

        private static void PrintCharacter(string message, ICharacter? character, bool printLine = false)
        {
            if (character == null)
            {
                return;
            }

            Console.WriteLine($"{message} | {character.Job} | HP: {character.Health.Value} | STR: {character.Strength} | DEF: {character.Defense} | MAG: {character.Magic} | LV: {character.Level} | EXP: {character.Experience} | IsAlive: {character.IsAlive}");

            if (printLine)
            {
                PrintLine();
            }
        }
    }
}