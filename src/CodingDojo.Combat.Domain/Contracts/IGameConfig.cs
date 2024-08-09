namespace CodingDojo.Combat.Domain.Contracts
{
    public interface IGameConfig
    {
        IDice NormalDice { get; }
        IDice MagicDice { get; }
        ITurnConfig TurnConfig { get; }
    }
}