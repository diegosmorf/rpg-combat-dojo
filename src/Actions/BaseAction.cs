using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Actions
{

    public abstract class BaseAction(ActionType action = ActionType.Attack) : IBaseAction
    {
        protected readonly ActionType action = action;

        public abstract ITurnLogInfo Run(ICharacter actor, ICharacter target);

        protected static int CalculateDamage(int actorValue, int targetValue, int actorDiceValue, int targetDiceValue)
        {
            var damage = ((actorValue * actorDiceValue) - (targetValue * targetDiceValue));

            return damage < 0 ? 0 : damage;
        }
    }
}