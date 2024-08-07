namespace CodingDojo.Combat.Characters
{

    public class Character : ICharacter
    {
        public int Level { get; protected set; } = 1;
        public string Name { get; protected set; }
        public InfoPoints Health { get; protected set; }
        public int Strength { get; protected set; }
        public int Defense { get; protected set; }
        public int Magic { get; protected set; }
        public CharacterJob Job { get; protected set; }
        public bool IsAlive { get { return Health.Value > 0; } }

        public Character(string name = "", int health = 0, int strength = 0, int defense = 0, int magic = 0, CharacterJob job = CharacterJob.Soldier)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty");

            if (name.Length > 20)
                throw new ArgumentException("Name max length is 20 chars");

            Name = name;
            Health = new InfoPoints(health);
            Strength = strength;
            Defense = defense;
            Magic = magic;
            Job = job;
        }


        public ICharacter Copy()
        {
            return new Character(Name, Health.Value, Strength, Defense, Magic, Job);
        }
    }
}