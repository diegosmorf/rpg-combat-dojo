﻿using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public class TurnLogInfo: ITurnLogInfo
    {
        public ICharacter? Actor { get; set; }
        public IDiceLogInfo? Dice { get; set; }
        public int Damage { get; set; }
        public ICharacter? Target { get; set; }
        public TurnAction Action { get; set; }
    }
}