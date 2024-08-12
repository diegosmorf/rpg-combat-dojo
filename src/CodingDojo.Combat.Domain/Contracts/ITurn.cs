namespace CodingDojo.Combat.Domain.Contracts
{
    public interface ITurn
    {
        ITurnLogInfo Run(ICharacter actor, ICharacter target);
    }
}