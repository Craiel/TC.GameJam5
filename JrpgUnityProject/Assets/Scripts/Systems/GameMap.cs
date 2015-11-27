namespace Assets.Scripts.Systems
{
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
            this.ResourceKey = resourceKey;
            this.data = data;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ResourceKey ResourceKey { get; private set; }

        public string Name
        {
            get
            {
                return this.ResourceKey.Path;
            }
        }

        public int Width
        {
            get
            {
                return this.data.Width;
            }
        }

        public int Height
        {
            get
            {
                return this.data.Height;
            }
        }

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
