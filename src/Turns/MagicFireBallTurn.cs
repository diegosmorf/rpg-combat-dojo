using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class MagicFireBallTurn(IDice normalDice, IDice magicDice) : Turn( TurnAction.MagicFireBall)
    {
        private readonly IDice normalDice = normalDice;
        private readonly IDice magicDice = magicDice;

        public override void Run(ICharacter actor, ICharacter target)
        {            
            var actorDiceValue = magicDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Magic, target.Magic, actorDiceValue, targetDiceValue);
            target.Health.Value -= damage;

            this.Log(actor, target, damage, actorDiceValue, targetDiceValue);
        }
    }
}