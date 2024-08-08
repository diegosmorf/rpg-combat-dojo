using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat.Turns
{
    public class Turn(IBaseAction action) : ITurn
    {
        protected readonly IBaseAction action = action;

        public ITurnLogInfo LogInfo { get; protected set; } = new TurnLogInfo();

        public void Run(ICharacter actor, ICharacter target)
        {
            LogInfo = action.Run(actor, target);
        }
    }
}