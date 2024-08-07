using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Dices
{
    public class DiceLogInfo : IDiceLogInfo
    {
        public int ActorValue { get; set; }
        public int TargetValue { get; set; }
    }
}