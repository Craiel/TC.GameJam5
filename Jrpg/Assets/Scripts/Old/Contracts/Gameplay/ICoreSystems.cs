namespace Jrpg.Game.Contracts.GamePlay
{
    using Jrpg.Game.Logic;

    public interface ICoreSystems
    {
        float GetRequiredPlayerExperience(long level);

        float GetEnemyExperienceReward(long level);

        float GetEnemyGoldReward(long level);

        StatDictionary<float> GetEnemyBaseStats(long level);

        StatDictionary<float> GetPlayerBaseStats(long level);
    }
}
