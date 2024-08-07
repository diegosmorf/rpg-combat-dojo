using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class TurnConfig(int maxTurns = 100) : ITurnConfig
    {
        public int MaxTurns { get; protected set; } = maxTurns;
    }
}