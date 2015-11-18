namespace Jrpg.Game.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveDataActor
    {
        public long Level { get; set; }

        public long Gold { get; set; }

        public long Experience { get; set; }
    }
}
