using CodingDojo.Combat.Domain.Actions;
using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Turns
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