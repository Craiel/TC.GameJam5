namespace Assets.Scripts.Game
{
    using Assets.Scripts.Audio;
    using Assets.Scripts.Player;

    using CarbonCore.Utils.Unity.Logic;
    
    public class Components : UnitySingleton<Components>
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Components()
        {
            this.Audio = new AudioComponent();
            this.Player = new PlayerComponent();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public AudioComponent Audio { get; private set; }

        public PlayerComponent Player { get; private set; }

        public void Initialize()
        {
            this.Audio.Initialize();
            this.Player.Initialize();
        }

        public void Update()
        {
            this.Audio.Update();
            this.Player.Update();
        }

        public GameComponent[] GetComponents()
        {
            return new GameComponent[] { this.Audio, this.Player };
        }
    }
}
