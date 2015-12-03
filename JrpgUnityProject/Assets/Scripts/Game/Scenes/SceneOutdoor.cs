namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Gameplay.Map;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Logic.Resource;

    public class SceneOutdoor : GameScene
    {
        private OutdoorController controller;

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

            Components.Instance.Map.RegisterMap(AssetResourceKeys.MapOutdoorTestAssetKey);
            
            return base.SceneRegisterResources1();
        }

        protected override bool SceneLoad()
        {
            if (Components.Instance.ContinueLoad())
            {
                return true;
            }

            return base.SceneLoad();
        }

        protected override bool ScenePostLoad()
        {
            // Create, initialize and register the game controller that will drive the scene
            this.controller = new OutdoorController();
            this.controller.Initialize();
            Components.Instance.RegisterComponent(this.controller);

            return base.ScenePostLoad();
        }

        protected override bool ScenePreDestroy()
        {
            // Un-register the main game controller for the scene
            Components.Instance.UnregisterComponent(this.controller);
            this.controller.Destroy();
            this.controller = null;

            return base.ScenePreDestroy();
        }

        protected override bool ScenePostDestroy()
        {
            // Clear out the map data, no longer needed
            Components.Instance.Map.Clear();

            return base.ScenePostDestroy();
        }
    }
}
