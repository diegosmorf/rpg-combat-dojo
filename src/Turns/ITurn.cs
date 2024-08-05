using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Turns
{
    public interface ITurn
    {
        ITurnLogInfo? LogInfo { get; }
        void Run(ICharacter actor, ICharacter target);
    }
}