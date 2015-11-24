namespace Jrpg.Game.Contracts.GamePlay
{
    using Jrpg.Game.Contracts.Logic;

    public interface IEnemy : IActor
    {
        void SetLevel(long level);
    }
}
