namespace Assets.Scripts.Maps
{
    using System.Collections.Generic;

    using Assets.Scripts.Game;
    using Assets.Scripts.Systems.MapLogic;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Enums;

    public class MapSystem : GameComponent
    {
        private readonly IDictionary<string, GameMap> mapNameLookup;
        private readonly Queue<ResourceKey> pendingMaps;

        private GameMap currentLoadingMap;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapSystem()
        {
            this.mapNameLookup = new Dictionary<string, GameMap>();
            this.pendingMaps = new Queue<ResourceKey>();

            this.Maps = new List<GameMap>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IList<GameMap> Maps { get; private set; }

        public void RegisterMap(ResourceKey resource)
        {
            this.pendingMaps.Enqueue(resource);
        }

        public void Clear()
        {
            // Clear out all data
            this.Maps.Clear();
            this.mapNameLookup.Clear();
            this.pendingMaps.Clear();

            this.currentLoadingMap = null;
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override bool DoContinueLoad(DelayedLoadedObjectPhase phase)
        {
            if (this.LoadMap())
            {
                return true;
            }

            return base.DoContinueLoad(phase);
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private bool LoadMap()
        {
            if (this.currentLoadingMap != null)
            {
                if (this.currentLoadingMap.ContinueLoad())
                {
                    return true;
                }

                if (this.mapNameLookup.ContainsKey(this.currentLoadingMap.Name))
                {
                    Diagnostic.Warning("Duplicate map loaded: {0}", this.currentLoadingMap.Name);
                }
                else
                {
                    this.mapNameLookup.Add(this.currentLoadingMap.Name, this.currentLoadingMap);
                    this.Maps.Add(this.currentLoadingMap);
                    this.currentLoadingMap = null;
                }
            }

            if (this.pendingMaps.Count > 0)
            {
                this.currentLoadingMap = GameMap.Create(this.pendingMaps.Dequeue());
                return true;
            }

            return false;
        }
    }
}
