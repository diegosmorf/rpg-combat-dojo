namespace CodingDojo.Combat
{
    public class GameConfig
    {
        public IDiceConfig NormalDice { get; set; } = new DiceConfig();
        public IDiceConfig MagicDice { get; set; } = new DiceConfig();
        public int MaxTurns { get; set; } = 100;
    }
}