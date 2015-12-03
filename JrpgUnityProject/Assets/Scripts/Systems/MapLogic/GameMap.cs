namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.Utils.Compat.Json;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class GameMap : DelayedLoadedObject
    {
        private readonly Queue<TiledMapLayerData> pendingLayerData;
        private readonly Queue<TiledMapTileset> pendingTilesetData;

        private GameMapLayer currentLoadingLayer;
        private GameTileSet currentLoadingTileset;

        private int nextLayerOrder;
         
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameMap(TiledMapData data, ResourceKey resourceKey)
        {
            this.ResourceKey = resourceKey;
            this.Name = resourceKey.Path;

            this.Size = new Vector2I(data.Width, data.Height);
            this.TileSize = new Vector2I(data.TileWidth, data.TileHeight);

            this.Layers = new List<GameMapLayer>();
            this.TileSets = new List<GameTileSet>();

            // Queue up the tilesets for loading
            this.pendingTilesetData = new Queue<TiledMapTileset>();
            foreach (TiledMapTileset tileset in data.Tilesets)
            {
                this.pendingTilesetData.Enqueue(tileset);
            }

            // Queue up the layers for loading
            this.pendingLayerData = new Queue<TiledMapLayerData>();
            foreach (TiledMapLayerData layer in data.Layers)
            {
                this.pendingLayerData.Enqueue(layer);
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ResourceKey ResourceKey { get; private set; }

        public string Name { get; private set; }

        public Vector2I Size { get; private set; }

        public Vector2I TileSize { get; private set; }

        public IList<GameMapLayer> Layers { get; private set; }

        public IList<GameTileSet> TileSets { get; private set; }
        
        public static GameMap Create(ResourceKey resourceKey)
        {
            using (var resource = ResourceProvider.Instance.AcquireResource<TextAsset>(resourceKey))
            {
                TiledMapData data = JsonExtensions.LoadFromData<TiledMapData>(resource.Data.text);
                return new GameMap(data, resourceKey);
            }
        }

        public override bool ContinueLoad()
        {
            if (this.LoadTilesets())
            {
                return true;
            }

            if (this.LoadLayers())
            {
                return true;
            }

            return base.ContinueLoad();
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private bool LoadLayers()
        {
            if (this.currentLoadingLayer != null)
            {
                if (this.currentLoadingLayer.ContinueLoad())
                {
                    return true;
                }

                this.Layers.Add(this.currentLoadingLayer);
                this.currentLoadingLayer = null;
            }

            if (this.pendingLayerData.Count > 0)
            {
                this.currentLoadingLayer = GameMapLayer.Create(this.pendingLayerData.Dequeue(), this.TileSize, this.nextLayerOrder++);
                return true;
            }

            return false;
        }

        private bool LoadTilesets()
        {
            if (this.currentLoadingTileset != null)
            {
                if (this.currentLoadingTileset.ContinueLoad())
                {
                    return true;
                }

                this.TileSets.Add(this.currentLoadingTileset);
                this.currentLoadingTileset = null;
            }

            if (this.pendingTilesetData.Count > 0)
            {
                this.currentLoadingTileset = GameTileSet.Create(this.pendingTilesetData.Dequeue());
                return true;
            }

            return false;
        }
    }
}
