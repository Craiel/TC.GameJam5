namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Gameplay.Map;
    using Assets.Scripts.Systems;

    public class SceneIndoor : GameScene
    {
        private IndoorComponent component;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneIndoor()
            : base("Indoor", "Indoor")
        {
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Indoor;
            }
        }

        protected override bool ScenePostLoad()
        {
            // Create, initialize and register the game component that will drive the scene
            this.component = new IndoorComponent();
            this.component.Initialize();
            Components.Instance.RegisterComponent(this.component);

            return base.ScenePostLoad();
        }

        protected override bool ScenePreDestroy()
        {
            // Unregister the main game component for the scene
            Components.Instance.UnregisterComponent(this.component);
            this.component.Destroy();
            this.component = null;

            return base.ScenePreDestroy();
        }
    }
}
