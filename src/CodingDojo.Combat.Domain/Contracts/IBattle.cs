namespace CodingDojo.Combat.Domain.Contracts
{
    public interface IBattle
    {
        int ProcessedTurns { get; }
        ICharacter? Winner { get; }
        ICharacter? Looser { get; }
        bool HasFinished { get; }
        List<ICharacter> Players { get; }
        List<ITurn> Turns { get; }
        void RunAutomatic();
    }
}