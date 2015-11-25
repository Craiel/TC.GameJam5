namespace Jrpg.Game.Data
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveData
    {
        public SaveDataPlayer Player { get; set; }

        public IDictionary<string, SaveDataActor> ActorData { get; set; }
    }
}
