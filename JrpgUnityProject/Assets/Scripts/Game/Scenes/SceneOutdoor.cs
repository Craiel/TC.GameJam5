namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Gameplay.Map;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Logic.Resource;

    public class SceneOutdoor : GameScene
    {
        private OutdoorComponent component;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneOutdoor()
            : base("Outdoor", "Outdoor")
        {
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Outdoor;
            }
        }

        // -------------------------------------------------------------------
        // Protected
        // -------------------------------------------------------------------
        protected override bool SceneRegisterResources1()
        {
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.MusicOverworldAssetKey);
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.SfxFootstepsAssetKey);
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.MapOutdoorTestAssetKey);
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.OutdoorMapDisplayAssetKey);
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.PrefabMapMeshAssetKey);
            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.MaterialMapAssetKey);
            
            return base.SceneRegisterResources1();
        }

        protected override bool ScenePostLoad()
        {
            // Create, initialize and register the game component that will drive the scene
            this.component = new OutdoorComponent();
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
