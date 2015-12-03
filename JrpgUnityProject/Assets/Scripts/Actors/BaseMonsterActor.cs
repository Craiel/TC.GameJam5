namespace Assets.Scripts.Actors
{
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Data;

    public class BaseMonsterActor : CombatActor
    {
        public BaseMonsterActor(int id, ResourceKey prefabKey, ResourceKey spriteKey, ResourceKey portraitKey)
            : base(id, prefabKey, spriteKey, portraitKey)
        {
        }
    }
}
