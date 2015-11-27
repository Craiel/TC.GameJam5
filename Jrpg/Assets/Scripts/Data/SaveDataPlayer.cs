namespace Assets.Scripts.Data
{
    using CarbonCore.Utils.Unity.Logic.Json;

    using Newtonsoft.Json;

    using UnityEngine;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveDataPlayer
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SaveDataPlayer()
        {
            this.ResetToDefault();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public ulong Gold { get; set; }

        [JsonConverter(typeof(Vector2ConverterSmall))]
        public Vector2 OutdoorPosition { get; set; }

        public void ResetToDefault()
        {
            this.Gold = 0;

            this.OutdoorPosition = new Vector2(6, 2);
        }
    }
}
