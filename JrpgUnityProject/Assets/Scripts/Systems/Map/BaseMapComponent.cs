namespace Assets.Scripts.Systems.Map
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using Assets.Scripts.Game;

    using CarbonCore.ContentServices.Compat.Logic.Enums;
    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    // Generic component dealing with map navigation and handling
    public abstract class BaseMapComponent : GameComponent
    {
        private readonly IList<MapRenderer> mapRenderers;

        private MapTileRegistry tileRegistry;

        public bool mapIsInvalid;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected BaseMapComponent()
        {
            this.mapRenderers = new List<MapRenderer>();
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
                this.RedrawMap();
                this.mapIsInvalid = false;
            }

            foreach (MapRenderer renderer in this.mapRenderers)
            {
                renderer.Update();
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
            this.mapRenderers.Clear();
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
            foreach (GameMapLayer layer in this.Map.Layers)
            {
                if (layer.Name.Equals(Constants.LayerSpecialCollision, StringComparison.OrdinalIgnoreCase))
                {
                    // Todo: handle collision separate
                    continue;
                }

                switch (layer.Type)
                {
                    case TiledMapLayerType.TileLayer:
                        {
                            // this is a tile layer, initialize a renderer for it and set all the required classes in place
                            SpriteRenderer layerRenderer = this.Display.RegisterLayer(layer.Name);
                            var mapRenderer = new MapRenderer(layer, layerRenderer, this.tileRegistry);
                            this.mapRenderers.Add(mapRenderer);
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

        protected virtual void RedrawMap()
        {
            // Todo
            // - change the display dimensions to the map rect we will display
            // - refresh the image of the area we are showing
            // - update the sprites of the display
        }
    }
}
