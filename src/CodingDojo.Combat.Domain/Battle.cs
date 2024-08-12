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

        public int ProcessedTurns { get { return LogTurns.Count; } }
        public ICharacter? Winner { get; private set; }
        public ICharacter? Looser { get; protected set; }
        public bool HasFinished { get { return Winner != null || ProcessedTurns >= maxTurns; } }
        public List<ICharacter> Players { get; protected set; } = [];
        public List<ITurnLogInfo> LogTurns { get; protected set; } = [];
        public void RunAutomatic()
        {
            while (!HasFinished)
            {
                foreach (var actor in Players)
                {
                    var target = Players.Single(p => p != actor);
                    var action = GetAction(actor, target);

                    if (action is MagicHealAction)
                        target = actor;

                    RunTurn(actor, target, action);

                    if (!target.IsAlive)
                    {
                        break;
                    }
                }
            }
        }

        public void RunTurn(ICharacter actor, ICharacter target, IBaseAction action)
        {
            var turn = new Turn(action);
            var log = turn.Run(actor, target);
            LogTurns.Add(log);

            if (!target.IsAlive)
            {
                Winner = actor;
                Looser = target;
            }
        }

        private IBaseAction GetAction(ICharacter actor, ICharacter target)
        {
            if (actor.Health.Value <= 100)
            {
                return new MagicHealAction(magicDice);
            }

            if (actor is Wizard || actor is Archer)
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