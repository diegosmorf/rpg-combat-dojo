using CodingDojo.Combat.Contracts;
using CodingDojo.Combat.Dices;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat.Actions
{
    public class MagicHealAction(IDice magicDice) : BaseAction(ActionType.MagicHeal)
    {
        private readonly IDice magicDice = magicDice;

        public override ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = magicDice.Roll();

            var healthToIncrease = actor.Magic * actorDiceValue;
            target.Health.Value += healthToIncrease;

            return new TurnLogInfo()
            {
                Actor = actor.Copy(),
                Target = target.Copy(),
                HealthToIncrease = healthToIncrease,
                Dice = new DiceLogInfo()
                {
                    ActorValue = actorDiceValue
                }
            };
        }
    }
}