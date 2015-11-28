namespace Assets.Scripts.Data
{
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public static class AssetResourceKeys
    {
        public static readonly ResourceKey PrefabGameAudioSourceAssetKey = ResourceKey.Create<GameObject>("Sound/GameAudioSource");

        public static readonly ResourceKey MusicOverworldAssetKey = ResourceKey.Create<AudioClip>("Sound/Music/Overworld");

        public static readonly ResourceKey SfxAcceptAssetKey = ResourceKey.Create<AudioClip>("Sound/Effects/Accept");
        public static readonly ResourceKey SfxFootstepsAssetKey = ResourceKey.Create<AudioClip>("Sound/Effects/Footsteps");

        public static readonly ResourceKey MapOutdoorTestAssetKey = ResourceKey.Create<TextAsset>("MapData/Start");

        public static readonly ResourceKey OutdoorMapDisplayAssetKey = ResourceKey.Create<GameObject>("Outdoor/MapDisplay");
    }
}
