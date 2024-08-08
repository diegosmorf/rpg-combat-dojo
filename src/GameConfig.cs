using CodingDojo.Combat.Contracts;

namespace CodingDojo.Combat
{
    public class GameConfig : IGameConfig
    {
        public GameConfig(IDice normalDice, ITurnConfig turnConfig) : this(normalDice, normalDice, turnConfig)
        { }


        public GameConfig(IDice normalDice, IDice magicDice, ITurnConfig turnConfig)
        {
            NormalDice = normalDice;
            MagicDice = magicDice;
            TurnConfig = turnConfig;
        }

        public IDice NormalDice { get; protected set; }
        public IDice MagicDice { get; protected set; }
        public ITurnConfig TurnConfig { get; protected set; }        
    }
}