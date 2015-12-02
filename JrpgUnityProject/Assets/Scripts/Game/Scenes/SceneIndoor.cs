namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Gameplay.Map;
    using Assets.Scripts.Systems;

    public class SceneIndoor : GameScene
    {
        private IndoorController controller;

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
            // Create, initialize and register the game controller that will drive the scene
            this.controller = new IndoorController();
            this.controller.Initialize();
            Components.Instance.RegisterComponent(this.controller);

            return base.ScenePostLoad();
        }

        protected override bool ScenePreDestroy()
        {
            // Unregister the main game controller for the scene
            Components.Instance.UnregisterComponent(this.controller);
            this.controller.Destroy();
            this.controller = null;

            return base.ScenePreDestroy();
        }
    }
}
