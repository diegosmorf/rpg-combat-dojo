using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Tests.Factories;
using NUnit.Framework;

namespace CodingDojo.Combat.Tests
{
    public class CharacterTests
    {
        [Test]
        public void When_Create_Valid_Player_Then_Validate_Name()
        {
            //arrange
            var name = "Default Soldier";

            //act
            var player = new Soldier(name);

            //assert            
            CharacterTestFactory.AssertSoldierPlayer(player);

        }

        [TestCase(CharacterJob.Soldier, 1)]
        [TestCase(CharacterJob.Soldier, 2)]
        [TestCase(CharacterJob.Soldier, 3)]
        [TestCase(CharacterJob.Soldier, 10)]
        [TestCase(CharacterJob.Knight, 1)]
        [TestCase(CharacterJob.Knight, 2)]
        [TestCase(CharacterJob.Knight, 3)]
        [TestCase(CharacterJob.Knight, 10)]
        [TestCase(CharacterJob.Wizard, 1)]
        [TestCase(CharacterJob.Wizard, 2)]
        [TestCase(CharacterJob.Wizard, 3)]
        [TestCase(CharacterJob.Wizard, 10)]
        [TestCase(CharacterJob.Archer, 1)]
        [TestCase(CharacterJob.Archer, 2)]
        [TestCase(CharacterJob.Archer, 3)]
        [TestCase(CharacterJob.Archer, 10)]
        public void When_Player_LevelUp_Then_Validate_Info(CharacterJob job, int numberOfLevelUp)
        {
            //arrange
            var player = CharacterTestFactory.CreatePlayer(job);
            var expectedHealth = player.Health.Value + LevelUpConfig.Health * numberOfLevelUp;
            var expectedStrength = player.Strength + LevelUpConfig.Strength * numberOfLevelUp;
            var expectedDefense = player.Defense + LevelUpConfig.Defense * numberOfLevelUp;
            var expectedMagic = player.Magic + LevelUpConfig.Magic * numberOfLevelUp;
            var expectedLevel = player.Level + 1 * numberOfLevelUp;

            //act
            for (int i = 0; i < numberOfLevelUp; i++)
            {
                player.IncreaseExperience(LevelUpConfig.ExperienceToLevelUp);
            }

            //assert            
            Assert.Multiple(() =>
            {
                Assert.That(player.Health.Value, Is.EqualTo(expectedHealth));
                Assert.That(player.Level, Is.EqualTo(expectedLevel));
                Assert.That(player.Strength, Is.EqualTo(expectedStrength));
                Assert.That(player.Defense, Is.EqualTo(expectedDefense));
                Assert.That(player.Magic, Is.EqualTo(expectedMagic));
                Assert.That(player.IsAlive, Is.True);
            });
        }

        [Test]
        public void When_Create_Player_Invalid_Name_Then_Raise_Exception()
        {
            //arrange
            var name = "Invalid Name has more than twenty characters";
            var message = "Name max length is 20 chars";

            //act
            var exception = Assert.Throws<ArgumentException>(() => new Character(name));

            //assert            
            Assert.That(exception.Message, Is.EqualTo(message));

        }
    }
}