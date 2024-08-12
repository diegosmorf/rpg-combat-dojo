using CodingDojo.Combat.Domain.Actions;

namespace CodingDojo.Combat.Domain.Contracts
{
    public interface ITurnLogInfo
    {
        ICharacter Actor { get; }
        IDiceLogInfo DiceLogInfo { get; }
        int Damage { get; }
        int HealthToIncrease { get; }
        ICharacter Target { get; }
        ActionType ActionType { get; }
    }
}