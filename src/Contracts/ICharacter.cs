using CodingDojo.Combat.Characters;

namespace CodingDojo.Combat.Contracts
{
    public interface ICharacter
    {
        int Level { get; }
        string Name { get; }
        HealthPoints Health { get; }
        int Strength { get; }
        int Defense { get; }
        int Magic { get; }
        bool IsAlive { get; }
        CharacterJob Job { get; }
        ICharacter Copy();
    }
}