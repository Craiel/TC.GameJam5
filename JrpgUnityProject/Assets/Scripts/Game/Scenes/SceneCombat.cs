namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Gameplay.Combat;
    using Assets.Scripts.Systems;

    public class SceneCombat : GameScene
    {
        private CombatComponent component;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneCombat()
            : base("Combat", "Combat")
        {
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Combat;
            }
        }

        protected override bool ScenePostLoad()
        {
            // Create, initialize and register the game component that will drive the scene
            this.component = new CombatComponent();
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
