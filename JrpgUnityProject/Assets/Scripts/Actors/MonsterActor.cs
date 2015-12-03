namespace Assets.Scripts.Actors
{
    using CarbonCore.Utils.Unity.Data;

    public class MonsterActor : BaseMonsterActor
    {
        public MonsterActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }
    }
}
