namespace CodingDojo.Combat
{
    public class GameConfig
    {
        public IDiceConfig NormalDice { get; set; } = new DiceConfig();
        public IDiceConfig MagicDice { get; set; } = new DiceConfig();
        public int MaxTurns { get; set; } = 100;
    }

    public class DiceConfig: IDiceConfig
    {
        public DiceConfig(int min =1 , int max = 6)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; protected set; }
        public int Max { get; protected set; }        
    }

    public interface IDiceConfig
    {
        int Min { get; } 
        int Max { get; }
    }
}