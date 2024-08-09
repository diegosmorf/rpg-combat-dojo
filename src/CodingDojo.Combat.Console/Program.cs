
using CodingDojo.Combat.Domain;

namespace CodingDojo.Combat.ConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var game = new Game();
            var battle = game.SetupBattle();
            battle.Run();
            Console.WriteLine($"Battle Finished: {battle.HasFinished}");
            Console.WriteLine($"Winner: {battle.Winner?.Name}");
            Console.WriteLine($"Winner: {battle.Winner?.IsAlive}");
            Console.WriteLine($"Winner: {battle.Winner?.Job}");
            Console.WriteLine($"Winner: {battle.Winner?.Health.Value}");
            Console.WriteLine($"Processed Turns : {battle.ProcessedTurns}");
            Console.WriteLine($"Looser: {battle.Looser?.Name}");
            Console.WriteLine($"Looser: {battle.Winner?.Job}");
            Console.WriteLine($"Looser: {battle.Looser?.IsAlive}");
        }
    }
}