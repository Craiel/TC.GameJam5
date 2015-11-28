namespace Assets.Scripts.Game
{
    using System.Collections.Generic;

    using Assets.Scripts.Audio;
    using Assets.Scripts.Player;

    using CarbonCore.Utils.Unity.Logic;
    
    public class Components : UnitySingleton<Components>
    {
        private readonly IList<GameComponent> dynamicComponents;

        private bool isInitialized;

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

            this.isInitialized = true;
        }

        public void Update()
        {
            if (!this.isInitialized)
            {
                return;
            }

            this.Audio.Update();
            this.Player.Update();

            foreach (GameComponent component in this.dynamicComponents)
            {
                component.Update();
            }
        }

        public IList<GameComponent> GetComponents()
        {
            IList<GameComponent> result = new List<GameComponent>(this.dynamicComponents);
            result.Add(this.Audio);
            result.Add(this.Player);
            return result;
        }

        public T GetComponent<T>()
            where T : GameComponent
        {
            // TODO: this is slow and needs refactoring
            foreach (GameComponent component in this.GetComponents())
            {
                if (component.GetType() == typeof(T))
                {
                    return component as T;
                }
            }

            return default(T);
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
