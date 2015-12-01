namespace Assets.Scripts.Data
{
    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Json;

    using Newtonsoft.Json;
    
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

        [JsonConverter(typeof(Vector2USConverterSmall))]
        public Vector2US OutdoorPosition { get; set; }

        public void ResetToDefault()
        {
            this.Gold = 0;

            this.OutdoorPosition = new Vector2US(6, 2);
        }
    }
}
