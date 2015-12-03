namespace Assets.Scripts.Systems.MapLogic
{
    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class GameTileSet : DelayedLoadedObject
    {
        private static int nextId;

        private readonly ResourceKey tilesetResourceKey;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameTileSet(TiledMapTileset data)
        {
            this.Id = nextId++;

            this.Name = data.Name;
            this.TileCount = data.TileCount;

            this.Size = new Vector2I(data.ImageWidth, data.ImageHeight);
            this.TileSize = new Vector2I(data.TileWidth, data.TileHeight);

            this.TilesPerColumn = (ushort)(data.ImageHeight / data.TileHeight);
            this.TilesPerRow = (ushort)(data.ImageWidth / data.TileWidth);

            this.tilesetResourceKey = Constants.GetTilesetResourceKey(data.Name);
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public int Id { get; private set; }

        public string Name { get; private set; }
        
        public Texture2D Texture { get; private set; }

        public Vector2I Size { get; private set; }

        public Vector2I TileSize { get; private set; }

        public ushort TilesPerRow { get; private set; }

        public ushort TilesPerColumn { get; private set; }
        
        public ushort TileCount { get; private set; }

        public static GameTileSet Create(TiledMapTileset source)
        {
            return new GameTileSet(source);
        }

        public override bool ContinueLoad()
        {
            // Load the actual texture for the tileset
            using (var resource = ResourceProvider.Instance.AcquireOrLoadResource<Texture2D>(this.tilesetResourceKey))
            {
                this.Texture = resource.Data;
            }

            return base.ContinueLoad();
        }

        public void Destroy()
        {
            // Clear out the texture
            this.Texture = null;
        }
    }
}
