using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Dices;
using CodingDojo.Combat.Domain.Turns;

namespace CodingDojo.Combat.Domain.Actions
{
    public class MagicHealAction(IDice magicDice) : BaseAction(ActionType.MagicHeal)
    {
        private readonly IDice magicDice = magicDice;

        public override ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = magicDice.Roll();

            var healthToIncrease = (int)((actor.Magic * actorDiceValue) * 0.3);
            var experience = CalculateExperience(actor.Health.Value, healthToIncrease);

            target.ApplyHeal(healthToIncrease);
            actor.IncreaseExperience(experience);

            return new TurnLogInfo()
            {
                Action = actionType,
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