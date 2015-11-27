namespace Assets.Scripts.Gameplay.Map
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Compat.Diagnostics;

    public class OutdoorComponent : BaseMapComponent
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Initialize()
        {
            base.Initialize();

            GameMap test = GameMap.Load(AssetResourceKeys.MapOverworldTest);
            Diagnostic.Info("Loaded map {0}", test.Name);

            // Play the overworld music
            // Todo: move to update cycle instead of initialize
            Components.Instance.Audio.BeginPlay(AssetResourceKeys.MusicOverworldAssetKey, GameAudioType.Music);
        }

        public override void Destroy()
        {
            // Stop all audio
            Components.Instance.Audio.Stop();

            base.Destroy();
        }
    }
}
