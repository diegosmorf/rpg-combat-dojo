using CodingDojo.Combat.Actions;
using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class TurnLogInfo : ITurnLogInfo
    {
        public ICharacter? Actor { get; set; }
        public IDiceLogInfo? Dice { get; set; }
        public int Damage { get; set; }
        public int HealthToIncrease { get; set; }
        public ICharacter? Target { get; set; }
        public ActionType Action { get; set; }
    }
}