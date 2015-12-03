namespace Assets.Scripts.Actors
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Data;

    public class NPCActor : WorldActor
    {
        public ShopType ShopType { get; private set; }
        public string ShopName { get; private set; }

        public NPCActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }
    }
}
