namespace CodingDojo.Combat.Characters
{
    public interface ICharacter
    {
        int Level { get; }
        string Name { get; }
        InfoPoints Health { get; }
        int Strength { get; }
        int Defense { get; }
        int Magic { get; }
        bool IsAlive { get; }
        CharacterJob Job { get; }        
        ICharacter Copy();
    }
}