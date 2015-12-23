namespace Assets.Scripts.Actors
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Resource;
    using UnityEngine;

    using UnityEngineInternal;

    public class CharacterActor : CombatActor
    {
        public CharacterActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }

        private Animator characterAnimator;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Initialize()
        {
            base.Initialize();

            using (var resource = ResourceProvider.Instance.AcquireResource<GameObject>(this.PrefabKey))
            {
                GameObject instance = Object.Instantiate(resource.Data);
            }
        }
<<<<<<< HEAD

        
        
=======
>>>>>>> 9fef471e4d4678cc1c74baf16c6ac82a74f9c251
    }
}
