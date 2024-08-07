using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public interface ITurnLogInfo
    {
        ICharacter? Actor { get; set; }
        IDiceLogInfo? Dice { get; set; }
        int Damage { get; set; }
        int HealthToIncrease { get; set; }
        ICharacter? Target { get; set; }
        TurnAction Action { get; set; }
    }
}