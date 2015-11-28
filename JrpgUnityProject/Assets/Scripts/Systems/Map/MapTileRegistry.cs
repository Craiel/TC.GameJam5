namespace Assets.Scripts.Systems.Map
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Utils.Unity.Data;
    
    public class MapTileRegistry
    {
        private readonly IDictionary<ushort, GameTileSet> tilesetLookup;
        private readonly IDictionary<ushort, ushort> tileIndexLookup;

        private ushort nextTileId;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapTileRegistry()
        {
            this.tilesetLookup = new Dictionary<ushort, GameTileSet>();
            this.tileIndexLookup = new Dictionary<ushort, ushort>();
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
                this.tileIndexLookup.Add(this.nextTileId, (ushort)i);
                this.nextTileId++;
            }
        }

        public void Clear()
        {
            this.tilesetLookup.Clear();
            this.nextTileId = 0;
        }

        public GameTileSet GetTile(ushort id, out Vector2US tileOffset)
        {
            // Given a id which is global to the registry we will find the right tileset
            // and then calculate the offset and size of the tile in question
            tileOffset = Vector2US.Zero;

            if (!this.tilesetLookup.ContainsKey(id))
            {
                return null;
            }

            GameTileSet result = this.tilesetLookup[id];
            ushort tileIndex = this.tileIndexLookup[id];
            
            ushort row = (ushort)(tileIndex / result.TilesPerRow);
            ushort column = (ushort)(tileIndex - (row * result.TilesPerRow));

            // We have to invert the row since it seems to load the texture upside down
            row = (ushort)(result.TilesPerColumn - row - 1);

            tileOffset = new Vector2US((ushort)(column * result.TileSize.X), (ushort)(row * result.TileSize.Y));

            return this.tilesetLookup[id];
        }
    }
}
