namespace Assets.Scripts.Systems
{
    using System;

    using Assets.Scripts.Enums;

    using CarbonCore.Utils.Unity.Data;

    public abstract class BaseActor
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public int ActorID { get; private set; }
        public ResourceKey PortraitKey { get; private set; }
        public ResourceKey SpriteKey { get; private set; }
        public ResourceKey PrefabKey { get; private set; }
        public bool IsInitialized { get; private set; }
        public ActorType ActorType { get; private set; }

        public virtual void Initialize(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
        {
            if (this.IsInitialized)
            {
                throw new InvalidOperationException(string.Format("Component {0} was already initialized", this.GetType().Name));
            }
            this.ActorID = id;
            this.PrefabKey = prefabKey;
            this.SpriteKey = spriteKey;
            this.PortraitKey = portraitKey;

            this.IsInitialized = true;
        }

        public virtual void Destroy()
        {
            this.IsInitialized = false;
        }
    }
}
