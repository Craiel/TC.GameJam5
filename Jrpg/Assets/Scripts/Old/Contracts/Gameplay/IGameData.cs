namespace Jrpg.Game.Contracts.GamePlay
{
    using Jrpg.Game.Contracts.Logic;

    public interface IGameData : IGameComponent
    {
        bool IsLoaded { get; }
        
        void Reload();
    }
}
