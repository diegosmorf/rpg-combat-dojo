namespace CodingDojo.Combat.Contracts
{

    public interface ITurn
    {
        ITurnLogInfo LogInfo { get; }
        void Run(ICharacter actor, ICharacter target);
    }
}