namespace Jrpg.Game.Contracts
{
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay.Enums;

    public interface IGame : IGameComponent
    {
        GameState State { get; set; }
        
        void QueueStateChange(GameState state);

        void SaveGame();
    }
}
