namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Logic.Resource;

    public class SceneOutdoor : GameScene
    {
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

            ResourceProvider.Instance.RegisterResource(AssetResourceKeys.MapOverworldTest);
            
            return base.SceneRegisterResources1();
        }

        protected override bool ScenePostLoad()
        {
            // Play the overworld music
            // Todo: we might wanna control this from whatever drives the overworld navigation
            Components.Instance.Audio.BeginPlay(AssetResourceKeys.MusicOverworldAssetKey, GameAudioType.Music);

            return base.ScenePostLoad();
        }

        protected override bool ScenePreDestroy()
        {
            // Stop all audio
            Components.Instance.Audio.Stop();

            return base.ScenePreDestroy();
        }
    }
}
