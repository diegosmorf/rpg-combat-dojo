using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public class MagicHealTurn(GameConfig config) : Turn(config, TurnAction.MagicHeal)
    {
        public override void Run(ICharacter actor, ICharacter target)
        {
            var magicDice = new Dice(config.MagicDice);            
            var actorDiceValue = magicDice.Roll();            

            var healthToIncrease = (actor.Magic * actorDiceValue);
            target.Health.Value += healthToIncrease;

            this.Log(actor, actorDiceValue, healthToIncrease);
        }
    }
}