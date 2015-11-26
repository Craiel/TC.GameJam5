namespace Assets.Scripts.Data
{
    using Newtonsoft.Json;

    [JsonObject(MemberSerialization.OptOut)]
    public class SaveDataCharacter
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SaveDataCharacter()
        {
            this.ResetToDefault();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; set; }

        public ushort Level { get; set; }

        public ulong Experience { get; set; }

        public ushort Str { get; set; }
        public ushort Int { get; set; }

        public ushort Hp { get; set; }
        public ushort HpTotal { get; set; }

        public ushort Mp { get; set; }
        public ushort MpTotal { get; set; }

        public void ResetToDefault()
        {
            this.Name = Constants.NameNotSet;
            this.Level = 1;
            this.Experience = 0;
            this.Str = 1;
            this.Int = 1;
            this.Hp = 1;
            this.HpTotal = 1;
            this.Mp = 1;
            this.MpTotal = 1;
        }
    }
}
