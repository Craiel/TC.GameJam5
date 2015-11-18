namespace Jrpg.Game.Tests
{
    using System;
    using System.Collections;

    using Jrpg.Game.Contracts;

    public class TestLink : IUnityLink
    {
        private readonly Random random;

        public TestLink()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
        }

        public void Run(IEnumerator coroutine)
        {
            throw new NotImplementedException();
        }

        public float RandomRange(float min, float max)
        {
            return (float)this.random.NextDouble() * (max - min) + min;
        }
    }
}
