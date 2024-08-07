using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class MagicHealTurn(IDice magicDice) : Turn(TurnAction.MagicHeal)
    {
        private readonly IDice magicDice = magicDice;

        public override void Run(ICharacter actor, ICharacter target)
        {           
            var actorDiceValue = magicDice.Roll();            

            var healthToIncrease = (actor.Magic * actorDiceValue);
            target.Health.Value += healthToIncrease;

            this.Log(actor, actorDiceValue, healthToIncrease);
        }
    }
}