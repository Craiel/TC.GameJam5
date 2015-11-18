namespace Jrpg.Game.IoC
{
    using CarbonCore.Utils.Compat.IoC;

    using Jrpg.Game.Contracts;
    using Jrpg.Game.Contracts.Gameplay;
    using Jrpg.Game.Contracts.Gameplay.Modules;
    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay;
    using Jrpg.Game.Gameplay.Modules;
    using Jrpg.Game.Logic.AssetLoader;

    [DependsOnModule(typeof(UtilsCompatModule))]
    public class WorldsModule : CarbonQuickModule
    {
        public WorldsModule()
        {
            this.For<IGame>().Use<Game>().Singleton();
            this.For<IGameData>().Use<GameData>().Singleton();
            this.For<ICoreSystems>().Use<CoreSystems>().Singleton();
            this.For<ICombatSystems>().Use<CombatSystems>().Singleton();
            this.For<IJsonAssetLoader>().Use<JsonAssetLoader>().Singleton();

            // Register the Game Modules
            this.For<IMainMenuModule>().Use<MainMenuModule>().Singleton();
            this.For<ICombatModule>().Use<CombatModule>().Singleton();
            
            this.For<IPlayer>().Use<Player>();
            this.For<IEnemy>().Use<Enemy>();
        }
    }
}