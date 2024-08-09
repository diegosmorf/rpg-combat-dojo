namespace CodingDojo.Combat.Domain.Characters
{
    public class Soldier(string name = "Default Soldier") :
        Character(name, 500, 40, 20, 20, CharacterJob.Soldier)
    {
    }
}