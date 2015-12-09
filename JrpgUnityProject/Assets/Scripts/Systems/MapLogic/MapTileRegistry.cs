namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using CarbonCore.Utils.MathUtils;

    using UnityEngine;

    public class MapTileRegistry
    {
        private readonly IDictionary<int, GameTileSet> tilesetLookup;
        private readonly IDictionary<int, int> tileIndexLookup;
        private readonly IDictionary<GameMapLayer, MapLayerMaterial> materialLookup; 

        private ushort nextTileId;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapTileRegistry()
        {
            this.tilesetLookup = new Dictionary<int, GameTileSet>();
            this.tileIndexLookup = new Dictionary<int, int>();
            this.materialLookup = new Dictionary<GameMapLayer, MapLayerMaterial>();
        }
         
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void RegisterTileSet(GameTileSet tileset)
        {
            // TODO: this is easiest for lookup but probably not very efficient..
            for (var i = 0; i < tileset.TileCount; i++)
            {
                this.tilesetLookup.Add(this.nextTileId, tileset);
                this.tileIndexLookup.Add(this.nextTileId, i);
                this.nextTileId++;
            }
        }

        public void Clear()
        {
            this.tilesetLookup.Clear();
            this.nextTileId = 0;
        }

        public Material GetMaterial(GameMapLayer layer)
        {
            if (!this.materialLookup.ContainsKey(layer))
            {
                // No material for this layer yet, we need to build one
                //MapLayerMaterial material = MapRenderer.BuildLayerMaterial(layer, this);
                //this.materialLookup.Add(layer, material);
            }

            return this.materialLookup[layer].Material;
        }

        public GameTileSet GetTile(int tileId, out Vector2I tileOffset)
        {
            // Given a id which is global to the registry we will find the right tileset
            // and then calculate the offset and size of the tile in question
            tileOffset = Vector2I.Zero;

            if (!this.tilesetLookup.ContainsKey(tileId))
            {
                return null;
            }

            GameTileSet result = this.tilesetLookup[tileId];
            int tileIndex = this.tileIndexLookup[tileId];
            
            int row = tileIndex / result.TilesPerRow;
            int column = tileIndex - (row * result.TilesPerRow);

            // We have to invert the row since it seems to load the texture upside down
            row = result.TilesPerColumn - row - 1;

            tileOffset = new Vector2I(column * result.TileSize.X, row * result.TileSize.Y);

            return this.tilesetLookup[tileId];
        }
    }
}
