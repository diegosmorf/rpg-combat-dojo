using CodingDojo.Combat.Actions;
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
        public ICharacter? Looser { get; protected set; }
        public bool IsEndOfGame { get { return Winner != null || ProcessedTurns >= config.TurnConfig.MaxTurns; } }
        public List<ICharacter> Players { get; protected set; } = [];
        public List<ITurn> Turns { get; protected set; } = [];
        public void RunBattle()
        {
            while (!IsEndOfGame)
            {
                foreach (var actor in Players)
                {
                    var target = Players.Single(p => p != actor);

                    var action = GetAction(actor,target);
                    var turn = new Turn(action);
                    turn.Run(actor, target);
                    Turns.Add(turn);

                    if (!target.IsAlive)
                    {
                        Winner = actor;
                        Looser = target;
                        break;
                    }
                }
            }
        }

        private IBaseAction GetAction(ICharacter actor, ICharacter target)
        {
            if(actor.Health.Value <= 200)
            {
                target = actor;
                return new MagicHealAction(config.MagicDice);
            }

            if (actor is Wizard )
            {
                return new MagicFireBallAction(config.NormalDice, config.MagicDice);
            }

            if (actor is Archer && target is not Wizard)
            {
                return new MagicFireBallAction(config.NormalDice, config.MagicDice);
            }

            return new AttackAction(config.NormalDice);
        }

        public void DefaultSetup()
        {
            Players.Add(new Soldier("Soldier 1"));
            Players.Add(new Soldier("Soldier 2"));
        }
    }
}