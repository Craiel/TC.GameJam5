namespace Assets.Scripts.Gameplay.Combat
{
    using Assets.Scripts.Actors;

    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;
    public class CombatSlot : MonoBehaviour
    {
        private int actorID;

        private Animator animator;

        public void Initialize(CharacterActor actor)
        {
            // Set up the prefab in scene and all that crap

            // instantiate prefab
            using (var resource = ResourceProvider.Instance.AcquireResource<GameObject>(actor.PrefabKey))
            {
                GameObject instance = Instantiate(resource.Data);
                //set the animator and set its default state to faceing left
                this.animator = instance.GetComponent<Animator>();
                this.animator.SetTrigger("Left");

            }
            this.actorID = actor.ActorID;
            
        }

        public void Initialize(MonsterActor actor)
        {
        }
    }
}
