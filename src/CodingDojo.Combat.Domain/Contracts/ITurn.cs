namespace CodingDojo.Combat.Domain.Contracts
{

    public interface ITurn
    {
        ITurnLogInfo LogInfo { get; }
        void Run(ICharacter actor, ICharacter target);
    }
}