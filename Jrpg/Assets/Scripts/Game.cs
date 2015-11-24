namespace Jrpg.Game
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Utils.Compat.Contracts.IoC;
    using CarbonCore.Utils.Compat.Json;
    using CarbonCore.Utils.Unity.Logic;

    using Jrpg.Game.Contracts;
    using Jrpg.Game.Contracts.Gameplay.Modules;
    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Data;
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;

    public class Game : GameComponent, IGame
    {
        private readonly IGameData gameData;

        private readonly IMainMenuModule mainMenuModule;
        private readonly ICombatModule combatModule;

        private readonly Queue<GameState> stateChangeQueue;

        private IGameModule activeModule;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Game(IFactory factory)
        {
            this.gameData = factory.Resolve<IGameData>();

            this.mainMenuModule = factory.Resolve<IMainMenuModule>();
            this.combatModule = factory.Resolve<ICombatModule>();

            this.State = GameState.BeforeInitialize;

            this.stateChangeQueue = new Queue<GameState>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public GameState State { get; set; }
        
        public void QueueStateChange(GameState state)
        {
            lock (this.stateChangeQueue)
            {
                if (this.stateChangeQueue.Count > 0 && this.stateChangeQueue.Peek() == state)
                {
                    System.Diagnostics.Trace.TraceWarning("State change to {0} already next in line, skipping!", state);
                    return;
                }

                this.stateChangeQueue.Enqueue(state);
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            // Setup the trace listener if it's not up yet (only in debug)
#if DEBUG
            UnityDebugTraceListener.Setup();
#endif

            this.gameData.Initialize();

            // Initialize all the modules
            this.mainMenuModule.Initialize();
            this.combatModule.Initialize();
            
            System.Diagnostics.Trace.TraceInformation("Game Initialized!");

            // Load the state
            this.LoadGame();

            // Setup some basic intervals
            this.RegisterInterval(5f, this.OnAutoSave);

            // Queue the change to main menu
            this.QueueStateChange(GameState.MainMenu);
        }

        public override void Update(float currentTime)
        {
            // See if we want to change into a different state
            lock (this.stateChangeQueue)
            {
                if (this.stateChangeQueue.Count > 0)
                {
                    GameState newState = this.stateChangeQueue.Dequeue();
                    this.SwitchState(newState);
                }
            }

            // Bail out of Update before we initialized
            if (this.State == GameState.BeforeInitialize)
            {
                return;
            }

            // Skip all updates if game data is not loaded
            if (!this.gameData.IsLoaded)
            {
                this.gameData.Reload();
                return;
            }

            base.Update(currentTime);

            this.gameData.Update(currentTime);

            if (this.activeModule != null)
            {
                this.activeModule.Update(currentTime);
            }
        }

        public void SaveGame()
        {
            SaveData targetData = new SaveData();

            this.Save(targetData);
            this.mainMenuModule.Save(targetData);
            this.combatModule.Save(targetData);

            try
            {
                string serialized = JsonExtensions.SaveToData(targetData);
                UnityEngine.PlayerPrefs.SetString(Constants.SaveKey, serialized);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("Failed to save state: {0}", e);
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void SwitchState(GameState state)
        {
            System.Diagnostics.Trace.TraceWarning("Changing GameState from {0} to {1}", this.State, state);

            if (this.activeModule != null)
            {
                this.activeModule.Deactivate();
                this.activeModule = null;
            }

            switch (state)
            {
                case GameState.MainMenu:
                    {
                        this.activeModule = this.mainMenuModule;
                        break;
                    }

                case GameState.Combat:
                    {
                        this.activeModule = this.combatModule;
                        break;
                    }
            }

            if (this.activeModule != null)
            {
                this.activeModule.Activate();
            }

            this.State = state;
        }

        private void LoadGame()
        {
            string saveData = UnityEngine.PlayerPrefs.GetString(Constants.SaveKey);
            if (string.IsNullOrEmpty(saveData))
            {
                return;
            }

            try
            {
                SaveData data = JsonExtensions.LoadFromData<SaveData>(saveData);

                // Run all components through the load process
                this.Load(data);
                this.mainMenuModule.Load(data);
                this.combatModule.Load(data);
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError("Failed to load save state: {0}", e);
            }
        }

        private void OnAutoSave(float currentTime, IntervalTrigger trigger)
        {
            this.SaveGame();
        }
    }
}