using CodingDojo.Combat.Characters;
using CodingDojo.Combat.Turns;

namespace CodingDojo.Combat.Tests
{
    public class GameTests
    {
        [Test]
        public void When_Default_Game_Start_Then_Two_Players_Created()
        {
            //arrange
            var config = new GameConfig();
            var game = new Game(config);
            var numberOfPlayers = 2;
            //act
            game.DefaultSetup();

            //assert
            Assert.That(game.Players, Has.Exactly(numberOfPlayers).Items);
        }

        [Test]
        public void When_Default_Game_Start_Then_Two_Soldiers_Created()
        {
            //arrange
            var config = new GameConfig();
            var game = new Game(config);

            //act
            game.DefaultSetup();

            //assert
            AssertDefaultPlayer(game.Players[0]);
            AssertDefaultPlayer(game.Players[1]);
        }

        [Test]
        public void When_Create_Valid_Player_Then_Validate_Name()
        {
            //arrange
            var name = "Invalid Name has more than twenty characters";
            var message = "Name max length is 20 chars";

            //act
            var exception = Assert.Throws<ArgumentException>(() => new Character(name));

            //assert            
            Assert.That(exception.Message, Is.EqualTo(message));

        }
        protected static void AssertDefaultPlayer(Character player)
        {
            //arrange
            var soldier = new Soldier();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Health, Is.EqualTo(soldier.Health));
                Assert.That(player.Level, Is.EqualTo(soldier.Level));
                Assert.That(player.Strength, Is.EqualTo(soldier.Strength));
                Assert.That(player.Defense, Is.EqualTo(soldier.Defense));
                Assert.That(player.IsAlive, Is.True);
                Assert.That(player.Job, Is.EqualTo(soldier.Job));
                Assert.That(player.GetType(), Is.EqualTo(soldier.GetType()));
            });
        }

        [TestCase(1, 20)]
        [TestCase(2, 40)]
        [TestCase(3, 60)]
        [TestCase(4, 80)]
        [TestCase(5, 100)]
        [TestCase(6, 120)]
        public void When_RunAttack_Then_ApplyDamage(int diceValue, int damage)
        {
            //arrange            
            var config = new GameConfig() { NormalDice = new DiceConfig(diceValue, diceValue) };

            var actor = new Soldier("Soldier Actor");
            var target = new Soldier("Soldier Target");
            var turn = new AttackAction(config);
            var health = target.Health;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(target.Health, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }

        [TestCase(1, 20)]
        [TestCase(2, 40)]
        [TestCase(3, 60)]
        [TestCase(4, 80)]
        [TestCase(5, 100)]
        [TestCase(6, 120)]
        public void When_RunAttack_Soldier_Then_LogInfo(int diceValue, int damage)
        {
            //arrange            
            var config = new GameConfig() { NormalDice = new DiceConfig(diceValue, diceValue) };

            var actor = new Soldier("Soldier Actor");
            var target = new Soldier("Soldier Target");
            var turn = new AttackAction(config);

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(damage, Is.EqualTo(turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Actor, Is.Not.Null);
                Assert.That(turn.LogInfo.Target, Is.Not.Null);
            });
            AssertCharacterInfo(actor, turn.LogInfo.Actor);
            AssertCharacterInfo(target, turn.LogInfo.Target);
        }

        private static void AssertCharacterInfo(ICharacter info1, ICharacter info2)
        {
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(info1.Name, Is.EqualTo(info2.Name));
                Assert.That(info1.Health, Is.EqualTo(info2.Health));
                Assert.That(info1.Strength, Is.EqualTo(info2.Strength));
                Assert.That(info1.Defense, Is.EqualTo(info2.Defense));
                Assert.That(info1.Level, Is.EqualTo(info2.Level));
                Assert.That(info1.Job, Is.EqualTo(info2.Job));
            });
        }

        [TestCase(1, 39)]
        [TestCase(2, 19)]
        [TestCase(3, 13)]
        [TestCase(4, 9)]
        [TestCase(5, 7)]
        [TestCase(6, 7)]
        public void When_Game_Start_Then_RunTurns_Until_EndOfGame(int diceValue, int maxTurns)
        {
            //arrange            
            var config = new GameConfig() { NormalDice = new DiceConfig(diceValue, diceValue), MaxTurns = maxTurns };
            var game = new Game(config);

            //act                        
            game.DefaultSetup();
            game.RunTurns();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(game.IsEndOfGame, Is.True);
                Assert.That(game.ProcessedTurns, Is.EqualTo(maxTurns));
            });
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        public void When_Game_Ends_Then_Valid_Winner_And_Looser(int diceValue)
        {
            //arrange            
            var config = new GameConfig() { NormalDice = new DiceConfig(diceValue, diceValue) };
            var game = new Game(config);

            //act                        
            game.DefaultSetup();
            game.RunTurns();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(game.Winner, Is.Not.Null);
                Assert.That(game.Looser, Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                Assert.That(game.Winner.IsAlive, Is.True);
                Assert.That(game.Winner.Health, Is.GreaterThan(0));
                Assert.That(game.Looser.IsAlive, Is.False);
                Assert.That(game.Looser.IsAlive, Is.False);
                Assert.That(game.Looser.Health, Is.EqualTo(0));
            });

        }

        [TestCase(1,1, 35)]
        [TestCase(2,2, 70)]
        [TestCase(3,3, 105)]
        [TestCase(4,4, 140)]
        [TestCase(5,5, 175)]
        [TestCase(6,6, 210)]
        [TestCase(7,6, 265)]
        [TestCase(8,6, 320)]
        public void When_RunMagic_To_Soldier_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var config = new GameConfig()
            {
                NormalDice = new DiceConfig(normalDiceValue, normalDiceValue),
                MagicDice = new DiceConfig(magicDiceValue, magicDiceValue)
            };

            var actor = new Wizard("Wizard Actor");
            var target = new Soldier("Soldier Target");
            var turn = new MagicFireBallAction(config);
            var health = target.Health;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }


        [TestCase(1, 1, 40)]
        [TestCase(2, 2, 80)]
        [TestCase(3, 3, 120)]
        [TestCase(4, 4, 160)]
        [TestCase(5, 5, 200)]
        [TestCase(6, 6, 240)]
        [TestCase(7, 6, 295)]
        [TestCase(8, 6, 350)]
        public void When_RunMagic_To_Knight_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var config = new GameConfig()
            {
                NormalDice = new DiceConfig(normalDiceValue, normalDiceValue),
                MagicDice = new DiceConfig(magicDiceValue, magicDiceValue)
            };

            var actor = new Wizard("Wizard Actor");
            var target = new Knight("Knight Target");
            var turn = new MagicFireBallAction(config);
            var health = target.Health;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }
    }
}