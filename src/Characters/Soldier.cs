namespace CodingDojo.Combat.Characters
{
    public class Soldier(string name = "Default Soldier") :
        Character(name, 500, 40, 20, 20, CharacterJob.Soldier)
    {
    }

    public class Wizard(string name = "Default Wizard") :
       Character(name, 400, 10, 15, 55, CharacterJob.Wizard)
    {
    }

    public class Archer(string name = "Default Archer") :
       Character(name, 450, 45, 20, 35, CharacterJob.Archer)
    {
    }

    public class Knight(string name = "Default Knight") :
       Character(name, 550, 50, 30, 15, CharacterJob.Knight)
    {
    }
}