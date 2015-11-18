namespace Jrpg.Game.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveDataPlayer
    {
        public string Name { get; set; }
    }
}
