namespace Assets.Scripts
{
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public static class Constants
    {
        public const int MaxLevel = 99;

        public const float FpsUpdateInterval = 0.5f;

        public const float DefaultMapUnit = 100.0f;

        public const ushort DefaultChunkSize = 4;

        public const ushort DefaultChunkRange = 5 * DefaultChunkSize;

        public const string GameName = "Unnamed Jrpg";
        
        public const string DataFile = "GameData.json";

        public const string SaveKey = "GameState";

        public const string NameNotSet = "NAME_UNSET";

        public const string FpsFormat = "{0:#0} Fps";

        public const string FolderTileSetRoot = "Tilesets";

        public const string LayerSpecialCollision = "Collision";

        public static readonly Color MapClearColor = new Color(0, 0, 0, 0);

        public static readonly Vector2 Version = new Vector2(0, 1);

        public static ResourceKey GetTilesetResourceKey(string name)
        {
            return ResourceKey.Create<Texture2D>(FolderTileSetRoot + '/' + name);
        }
    }
}
