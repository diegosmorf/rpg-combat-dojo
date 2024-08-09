using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Dices
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