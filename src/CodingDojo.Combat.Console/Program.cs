
using CodingDojo.Combat.Domain;
using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.ConsoleApp
{
    public static class Program
    {
        private const string gameName = "CodingDojo RPG Battle Game";

        public static void Main()
        {
            PrintWelcomeGame();            
            var game = new Game();
            var maxTurns = 100;

            while (AcceptPlayBattle())
            {
                var player1 = PrintSelectPlayer(1);
                var player2 = PrintSelectPlayer(2);
                
                var players = new[] { player1, player2 };
                var battle = game.SetupBattle(players, maxTurns);
                battle.RunAutomatic();

                foreach (var turn in battle.Turns)
                {
                    PrintTurn(turn.LogInfo);
                }                

                PrintBattle(battle);
            }

            PrintEndGame();
           
        }

        private static void PrintWelcomeGame()
        {
            Console.Clear();
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Welcome to {gameName}");
            Console.WriteLine($"-----------------------------------------------------");
        }

        private static ICharacter PrintSelectPlayer(int player)
        {
            Console.Clear();
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Let's define Player: {player}");
            Console.WriteLine($"Select your Character Job:");
            Console.WriteLine($"-----------------------------------------------------");

            var soldier = new Soldier();
            var knight = new Knight();
            var wizard = new Wizard();
            var archer = new Archer();

            PrintCharacter(1, soldier);
            PrintCharacter(2, knight);
            PrintCharacter(3, wizard);
            PrintCharacter(4, archer);

            Console.WriteLine($"Please select an option: ");
            var option = Console.ReadLine() ?? "1";
            Console.WriteLine($"You selected: {option}");

            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Now, you can type a Name:");
            string name = Console.ReadLine() ?? $"Default Player {player}";

            return CreatePlayer(option, name);

        }

        private static ICharacter CreatePlayer(string option, string name) => option switch
        {
            "1" => new Soldier(name),
            "2" => new Knight(name),
            "3" => new Wizard(name),
            "4" => new Archer(name),
            _ => new Soldier(name)
        };

        private static void PrintCharacter(int number, ICharacter character)
        {            
            Console.WriteLine();
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"({number}): {character.Job} | HP: {character.Health.Value} | STR: {character.Strength} | DEF: {character.Defense} | MAG: {character.Magic}");

        }

        private static bool AcceptPlayBattle()
        {            
            Console.WriteLine();
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Let's play a RPG Battle ? ");
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"1- Yes");
            Console.WriteLine($"2- No");

            Console.WriteLine($"Please select an option: ");
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
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Battle Finished: {battle.HasFinished}");
            Console.WriteLine($"Processed Turns : {battle.ProcessedTurns}");
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Winner: {battle.Winner?.Name}");
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"IsAlive: {battle.Winner?.IsAlive}");
            Console.WriteLine($"Job: {battle.Winner?.Job}");
            Console.WriteLine($"HP: {battle.Winner?.Health.Value}");
            Console.WriteLine($"IsAlive: {battle.Winner?.IsAlive}");
            Console.WriteLine($"-----------------------------------------------------");            
            Console.WriteLine($"Looser: {battle.Looser?.Name}");
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Job: {battle.Looser?.Job}");
            Console.WriteLine($"HP: {battle.Looser?.Health.Value}");
            Console.WriteLine($"IsAlive: {battle.Looser?.IsAlive}");
            Console.WriteLine($"-----------------------------------------------------");
        }

        private static void PrintEndGame()
        {
            Console.WriteLine();            
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Goodbye. Thanks for play {gameName} ");            
            Console.WriteLine($"-----------------------------------------------------");
            Console.ReadLine();
        }

        private static void PrintTurn(ITurnLogInfo log)
        {
            Console.WriteLine();
            Console.WriteLine($"-----------------------------------------------------");
            Console.WriteLine($"Actor: {log.Actor.Name} | HP: {log.Actor.Health.Value} | Job: {log.Actor.Job} | Level: {log.Actor.Level}");
            Console.WriteLine($"Target: {log.Target.Name} | HP: {log.Target.Health.Value} | Job: {log.Target.Job} | Level: {log.Target.Level}" );
            Console.WriteLine($"Action: {log.ActionType}");
            Console.WriteLine($"Damage: {log.Damage}");
            Console.WriteLine($"Heal: {log.HealthToIncrease}");

        }
    }
}