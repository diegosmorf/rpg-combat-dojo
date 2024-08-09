namespace CodingDojo.Combat.Domain.Contracts
{
    public interface IBaseAction
    {
        ITurnLogInfo Run(ICharacter actor, ICharacter target);

    }
}