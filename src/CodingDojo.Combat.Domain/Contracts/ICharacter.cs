using CodingDojo.Combat.Domain.Characters;

namespace CodingDojo.Combat.Domain.Contracts
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
        void ApplyDamage(int damage);
        void ApplyHeal(int health);
        void IncreaseExperience(int experience);
    }
}