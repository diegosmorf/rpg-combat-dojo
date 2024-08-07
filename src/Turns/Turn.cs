using CodingDojo.Combat.Contracts;
using CodingDojo.Combat.Dices;

namespace CodingDojo.Combat.Turns
{
    public abstract class Turn(TurnAction action = TurnAction.Attack) : ITurn
    {
        protected readonly TurnAction action = action;

        public ITurnLogInfo LogInfo { get; protected set; } = new TurnLogInfo();

        public abstract void Run(ICharacter actor, ICharacter target);

        protected static int CalculateDamage(int actorValue, int targetValue, int actorDiceValue, int targetDiceValue)
        {
            var damage = (actorValue * actorDiceValue) - (targetValue * targetDiceValue);

            if (damage < 0)
            {
                damage = 0;
            }

            return damage;
        }

        protected void Log(ICharacter actor, ICharacter target, int damage, int actorDiceValue, int targetDiceValue)
        {
            LogInfo = new TurnLogInfo()
            {
                Actor = actor.Copy(),
                Target = target.Copy(),
                Damage = damage,
                Dice = new DiceLogInfo()
                {
                    ActorValue = actorDiceValue,
                    TargetValue = targetDiceValue
                }
            };
        }

        protected void Log(ICharacter actor, int actorDiceValue, int healthToIncrease)
        {
            LogInfo = new TurnLogInfo()
            {
                Actor = actor.Copy(),
                HealthToIncrease = healthToIncrease,
                Dice = new DiceLogInfo()
                {
                    ActorValue = actorDiceValue
                }
            };
        }
    }
}