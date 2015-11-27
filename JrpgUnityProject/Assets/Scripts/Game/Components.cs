namespace Assets.Scripts.Game
{
    using System.Collections.Generic;

    using Assets.Scripts.Audio;
    using Assets.Scripts.Player;

    using CarbonCore.Utils.Unity.Logic;
    
    public class Components : UnitySingleton<Components>
    {
        private readonly IList<GameComponent> dynamicComponents;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Components()
        {
            this.dynamicComponents = new List<GameComponent>();

            // Create the static components
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

        public IList<GameComponent> GetComponents()
        {
            IList<GameComponent> result = new List<GameComponent>(this.dynamicComponents);
            result.Add(this.Audio);
            result.Add(this.Player);
            return result;
        }

        public void RegisterComponent(GameComponent component)
        {
            this.dynamicComponents.Add(component);
        }

        public void UnregisterComponent(GameComponent component)
        {
            this.dynamicComponents.Remove(component);
        }
    }
}
