namespace CodingDojo.Combat.Contracts
{
    public interface IGameConfig
    {
        IDice NormalDice { get; }
        IDice MagicDice { get; }
        ITurnConfig TurnConfig { get; }        
    }
}