namespace Jrpg.Game.Tests
{
    using NUnit.Framework;

    using Jrpg.Game.Contracts.Gameplay;
    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Gameplay;
    using Jrpg.Game.Gameplay.Enums;

    [TestFixture]
    public class StatTests
    {
        [Test]
        public void PlayerStatTest()
        {
            var player = Core.Factory.Resolve<IPlayer>();
            player.Initialize();
            player.GainLevel(10, GainSourceEnum.Idle);
            player.Update(0f);

            // Check that the player has base stats
            Assert.IsTrue(player.GetMaxStat(StatEnum.Str) > 0);

            // Reset the player current stats back to max
            player.ResetCurrentStats();

            // Check that the player has health from trickeldown and that max == current
            Assert.IsTrue(player.GetCurrentStat(StatEnum.Hp) > 0);
            Assert.AreEqual(player.GetMaxStat(StatEnum.Hp), player.GetCurrentStat(StatEnum.Hp));
        }

        [Test]
        public void CombatTest()
        {
            var player = Core.Factory.Resolve<IPlayer>();
            player.Initialize();
            player.Update(0f);
            player.ResetCurrentStats();

            var enemy = Core.Factory.Resolve<IEnemy>();
            enemy.Initialize();
            enemy.Update(0f);
            enemy.ResetCurrentStats();

            CombatResult result = Core.Factory.Resolve<ICombatSystems>().ResolveCombat(new Ability("TEST"), player, enemy);
            Assert.Greater(result.Damage, 0f);
        }
    }
}
