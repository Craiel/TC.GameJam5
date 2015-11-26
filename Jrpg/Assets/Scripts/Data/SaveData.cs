namespace Assets.Scripts.Data
{
    using System.Collections.Generic;
    
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveData
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SaveData()
        {
            this.ResetToDefault();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public SaveDataPlayer Player { get; set; }

        public List<SaveDataCharacter> CharacterData { get; set; }

        public void ResetToDefault()
        {
            this.Player = new SaveDataPlayer();
            this.CharacterData = new List<SaveDataCharacter>();
        }
    }
}
