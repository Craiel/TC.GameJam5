namespace Jrpg.Game.Gameplay
{
    using System;

    using CarbonCore.Utils.Compat.Contracts;
    using CarbonCore.Utils.Compat.Contracts.IoC;
    using CarbonCore.Utils.Compat.IO;
    using CarbonCore.Utils.Compat.Json;

    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Data;
    using Jrpg.Game.Logic;
    using Jrpg.Game.Logic.Events;

    using Constants = Jrpg.Game.Constants;

    public class GameData : GameComponent, IGameData
    {
        private readonly IJsonAssetLoader assetLoader;
        private readonly IEventRelay eventRelay;

        private CoreData data;

        private bool isLoading;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameData(IFactory factory)
        {
            this.assetLoader = factory.Resolve<IJsonAssetLoader>();
            this.eventRelay = factory.Resolve<IEventRelay>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool IsLoaded { get; private set; }
        
        public void Reload()
        {
            // Todo: Disabled for now, not sure we'll be needing this for the jrpg

            /*if (this.isLoading)
            {
                return;
            }

            this.eventRelay.Relay(new EventDebugMessage("Reloading Game Data"));

            this.isLoading = true;
            this.IsLoaded = false;

            if (Core.IsRunningUnitTest)
            {
                CarbonDirectory current = new CarbonDirectory(System.IO.Directory.GetCurrentDirectory());
                CarbonFile file = current.ToFile(@"..\..\..\..\..\Jrpg\Assets\StreamingAssets\" + Constants.DataFile);
                this.OnReload(file.ReadAsString());
            }
            else
            {
                this.assetLoader.LoadJson(Constants.DataFile, this.OnReload);
            }*/
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnReload(string gameData)
        {
            if (!string.IsNullOrEmpty(gameData))
            {
                try
                {
                    this.data = JsonExtensions.LoadFromData<CoreData>(gameData);
                    this.IsLoaded = true;
                    this.isLoading = false;
                }
                catch (Exception e)
                {
                    this.eventRelay.Relay(new EventDebugMessage("Unknown Error in Reload: {0}", e));
                }

                return;
            }

            System.Diagnostics.Trace.TraceError("Reload failed!");
            this.eventRelay.Relay(new EventDebugMessage("Reload Failed"));
        }
    }
}
