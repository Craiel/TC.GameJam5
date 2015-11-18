namespace Jrpg.Game.Contracts
{
    using System.Collections;

    public interface IUnityLink
    {
        void Run(IEnumerator coroutine);

        float RandomRange(float min, float max);
    }
}
