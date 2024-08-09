using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Dices;

namespace CodingDojo.Combat.Domain
{
    public class Game
    {
        public List<IBattle> Battles { get; protected set; } = [];

        public Battle SetupBattle(IEnumerable<ICharacter> players, IDice normalDice, IDice magicDice, int maxTurns=100)
        {
            var battle = new Battle(normalDice, magicDice, maxTurns);
            battle.Players.AddRange(players);            
            Battles.Add(battle);
            return battle;
        }
        
        public Battle SetupBattle(IEnumerable<ICharacter> players, int maxTurns = 100)
        {
            var normalDice = new Dice(new NormalDiceConfig());
            var magicDice = new Dice(new MagicDiceConfig());  

            return SetupBattle(players, normalDice, magicDice, maxTurns);
        }

        public Battle SetupBattle(int maxTurns = 100)
        {
            var players = new List<ICharacter>
            {
                new Soldier("Soldier 1"),
                new Soldier("Soldier 2")
            };
            
            return SetupBattle(players, maxTurns);            
        }
    }
}