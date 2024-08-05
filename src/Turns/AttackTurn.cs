using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public class AttackTurn(GameConfig config) : Turn(config, TurnAction.Attack)
    {
        public override void Run(ICharacter actor, ICharacter target)
        {            
            var normalDice = new Dice(config.NormalDice);
            var actorDiceValue = normalDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Strength, target.Defense, actorDiceValue, targetDiceValue);
            target.ApplyDamage(damage);

            Log(actor, target, damage, actorDiceValue, targetDiceValue);
        }        
    }
}