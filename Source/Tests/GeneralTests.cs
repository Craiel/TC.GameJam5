namespace Jrpg.Game.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class GeneralTests
    {
        [Test]
        public void GameUpdate()
        {
            // Run the Update cycle a couple of times
            for (var i = 0; i < 10; i++)
            {
                TestSetup.Game.Update(DateTime.Now.Ticks);
                System.Threading.Thread.Sleep(10);
            }
        }
    }
}
