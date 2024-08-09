using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Turns
{
    public class TurnConfig(int maxTurns = 100) : ITurnConfig
    {
        public int MaxTurns { get; protected set; } = maxTurns;
    }
}