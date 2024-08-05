using CodingDojo.Combat.Characters;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat
{
    public class Game(GameConfig config)
    {
        protected readonly GameConfig config = config;

        public int ProcessedTurns { get { return Turns.Count; } }
        public Character? Winner { get; private set; }
        public Character? Looser { get; private set; }
        public bool IsEndOfGame { get { return Winner != null || ProcessedTurns >= config.MaxTurns; } }
        public List<Character> Players { get; private set; } = [];
        public List<Turn> Turns { get; private set; } = [];
        public void RunTurns()
        {
            int indexActor = 0;
            int indexTarget = 1;

            while (!IsEndOfGame)
            {
                RunTurn(Players[indexActor], Players[indexTarget]);

                if (indexActor == 1)
                {
                    indexActor = 0;
                    indexTarget = 1;
                }
                else
                {
                    indexActor = 1;
                    indexTarget = 0;
                }
            }
        }
        public void RunTurn(Character actor, Character target)
        {
            var turn = new AttackTurn(config);
            turn.Run(actor,target);

            if (!target.IsAlive)
            {
                Winner = actor;
                Looser = target;
            }

            Turns.Add(turn);
        }
        public void DefaultSetup()
        {
            AddPlayer("Soldier 1");
            AddPlayer("Soldier 2");
        }
        protected void AddPlayer(string name)
        {            
            Players.Add(new Soldier(name));
        }
    }
}