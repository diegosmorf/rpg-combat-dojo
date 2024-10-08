﻿using CodingDojo.Combat.Domain.Contracts;
using System.Security.Cryptography;

namespace CodingDojo.Combat.Domain.Dices
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