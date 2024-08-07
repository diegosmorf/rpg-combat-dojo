namespace CodingDojo.Combat
{
    public class DiceConfig(int min = 1, int max = 6) : IDiceConfig
    {
        public int Min { get; protected set; } = min;
        public int Max { get; protected set; } = max;
    }
}