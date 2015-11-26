namespace Assets.Scripts.Game
{
    using Assets.Scripts.Player;

    using CarbonCore.Utils.Unity.Logic;
    
    public class Components : UnitySingleton<Components>
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Components()
        {
            this.Player = new PlayerComponent();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public PlayerComponent Player { get; private set; }

        public GameComponent[] GetComponents()
        {
            return new GameComponent[] { this.Player };
        }
    }
}
