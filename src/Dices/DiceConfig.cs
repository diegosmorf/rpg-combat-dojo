using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Dices
{
    public class DiceConfig(int min, int max) : IDiceConfig
    {
        public DiceConfig(int uniqueValue) : this(uniqueValue, uniqueValue)
        {
        }

        public int Min { get; protected set; } = min;
        public int Max { get; protected set; } = max;
    }
}