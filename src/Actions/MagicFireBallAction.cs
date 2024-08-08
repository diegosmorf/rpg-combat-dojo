using CodingDojo.Combat.Contracts;
using CodingDojo.Combat.Dices;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat.Actions
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