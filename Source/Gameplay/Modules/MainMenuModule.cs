namespace Jrpg.Game.Gameplay.Modules
{
    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts;
    using Jrpg.Game.Contracts.Gameplay.Modules;
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;

    public class MainMenuModule : GameModule, IMainMenuModule
    {
        private readonly IFactory factory;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public MainMenuModule(IFactory factory)
        {
            this.factory = factory;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void StartGame()
        {
            this.factory.Resolve<IGame>().QueueStateChange(GameState.Combat);
        }

        public void QuitGame()
        {
            // Save game
            this.factory.Resolve<IGame>().SaveGame();
        }
    }
}
