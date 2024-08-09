using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Dices;
using CodingDojo.Combat.Domain.Turns;

namespace CodingDojo.Combat.Domain.Actions
{
    public class AttackAction(IDice normalDice) : BaseAction(ActionType.Attack)
    {
        private readonly IDice normalDice = normalDice;

        public override ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = normalDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Strength, target.Defense, actorDiceValue, targetDiceValue);
            var experience = CalculateExperience(target.Health.Value, damage);

            target.ApplyDamage(damage);
            actor.IncreaseExperience(experience);

            return new TurnLogInfo()
            {
                Action = actionType,
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
    }
}