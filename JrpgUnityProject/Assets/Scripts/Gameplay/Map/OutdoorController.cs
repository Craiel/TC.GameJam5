namespace Assets.Scripts.Gameplay.Map
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Systems;
    using Assets.Scripts.Systems.MapLogic;

    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class OutdoorController : BaseMapComponent
    {
        private bool initiateMusicUpdate;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Initialize()
        {
            base.Initialize();

            using (var resource = ResourceProvider.Instance.AcquireResource<GameObject>(AssetResourceKeys.OutdoorMapDisplayAssetKey))
            {
                GameObject instance = Object.Instantiate(resource.Data);
                this.SetDisplay(instance.GetComponent<MapDisplayBehavior>());
                SceneController.Instance.RegisterObjectAsRoot(SceneRootCategory.Dynamic, instance, false);
            }

            // Todo: fetch from current data instead of hardcoded
            // this.SetMap(AssetResourceKeys.MapOutdoorTestAssetKey);

            this.initiateMusicUpdate = true;
        }

        public override void Destroy()
        {
            // Stop all audio
            Components.Instance.Audio.Stop();

            base.Destroy();
        }

        public override void Update()
        {
            base.Update();

            // todo
            if (this.initiateMusicUpdate)
            {
                // Todo: play according to map / position
                Components.Instance.Audio.BeginPlay(AssetResourceKeys.MusicOverworldAssetKey, GameAudioType.Music, true);
                this.initiateMusicUpdate = false;
            }
        }
    }
}
