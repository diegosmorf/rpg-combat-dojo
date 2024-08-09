using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Dices
{
    public class DiceLogInfo : IDiceLogInfo
    {
        public int ActorValue { get; set; }
        public int TargetValue { get; set; }
    }
}