namespace Assets.Scripts.Actors
{
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Data;

    public class PlayerActor : WorldActor
    {
        public string Status { get; private set; }

        public PlayerActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }
    }
}