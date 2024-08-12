using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Dices
{
    public class DiceLogInfo(int actorValue = 0, int targetValue = 0) : IDiceLogInfo
    {
        public int ActorValue { get; set; } = actorValue;
        public int TargetValue { get; set; } = targetValue;
    }
}