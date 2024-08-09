using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;
using NUnit.Framework;

namespace CodingDojo.Combat.Tests.Factories
{
    public static class CharacterTestFactory
    {
        public static void AssertSoldierPlayer(ICharacter player)
        {
            //arrange
            var soldier = new Soldier();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Health.Value, Is.EqualTo(soldier.Health.Value));
                Assert.That(player.Level, Is.EqualTo(soldier.Level));
                Assert.That(player.Strength, Is.EqualTo(soldier.Strength));
                Assert.That(player.Defense, Is.EqualTo(soldier.Defense));
                Assert.That(player.IsAlive, Is.True);
                Assert.That(player.Job, Is.EqualTo(soldier.Job));
                Assert.That(player.GetType(), Is.EqualTo(soldier.GetType()));
            });
        }

        public static ICharacter CreatePlayer(CharacterJob job) => job switch
        {
            CharacterJob.Wizard => new Wizard("Wizard"),
            CharacterJob.Soldier => new Soldier("Soldier"),
            CharacterJob.Archer => new Archer("Archer"),
            CharacterJob.Knight => new Knight("Knight"),
            _ => throw new ArgumentException("Invalid Job")
        };
    }
}