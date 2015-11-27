namespace Assets.Scripts
{
    using UnityEngine;

    public static class Constants
    {
        public const int MaxLevel = 99;

        public const float FpsUpdateInterval = 0.5f;

        public const string GameName = "Unnamed Jrpg";
        
        public const string DataFile = "GameData.json";

        public const string SaveKey = "GameState";

        public const string NameNotSet = "NAME_UNSET";

        public const string FpsFormat = "{0:#0} Fps";

        public static readonly Vector2 Version = new Vector2(0, 1);
    }
}
