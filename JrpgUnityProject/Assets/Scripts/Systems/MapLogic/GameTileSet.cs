namespace Assets.Scripts.Systems.MapLogic
{
    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class GameTileSet
    {
        private readonly TiledMapTileset data;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameTileSet(TiledMapTileset data)
        {
            this.data = data;

            this.Size = new Vector2I(data.ImageWidth, data.ImageHeight);
            this.TileSize = new Vector2I(data.TileWidth, data.TileHeight);

            this.TilesPerColumn = (ushort)(data.ImageHeight / data.TileHeight);
            this.TilesPerRow = (ushort)(data.ImageWidth / data.TileWidth);
            
            using (var resource = ResourceProvider.Instance.AcquireOrLoadResource<Texture2D>(Constants.GetTilesetResourceKey(data.Name)))
            {
                this.Texture = resource.Data;
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name
        {
            get
            {
                return this.data.Name;
            }
        }

        public Texture2D Texture { get; private set; }

        public Vector2I Size { get; private set; }

        public Vector2I TileSize { get; private set; }

        public ushort TilesPerRow { get; private set; }

        public ushort TilesPerColumn { get; private set; }
        
        public ushort TileCount
        {
            get
            {
                return this.data.TileCount;
            }
        }

        public static GameTileSet Create(TiledMapTileset source)
        {
            return new GameTileSet(source);
        }
    }
}
