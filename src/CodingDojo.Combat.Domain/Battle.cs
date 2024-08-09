using CodingDojo.Combat.Domain.Actions;
using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Turns;

namespace CodingDojo.Combat.Domain
{
    public class Battle(IDice normalDice, IDice magicDice, int maxTurns = 100) : IBattle
    {
        protected readonly IDice magicDice = magicDice;
        protected readonly IDice normalDice = normalDice;
        protected readonly int maxTurns = maxTurns;

        public Battle(IDice normalDice, int maxTurns = 100) : this(normalDice, normalDice, maxTurns)
        {
        }

        public int ProcessedTurns { get { return Turns.Count; } }
        public ICharacter? Winner { get; private set; }
        public ICharacter? Looser { get; protected set; }
        public bool HasFinished { get { return Winner != null || ProcessedTurns >= maxTurns; } }
        public List<ICharacter> Players { get; protected set; } = [];
        public List<ITurn> Turns { get; protected set; } = [];
        public void Run()
        {
            while (!HasFinished)
            {
                foreach (var actor in Players)
                {
                    var target = Players.Single(p => p != actor);
                    var action = GetAction(actor, target);

                    if (action is MagicHealAction)
                        target = actor;

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
            if (actor.Health.Value <= 50)
            {
                return new MagicHealAction(magicDice);
            }

            if (actor is Wizard)
            {
                return new MagicFireBallAction(normalDice, magicDice);
            }

            if (actor is Archer && target is not Wizard)
            {
                return new MagicFireBallAction(normalDice, magicDice);
            }

            return new AttackAction(normalDice);
        }
    }
}