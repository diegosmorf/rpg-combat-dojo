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
                Assert.That(player.Experience, Is.Zero);
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

        [Test]
        public void When_Create_Player_Empty_Name_Then_Raise_Exception()
        {
            //arrange            
            var message = "Name cannot be empty";

            //act
            var exception = Assert.Throws<ArgumentException>(() => new Character(""));

            //assert            
            Assert.That(exception.Message, Is.EqualTo(message));

        }

        [Test]
        public void When_Health_Set_Negative_Value_Then_Set_Zero()
        {
            //arrange
            var player = new Soldier();

            //act
            player.ApplyDamage(player.Health.MaxValue + 1);

            //assert            
            Assert.That(player.Health.Value, Is.EqualTo(0));

        }

        [Test]
        public void When_Health_Set_GreatThanMax_Value_Then_Set_MaxValue()
        {
            //arrange
            var player = new Soldier();

            //act
            player.ApplyHeal(player.Health.MaxValue + 1);

            //assert            
            Assert.That(player.Health.Value, Is.EqualTo(player.Health.MaxValue));

        }

        [Test]
        public void When_Health_Set_Zero_Value_Then_Player_IsDead()
        {
            //arrange
            var player = new Soldier();

            //act
            player.ApplyDamage(player.Health.MaxValue);

            //assert            
            Assert.That(player.IsAlive, Is.False);

        }

        [Test]
        public void When_Copy_Player_Then_Validate_Values()
        {
            //arrange
            var player = new Soldier("Soldier 1");

            //act
            var copy = player.Copy();

            //assert            
            Assert.Multiple(() =>
            {
                Assert.That(player.Health.Value, Is.EqualTo(copy.Health.Value));
                Assert.That(player.Level, Is.EqualTo(copy.Level));
                Assert.That(player.Strength, Is.EqualTo(copy.Strength));
                Assert.That(player.Defense, Is.EqualTo(copy.Defense));
                Assert.That(player.IsAlive, Is.True);
                Assert.That(player.Job, Is.EqualTo(copy.Job));
            });

        }

        [Test]
        public void When_ApplyDamage_Then_Validate_Health()
        {
            //arrange
            var player = new Soldier();
            var damage = 10;
            var expectedHealth = player.Health.Value - damage;

            //act
            player.ApplyDamage(damage);

            //assert            
            Assert.That(player.Health.Value, Is.EqualTo(expectedHealth));

        }

        [Test]
        public void When_ApplyHeal_Then_Validate_Health()
        {
            //arrange
            var player = new Soldier();
            player.Health.Value = 100;
            var heal = 10;
            var expectedHealth = player.Health.Value + heal;

            //act            
            player.ApplyHeal(heal);

            //assert            
            Assert.That(player.Health.Value, Is.EqualTo(expectedHealth));
        }

        [Test]
        public void When_IncreaseExperience_Then_Validate_LevelUp()
        {
            //arrange
            var player = new Soldier();
            var experience = LevelUpConfig.ExperienceToLevelUp;
            var expectedLevel = player.Level + 1;

            //act
            player.IncreaseExperience(experience);

            //assert            
            Assert.That(player.Level, Is.EqualTo(expectedLevel));
        }
    }
}