using CodingDojo.Combat.Characters;
using CodingDojo.Combat.Contracts;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat
{
    public class Game(IGameConfig config)
    {
        protected readonly IGameConfig config = config;

        public int ProcessedTurns { get { return Turns.Count; } }
        public ICharacter? Winner { get; private set; }
        public ICharacter? Looser { get; private set; }
        public bool IsEndOfGame { get { return Winner != null || ProcessedTurns >= config.TurnConfig.MaxTurns; } }
        public List<ICharacter> Players { get; private set; } = [];
        public List<ITurn> Turns { get; private set; } = [];
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
        public void RunTurn(ICharacter actor, ICharacter target)
        {
            var turn = new AttackTurn(config.NormalDice);
            turn.Run(actor, target);

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