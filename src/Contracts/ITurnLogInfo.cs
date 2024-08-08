using CodingDojo.Combat.Actions;

namespace CodingDojo.Combat.Contracts
{
    public interface ITurnLogInfo
    {
        ICharacter? Actor { get; set; }
        IDiceLogInfo? Dice { get; set; }
        int Damage { get; set; }
        int HealthToIncrease { get; set; }
        ICharacter? Target { get; set; }
        ActionType Action { get; set; }
    }
}