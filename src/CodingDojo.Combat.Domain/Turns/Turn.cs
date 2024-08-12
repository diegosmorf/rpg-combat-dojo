using CodingDojo.Combat.Domain.Contracts;

namespace CodingDojo.Combat.Domain.Turns
{
    public class Turn(IBaseAction action) : ITurn
    {
        protected readonly IBaseAction action = action;

        public ITurnLogInfo Run(ICharacter actor, ICharacter target)
        {
            return action.Run(actor, target);
        }
    }
}