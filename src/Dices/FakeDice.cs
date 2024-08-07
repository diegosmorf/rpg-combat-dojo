using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Dices
{
    public class FakeDice(IDiceConfig config) : IDice
    {
        protected readonly IDiceConfig config = config;

        public int Roll()
        {
            return config.Max;
        }
    }
}