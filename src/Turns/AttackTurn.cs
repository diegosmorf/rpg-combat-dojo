using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class AttackTurn(IDice normalDice) : Turn(TurnAction.Attack)
    {
        private readonly IDice normalDice = normalDice;

        public override void Run(ICharacter actor, ICharacter target)
        {
            var actorDiceValue = normalDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Strength, target.Defense, actorDiceValue, targetDiceValue);
            target.Health.Value -= damage;

            Log(actor, target, damage, actorDiceValue, targetDiceValue);
        }
    }
}