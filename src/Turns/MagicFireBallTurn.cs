using CodingDojo.Combat.Characters;
using static System.Net.Mime.MediaTypeNames;

namespace CodingDojo.Combat.Turns
{
    public class MagicFireBallTurn(GameConfig config) : Turn(config, TurnAction.MagicFireBall)
    {
        public override void Run(ICharacter actor, ICharacter target)
        {   
            var magicDice = new Dice(config.MagicDice);
            var normalDice = new Dice(config.NormalDice);            
            var actorDiceValue = magicDice.Roll();
            var targetDiceValue = normalDice.Roll();

            var damage = CalculateDamage(actor.Magic, target.Magic, actorDiceValue, targetDiceValue);
            target.Health.Value -= damage;

            this.Log(actor, target, damage, actorDiceValue, targetDiceValue);
        }
    }
}