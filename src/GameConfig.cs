using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat
{
    public class GameConfig(IDice normalDice, IDice magicDice, ITurnConfig turnConfig) : IGameConfig
    {
        public IDice NormalDice { get; protected set; } = normalDice;
        public IDice MagicDice { get; protected set; } = magicDice;
        public ITurnConfig TurnConfig { get; protected set; } = turnConfig;
    }
}