using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Dices;
using CodingDojo.Combat.Domain.Turns;

namespace CodingDojo.Combat.Domain.Actions
{
    public class MagicFireBallAction(IDice normalDice, IDice magicDice) : BaseAction(ActionType.MagicFireBall)
    {
        private readonly IDice normalDice = normalDice;
        private readonly IDice magicDice = magicDice;

        public override ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = magicDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Magic, target.Magic, actorDiceValue, targetDiceValue);
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