namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using Assets.Scripts.Systems.Map;

    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.Utils.Compat.Json;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class GameMap
    {
        private readonly TiledMapData data;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameMap(TiledMapData data, ResourceKey resourceKey)
        {
            this.data = data;

            this.ResourceKey = resourceKey;
            this.Name = resourceKey.Path;

            this.Size = new Vector2US(data.Width, data.Height);
            this.TileSize = new Vector2US(data.TileWidth, data.TileHeight);

            // Create the layer data
            this.Layers = new List<GameMapLayer>();
            foreach (TiledMapLayerData layer in data.Layers)
            {
                this.Layers.Add(GameMapLayer.Create(layer, this.TileSize));
            }

            // Create the tileset data
            this.TileSets = new List<GameTileSet>();
            foreach (TiledMapTileset tileset in data.Tilesets)
            {
                this.TileSets.Add(GameTileSet.Create(tileset));
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ResourceKey ResourceKey { get; private set; }

        public string Name { get; private set; }

        public Vector2US Size { get; private set; }

        public Vector2US TileSize { get; private set; }

        public IList<GameMapLayer> Layers { get; private set; }

        public IList<GameTileSet> TileSets { get; private set; }

        public static GameMap Load(ResourceKey resourceKey)
        {
            using (var resource = ResourceProvider.Instance.AcquireResource<TextAsset>(resourceKey))
            {
                TiledMapData data = JsonExtensions.LoadFromData<TiledMapData>(resource.Data.text);
                return new GameMap(data, resourceKey);
            }
        }
    }
}