namespace CodingDojo.Combat.Contracts
{
    public interface IBaseAction
    {
        ITurnLogInfo Run(ICharacter actor, ICharacter target);

    }
}