using CodingDojo.Combat.Contracts;
using CodingDojo.Combat.Dices;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat.Actions
{
    public class AttackAction(IDice normalDice) : BaseAction(ActionType.Attack)
    {
        private readonly IDice normalDice = normalDice;

        public override ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = normalDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Strength, target.Defense, actorDiceValue, targetDiceValue);
            target.Health.Value -= damage;

            return new TurnLogInfo()
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
    }
}