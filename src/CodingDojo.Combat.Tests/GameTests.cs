using CodingDojo.Combat.Domain;
using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Dices;
using CodingDojo.Combat.Tests.Factories;
using NUnit.Framework;

namespace CodingDojo.Combat.Tests
{
    public class GameTests
    {
        [Test]
        public void When_Game_SetupBattle_Then_One_Battle()
        {
            //arrange
            var game = new Game();
            var numberOfBattles = 1;
            //act
            game.SetupBattle();

            //assert
            Assert.That(game.Battles, Has.Exactly(numberOfBattles).Items);
        }

        [Test]
        public void When_Create_DefaultBattle_Then_Two_Players()
        {
            //arrange
            var game = new Game();
            var numberOfPlayers = 2;
            //act
            var battle = game.SetupBattle();

            //assert
            Assert.That(battle.Players, Has.Exactly(numberOfPlayers).Items);
        }

        [Test]
        public void When_Create_DefaultBattle_Then_Two_Soldiers()
        {
            //arrange
            var game = new Game();

            //act
            var battle = game.SetupBattle();

            //assert
            CharacterTestFactory.AssertSoldierPlayer(battle.Players[0]);
            CharacterTestFactory.AssertSoldierPlayer(battle.Players[1]);
        }

        [TestCase(1, 40)]
        [TestCase(2, 20)]
        [TestCase(3, 14)]
        [TestCase(4, 10)]
        [TestCase(5, 8)]
        [TestCase(6, 8)]
        public void When_Run_DefaultBattle_Then_RunTurns_Until_TheEnd(int diceValue, int maxTurns)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(diceValue));
            var battle = new Battle(normalDice, maxTurns);
            battle.Players.Add(new Soldier("Soldier 1"));
            battle.Players.Add(new Soldier("Soldier 2"));

            //act                                    
            battle.RunAutomatic();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(battle.HasFinished, Is.True);
                Assert.That(battle.ProcessedTurns, Is.EqualTo(maxTurns));
            });
        }

        [TestCase(CharacterJob.Soldier, CharacterJob.Soldier)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Archer)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Wizard)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Knight)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Soldier)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Archer)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Wizard)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Knight)]
        [TestCase(CharacterJob.Archer, CharacterJob.Soldier)]
        [TestCase(CharacterJob.Archer, CharacterJob.Archer)]
        [TestCase(CharacterJob.Archer, CharacterJob.Wizard)]
        [TestCase(CharacterJob.Archer, CharacterJob.Knight)]
        [TestCase(CharacterJob.Knight, CharacterJob.Soldier)]
        [TestCase(CharacterJob.Knight, CharacterJob.Archer)]
        [TestCase(CharacterJob.Knight, CharacterJob.Wizard)]
        [TestCase(CharacterJob.Knight, CharacterJob.Knight)]
        public void When_Run_Battle_Then_RunTurns_Until_TheEnd(CharacterJob job1, CharacterJob job2)
        {
            //arrange
            var maxTurns = 100;
            var game = new Game();
            var player1 = CharacterTestFactory.CreatePlayer(job1);
            var player2 = CharacterTestFactory.CreatePlayer(job2);
            var players = new[] { player1, player2 };
            var battle = game.SetupBattle(players, maxTurns);

            //act                        
            battle.RunAutomatic();

            //assert
            AssertBattleHasFinished(battle, maxTurns);
        }

        private static void AssertBattleHasFinished(Battle battle, int maxTurns)
        {
            Assert.Multiple(() =>
            {
                Assert.That(battle.Winner, Is.Not.Null);
                Assert.That(battle.Looser, Is.Not.Null);
            });

            Assert.Multiple(() =>
            {
                Assert.That(battle.Winner.IsAlive, Is.True);
                Assert.That(battle.Winner.Health.Value, Is.GreaterThan(0));
                Assert.That(battle.Looser.IsAlive, Is.False);
                Assert.That(battle.Looser.Health.Value, Is.EqualTo(0));
                Assert.That(battle.HasFinished, Is.True);
                Assert.That(battle.ProcessedTurns, Is.LessThan(maxTurns));
            });
        }

        [Test]
        public void When_Run_Battle_Soldier_Soldier_Then_RunTurns_Until_TheEnd()
        {
            //arrange
            var maxTurns = 100;
            var game = new Game();
            var player1 = new Soldier("Soldier 1");
            var player2 = new Soldier("Soldier 2");
            var players = new[] { player1, player2 };
            var battle = game.SetupBattle(players, maxTurns);

            //act                        
            battle.RunAutomatic();

            //assert
            AssertBattleHasFinished(battle, maxTurns);
        }

        [Test]
        public void When_Run_Battle_Then_RunTurns_Until_TheEnd_With_DefaultPlayers()
        {
            //arrange
            var maxTurns = 100;
            var game = new Game();
            var battle = game.SetupBattle(maxTurns);

            //act                        
            battle.RunAutomatic();

            //assert
            AssertBattleHasFinished(battle, maxTurns);
        }
    }
}