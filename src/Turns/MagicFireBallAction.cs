using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public class MagicFireBallAction(GameConfig config) : Turn(config, TurnAction.MagicFireBall)
    {
        public override void Run(ICharacter actor, ICharacter target)
        {   
            var magicDice = new Dice(config.MagicDice);
            var normalDice = new Dice(config.NormalDice);            
            var actorDiceValue = magicDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Magic, target.Magic, actorDiceValue, targetDiceValue);
            target.ApplyDamage(damage);

            this.Log(actor, target, damage, actorDiceValue, targetDiceValue);
        }
    }
}