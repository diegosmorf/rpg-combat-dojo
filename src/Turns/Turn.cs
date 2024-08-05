using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public interface ITurn
    {
        ITurnLogInfo? LogInfo { get; }
        void Run(ICharacter actor, ICharacter target);
    }

    public abstract class Turn(GameConfig config, TurnAction action = TurnAction.Attack) : ITurn
    {
        protected readonly GameConfig config = config;
        protected readonly TurnAction action = action;        
        
        public ITurnLogInfo? LogInfo { get; protected set; }

        public abstract void Run(ICharacter actor, ICharacter target);

        protected static int CalculateDamage(int actorValue, int targetValue, int actorDiceValue, int targetDiceValue)
        {
            var damage = (actorValue * actorDiceValue) - (targetValue * targetDiceValue);

            if (damage < 0)
            {
                damage = 0;
            }

            return damage;
        }

        protected void Log(ICharacter actor, ICharacter target, int damage, int actorDiceValue, int targetDiceValue)
        {
            LogInfo = new TurnLogInfo()
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