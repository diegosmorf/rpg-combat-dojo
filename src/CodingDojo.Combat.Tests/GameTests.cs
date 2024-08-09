using CodingDojo.Combat.Domain;
using CodingDojo.Combat.Domain.Actions;
using CodingDojo.Combat.Domain.Characters;
using CodingDojo.Combat.Domain.Contracts;
using CodingDojo.Combat.Domain.Dices;
using CodingDojo.Combat.Domain.Turns;
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
            AssertDefaultPlayer(battle.Players[0]);
            AssertDefaultPlayer(battle.Players[1]);
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

        protected static void AssertDefaultPlayer(ICharacter player)
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

        [TestCase(1, 20)]
        [TestCase(2, 40)]
        [TestCase(3, 60)]
        [TestCase(4, 80)]
        [TestCase(5, 100)]
        [TestCase(6, 120)]
        public void When_RunAttack_Then_ApplyDamage(int diceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(diceValue));
            var actor = new Soldier("Soldier Actor");
            var target = new Soldier("Soldier Target");
            var turn = new Turn(new AttackAction(normalDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
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
            var normalDice = new FakeDice(new DiceConfig(diceValue));
            var actor = new Soldier("Soldier Actor");
            var target = new Soldier("Soldier Target");
            var turn = new Turn(new AttackAction(normalDice));

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(damage, Is.EqualTo(turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Actor, Is.Not.Null);
                Assert.That(turn.LogInfo.Target, Is.Not.Null);
                Assert.That(turn.LogInfo.Action, Is.EqualTo(ActionType.Attack));
            });
            AssertCharacterInfo(actor, turn.LogInfo.Actor);
            AssertCharacterInfo(target, turn.LogInfo.Target);
        }

        private static void AssertCharacterInfo(ICharacter player, ICharacter log)
        {
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(player.Name, Is.EqualTo(log.Name));
                Assert.That(player.Health.Value, Is.EqualTo(log.Health.Value));
                Assert.That(player.Strength, Is.EqualTo(log.Strength));
                Assert.That(player.Defense, Is.EqualTo(log.Defense));
                Assert.That(player.Magic, Is.EqualTo(log.Magic));
                Assert.That(player.Level, Is.EqualTo(log.Level));
                Assert.That(player.Job, Is.EqualTo(log.Job));
            });
        }

        [TestCase(1, 40)]
        [TestCase(2, 20)]
        [TestCase(3, 14)]
        [TestCase(4, 10)]
        [TestCase(5, 8)]
        [TestCase(6, 8)]
        public void When_Run_DefaultBattle_Then_RunTurns_Until_EndOfGame(int diceValue, int maxTurns)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(diceValue));                        
            var battle = new Battle(normalDice, maxTurns);
            battle.Players.Add(new Soldier("Soldier 1"));
            battle.Players.Add(new Soldier("Soldier 2"));

            //act                                    
            battle.Run();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(battle.HasFinished, Is.True);
                Assert.That(battle.ProcessedTurns, Is.EqualTo(maxTurns));
            });
        }
        
        [TestCase(1, 1, 35)]
        [TestCase(2, 2, 70)]
        [TestCase(3, 3, 105)]
        [TestCase(4, 4, 140)]
        [TestCase(5, 5, 175)]
        [TestCase(6, 6, 210)]
        [TestCase(7, 6, 265)]
        [TestCase(8, 6, 320)]
        public void When_Wizard_RunFireBall_To_Soldier_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Wizard("Wizard Actor");
            var target = new Soldier("Soldier Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
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
        public void When_Wizard_RunMagic_To_Knight_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Wizard("Wizard Actor");
            var target = new Knight("Knight Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }

        [TestCase(1, 1, 20)]
        [TestCase(2, 2, 40)]
        [TestCase(3, 3, 60)]
        [TestCase(4, 4, 80)]
        [TestCase(5, 5, 100)]
        [TestCase(6, 6, 120)]
        [TestCase(7, 6, 175)]
        [TestCase(8, 6, 230)]
        public void When_Wizard_RunFireBall_To_Archer_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Wizard("Wizard Actor");
            var target = new Archer("Archer Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }

        [TestCase(1, 1, 15)]
        [TestCase(2, 2, 30)]
        [TestCase(3, 3, 45)]
        [TestCase(4, 4, 60)]
        [TestCase(5, 5, 75)]
        [TestCase(6, 6, 90)]
        [TestCase(7, 6, 125)]
        [TestCase(8, 6, 160)]
        public void When_Archer_RunFireBall_To_Soldier_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Archer("Archer Actor");
            var target = new Soldier("Soldier Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }

        [TestCase(1, 1, 20)]
        [TestCase(2, 2, 40)]
        [TestCase(3, 3, 60)]
        [TestCase(4, 4, 80)]
        [TestCase(5, 5, 100)]
        [TestCase(6, 6, 120)]
        [TestCase(7, 6, 155)]
        [TestCase(8, 6, 190)]
        public void When_Archer_RunFireBall_To_Knight_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Archer("Archer Actor");
            var target = new Knight("Knight Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
            });
        }

        [TestCase(1, 1, 0)]
        [TestCase(2, 2, 0)]
        [TestCase(3, 3, 0)]
        [TestCase(4, 4, 0)]
        [TestCase(5, 5, 0)]
        [TestCase(6, 6, 0)]
        [TestCase(7, 6, 0)]
        [TestCase(8, 6, 0)]
        [TestCase(8, 1, 225)]
        [TestCase(8, 2, 170)]
        [TestCase(8, 3, 115)]
        [TestCase(8, 4, 60)]
        [TestCase(8, 5, 5)]
        public void When_Archer_RunFireBall_To_Wizard_Then_ApplyDamage(int magicDiceValue, int normalDiceValue, int damage)
        {
            //arrange            
            var normalDice = new FakeDice(new DiceConfig(normalDiceValue));
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Archer("Archer Actor");
            var target = new Wizard("Wizard Target");
            var turn = new Turn(new MagicFireBallAction(normalDice, magicDice));
            var health = target.Health.Value;

            //act            
            turn.Run(actor, target);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(target.Health.Value, Is.EqualTo(health - turn.LogInfo.Damage));
                Assert.That(turn.LogInfo.Damage, Is.EqualTo(damage));
                Assert.That(turn.LogInfo.Action, Is.EqualTo(ActionType.MagicFireBall));
            });
        }


        [TestCase(1, 300, 116)]
        [TestCase(2, 300, 133)]
        [TestCase(3, 300, 149)]
        [TestCase(4, 300, 166)]
        [TestCase(5, 300, 182)]
        [TestCase(6, 300, 199)]
        [TestCase(7, 300, 215)]
        [TestCase(8, 300, 232)]
        public void When_Wizard_RunHealMagic_Then_IncreaseHealth(int magicDiceValue, int damage, int expectedHealth)
        {
            //arrange            
            var magicDice = new FakeDice(new DiceConfig(magicDiceValue));

            var actor = new Wizard("Wizard Actor");
            var turn = new Turn(new MagicHealAction(magicDice));

            //act            
            actor.ApplyDamage(damage);
            turn.Run(actor, actor);
            //assert
            Assert.That(turn.LogInfo, Is.Not.Null);

            Assert.Multiple(() =>
            {
                Assert.That(actor.Health.Value, Is.EqualTo(expectedHealth));
                Assert.That(turn.LogInfo.Action, Is.EqualTo(ActionType.MagicHeal));
            });
        }

        [TestCase(CharacterJob.Soldier, CharacterJob.Soldier, 100)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Archer, 100)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Wizard, 100)]
        [TestCase(CharacterJob.Soldier, CharacterJob.Knight, 100)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Soldier, 100)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Archer, 100)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Wizard, 100)]
        [TestCase(CharacterJob.Wizard, CharacterJob.Knight, 100)]
        [TestCase(CharacterJob.Archer, CharacterJob.Soldier, 100)]
        [TestCase(CharacterJob.Archer, CharacterJob.Archer, 100)]
        [TestCase(CharacterJob.Archer, CharacterJob.Wizard, 100)]
        [TestCase(CharacterJob.Archer, CharacterJob.Knight, 100)]
        [TestCase(CharacterJob.Knight, CharacterJob.Soldier, 100)]
        [TestCase(CharacterJob.Knight, CharacterJob.Archer, 100)]
        [TestCase(CharacterJob.Knight, CharacterJob.Wizard, 100)]
        [TestCase(CharacterJob.Knight, CharacterJob.Knight, 100)]
        public void When_Run_Battle_Then_RunTurns_Until_EndOfGame(CharacterJob job1, CharacterJob job2, int maxTurns)
        {
            //arrange
            var game = new Game();
            var player1 = CreatePlayer(job1);
            var player2 = CreatePlayer(job2);
            var players = new[] { player1, player2 };
            var battle = game.SetupBattle(players, maxTurns);
            
            //act                        
            battle.Run();

            //assert
            AssertBattleHasFinished(battle, maxTurns);
        }

        private static ICharacter CreatePlayer(CharacterJob job) => job switch
        {
            CharacterJob.Wizard => new Wizard("Wizard"),
            CharacterJob.Soldier => new Soldier("Soldier"),
            CharacterJob.Archer => new Archer("Archer"),
            CharacterJob.Knight => new Knight("Knight"),
            _ => throw new ArgumentException("Invalid Job")
        };

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
    }
}