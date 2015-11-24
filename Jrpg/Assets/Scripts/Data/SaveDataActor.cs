namespace Jrpg.Game.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveDataActor
    {
        public string Name { get; set; }

        public long Level { get; set; }

        public long Experience { get; set; }
    }
}
