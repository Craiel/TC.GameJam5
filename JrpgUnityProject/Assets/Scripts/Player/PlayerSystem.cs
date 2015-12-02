namespace Assets.Scripts.Player
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Game;

    using CarbonCore.Utils.Unity.Data;
    
    public class PlayerSystem : GameComponent
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public PlayerSystem()
        {
            this.OutdoorPosition = Vector2I.Zero;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public Vector2I OutdoorPosition { get; set; }

        public ulong Gold { get; set; }

        public override void Save(SaveData target)
        {
            base.Save(target);

            target.Player.OutdoorPosition = this.OutdoorPosition;
            target.Player.Gold = this.Gold;
        }

        public override void Load(SaveData source)
        {
            base.Load(source);

            this.OutdoorPosition = source.Player.OutdoorPosition;
            this.Gold = source.Player.Gold;
        }
    }
}
