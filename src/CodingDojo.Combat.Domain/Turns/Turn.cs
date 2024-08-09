using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Turns
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