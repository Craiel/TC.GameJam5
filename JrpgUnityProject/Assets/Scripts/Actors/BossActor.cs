namespace Assets.Scripts.Actors
{
    using CarbonCore.Utils.Unity.Data;

    public class BossActor : BaseMonsterActor
    {
        public BossActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }
    }
}
