using CodingDojo.Combat.Domain.Actions;
using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Turns
{
    public class TurnLogInfo(ICharacter actor, ICharacter target, IDiceLogInfo diceLogInfo, int damage = 0, int healthToIncrease = 0, ActionType actionType = ActionType.Attack) : ITurnLogInfo
    {
        public ICharacter Actor { get; protected set; } = actor;        
        public int Damage { get; protected set; } = damage;
        public int HealthToIncrease { get; protected set; } = healthToIncrease;
        public ActionType ActionType { get; protected set; } = actionType;
        public ICharacter Target { get; protected set; } = target;
        public IDiceLogInfo DiceLogInfo { get; protected set; } = diceLogInfo;       
    }
}