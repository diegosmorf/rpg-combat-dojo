using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Actions
{

    public abstract class BaseAction(ActionType actionType = ActionType.Attack) : IBaseAction
    {
        protected readonly ActionType actionType = actionType;

        public abstract ITurnLogInfo Run(ICharacter actor, ICharacter target);

        protected static int CalculateDamage(int actorValue, int targetValue, int actorDiceValue, int targetDiceValue)
        {
            var damage = ((actorValue * actorDiceValue) - (targetValue * targetDiceValue));

            return damage < 0 ? 0 : damage;
        }
        protected static int CalculateExperience(int current, int change) => (int)(((double)change / current) * 20);
    }
}