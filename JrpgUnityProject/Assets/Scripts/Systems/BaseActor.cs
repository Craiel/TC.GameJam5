namespace Assets.Scripts.Systems
{

    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public abstract class BaseActor : MonoBehaviour
    {
        public int ActorID { get; private set; }
        public string Name { get; private set; }
        public ResourceKey PortraitKey { get; private set; }
        public ResourceKey SpriteKey { get; private set; }
        public ResourceKey AnimatorKey { get; private set; }
    }
}
