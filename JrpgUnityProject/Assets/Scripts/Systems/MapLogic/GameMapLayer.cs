namespace Assets.Scripts.Systems.MapLogic
{
    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.ContentServices.Compat.Logic.Enums;
    using CarbonCore.Utils.Unity.Data;

    public class GameMapLayer
    {
        private readonly TiledMapLayerData data;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameMapLayer(TiledMapLayerData data, Vector2US tileSize)
        {
            this.data = data;

            this.Size = new Vector2US(data.Width, data.Height);
            this.TileSize = tileSize;
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

        public TiledMapLayerType Type
        {
            get
            {
                return this.data.Type;
            }
        }

        public Vector2US Size { get; private set; }

        public Vector2US TileSize { get; private set; }

        public ushort[] Data
        {
            get
            {
                return this.data.Data;
            }
        }

        public static GameMapLayer Create(TiledMapLayerData source, Vector2US tileSize)
        {
            return new GameMapLayer(source, tileSize);
        }
    }
}
