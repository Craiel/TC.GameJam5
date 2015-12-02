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
        public GameMapLayer(TiledMapLayerData data, Vector2I tileSize, int order)
        {
            this.data = data;

            this.Size = new Vector2I(data.Width, data.Height);
            this.TileSize = tileSize;
            this.Order = order;
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

        public Vector2I Size { get; private set; }

        public Vector2I TileSize { get; private set; }

        public int Order { get; private set; }

        public ushort[] Data
        {
            get
            {
                return this.data.Data;
            }
        }

        public static GameMapLayer Create(TiledMapLayerData source, Vector2I tileSize, int order)
        {
            return new GameMapLayer(source, tileSize, order);
        }
    }
}
