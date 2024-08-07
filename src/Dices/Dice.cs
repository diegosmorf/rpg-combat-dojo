using System.Security.Cryptography;
using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Dices
{
    public class Dice(IDiceConfig config) : IDice
    {
        protected readonly IDiceConfig config = config;

        public int Roll()
        {
            return RandomNumberGenerator.GetInt32(config.Min, config.Max);
        }
    }
}