namespace Assets.Scripts.Systems.MapLogic
{
    using CarbonCore.ContentServices.Compat.Data.Tiled;
    using CarbonCore.ContentServices.Compat.Logic.Enums;
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic;

    public class GameMapLayer : DelayedLoadedObject
    {
        private TiledMapLayerData pendingData;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public GameMapLayer(TiledMapLayerData data, Vector2I tileSize, int order)
        {
            this.pendingData = data;

            this.Name = data.Name;
            this.Type = data.Type;
            this.Size = new Vector2I(data.Width, data.Height);
            this.TileSize = tileSize;
            this.Order = order;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; private set; }

        public TiledMapLayerType Type { get; private set; }

        public Vector2I Size { get; private set; }

        public Vector2I TileSize { get; private set; }

        public int Order { get; private set; }

        public static GameMapLayer Create(TiledMapLayerData source, Vector2I tileSize, int order)
        {
            return new GameMapLayer(source, tileSize, order);
        }

        public override bool ContinueLoad()
        {
            // TODO: Load the texture and re-format the data accordingly

            this.pendingData = null;

            return base.ContinueLoad();
        }

        public void Destroy()
        {
            // Todo: destroy the texture and clear the data to free up the memory
        }
    }
}
