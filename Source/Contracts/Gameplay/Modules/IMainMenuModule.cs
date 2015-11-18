namespace Jrpg.Game.Contracts.Gameplay.Modules
{
    using Jrpg.Game.Contracts.Logic;

    public interface IMainMenuModule : IGameModule
    {
        void StartGame();

        void QuitGame();
    }
}
