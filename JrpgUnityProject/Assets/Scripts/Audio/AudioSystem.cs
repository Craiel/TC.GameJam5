namespace Assets.Scripts.Audio
{
    using System.Collections;
    using System.Collections.Generic;

    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public class AudioSystem : GameComponent
    {
        private readonly IDictionary<GameAudioType, AudioSource> audioSources;
        private readonly IDictionary<GameAudioType, bool> loopState;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public AudioSystem()
        {
            this.audioSources = new Dictionary<GameAudioType, AudioSource>();
            this.loopState = new Dictionary<GameAudioType, bool>();

            // Prepare the base state
            foreach (GameAudioType type in EnumLists.AudioTypes)
            {
                this.loopState.Add(type, false);
                this.audioSources.Add(type, null);
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Initialize()
        {
            using (var resource = ResourceProvider.Instance.AcquireOrLoadResource<GameObject>(AssetResourceKeys.PrefabGameAudioSourceAssetKey))
            {
                // Create an instance of the master audio source and register it as a root
                var audioSourceInstance = Object.Instantiate(resource.Data);
                SceneController.Instance.RegisterObjectAsRoot(SceneRootCategory.System, audioSourceInstance, true);

                var sourceData = audioSourceInstance.GetComponent<GameAudioSources>();
                this.audioSources[GameAudioType.Music] = sourceData.Music;
                this.audioSources[GameAudioType.Sfx] = sourceData.Sfx;
            }

            base.Initialize();
        }

        public void PlayOneShot(ResourceKey key, GameAudioType type)
        {
            this.Stop(type);

            using (var resource = ResourceProvider.Instance.AcquireResource<AudioClip>(key))
            {
                this.DoPlayOneshot(resource.Data, this.audioSources[type]);
            }
        }

        public void BeginPlay(ResourceKey key, GameAudioType type, bool loop = false)
        {
            this.Stop(type);

            using (var resource = ResourceProvider.Instance.AcquireResource<AudioClip>(key))
            {
                this.DoBeginPlay(resource.Data, this.audioSources[type]);
                this.loopState[type] = loop;
            }
        }

        public void Stop()
        {
            foreach (GameAudioType type in EnumLists.AudioTypes)
            {
                this.Stop(type);
            }
        }

        public void Stop(GameAudioType type)
        {
            if (this.audioSources[type].isPlaying)
            {
                this.audioSources[type].Stop();
            }

            this.audioSources[type].clip = null;
        }

        public override void Update()
        {
            foreach (GameAudioType type in this.loopState.Keys)
            {
                if (this.loopState[type])
                {
                    this.ContinuePlay(this.audioSources[type]);
                }
            }

            base.Update();
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void DoPlayOneshot(AudioClip clip, AudioSource source)
        {
            source.PlayOneShot(clip);
        }

        private void DoBeginPlay(AudioClip clip, AudioSource source)
        {
            source.clip = clip;
            source.Play();
        }

        private void ContinuePlay(AudioSource source)
        {
            if (source.clip == null || source.isPlaying)
            {
                return;
            }

            source.Play();
        }
    }
}
