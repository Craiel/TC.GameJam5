namespace Jrpg.Game.Tests
{
    using NUnit.Framework;

    using Jrpg.Game.Contracts;

    [SetUpFixture]
    public class TestSetup
    {
        public static IGame Game { get; private set; }

        [SetUp]
        public void Setup()
        {
            Game = Core.Factory.Resolve<IGame>();
            Assert.NotNull(Game);

            Game.UnityLink = new TestLink();
            Game.Initialize();
            Assert.IsTrue(Game.IsInitialized);

            // Call a single update tick to reload the settings
            Game.Update(0f);
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
