namespace Assets.Scripts.Systems.MapLogic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.Scripts.Game;
    using Assets.Scripts.Systems.Map;

    using CarbonCore.ContentServices.Compat.Logic.Enums;
    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    // Generic component dealing with map navigation and handling
    public abstract class BaseMapComponent : GameComponent
    {
        private readonly IList<MapLayer> layers;

        private MapTileRegistry tileRegistry;

        public bool mapIsInvalid;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected BaseMapComponent()
        {
            this.layers = new List<MapLayer>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public Vector2US MapSize
        {
            get
            {
                return this.Map.Size;
            }
        }

        public Vector2US MapTileSize
        {
            get
            {
                return this.Map.TileSize;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            this.tileRegistry = new MapTileRegistry();

            this.mapIsInvalid = true;
        }

        public override void Update()
        {
            base.Update();

            if (this.mapIsInvalid)
            {
                foreach (MapLayer layer in this.layers)
                {
                    layer.Invalidate();
                }

                this.mapIsInvalid = false;
            }
        }

        public void Invalidate()
        {
            this.mapIsInvalid = true;
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected GameMap Map { get; set; }

        protected MapDisplayBehavior Display { get; set; }

        protected void SetMap(ResourceKey resource)
        {
            Diagnostic.Assert(this.Display != null, "Display should be set before map!");

            // Clear out the display since we are changing the map
            this.tileRegistry.Clear();
            this.layers.Clear();
            this.Display.Reset();

            // Load the new map
            this.Map = GameMap.Load(resource);
            Diagnostic.Info("Loaded map {0}", this.Map.Name);

            // Build the tile registry
            foreach (GameTileSet tileSet in this.Map.TileSets)
            {
                tileRegistry.RegisterTileSet(tileSet);
            }

            // Register the layers
            foreach (GameMapLayer layerData in this.Map.Layers.Reverse())
            {
                if (layerData.Name.Equals(Constants.LayerSpecialCollision, StringComparison.OrdinalIgnoreCase))
                {
                    // Todo: handle collision separate
                    continue;
                }

                switch (layerData.Type)
                {
                    case TiledMapLayerType.TileLayer:
                        {
                            var layerObject = new GameObject(layerData.Name);
                            var layerInstance = layerObject.AddComponent<MapLayer>();
                            layerInstance.Initialize(layerData, this.tileRegistry, Constants.DefaultChunkSize);

                            this.Display.RegisterLayer(layerObject, layerInstance);
                            this.layers.Add(layerInstance);
                            break;
                        }
                }
            }

            this.Invalidate();
        }

        protected void SetDisplay(MapDisplayBehavior display)
        {
            this.Display = display;
        }
    }
}
