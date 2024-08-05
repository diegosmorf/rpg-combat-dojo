namespace CodingDojo.Combat.Turns
{
    public class Dice(IDiceConfig config): IDice
    {
        protected readonly IDiceConfig config = config;

        public int Roll()
        {
            return new Random().Next(config.Min, config.Max);
        }
    }
}