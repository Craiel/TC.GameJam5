namespace Jrpg.Game.Gameplay
{
    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts;
    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;

    public class CoreSystems : ICoreSystems
    {
        private readonly IFactory factory;
        private readonly IGameData gameData;

        private IUnityLink link;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CoreSystems(IFactory factory)
        {
            this.factory = factory;
            this.gameData = factory.Resolve<IGameData>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public float GetRequiredPlayerExperience(long level)
        {
            // Todo
            return 100;
        }

        public float GetEnemyExperienceReward(long level)
        {
            // Todo
            return 10;
        }

        public float GetEnemyGoldReward(long level)
        {
            // Todo: needs calculation
            return 5 + (level * 2);
        }

        public StatDictionary<float> GetEnemyBaseStats(long level)
        {
            var result = new StatDictionary<float>();

            // TODO
            result.SetStat(StatEnum.Str, 1 + level);
            result.SetStat(StatEnum.Hp, 10 + 10 * level);
            return result;
        }

        public StatDictionary<float> GetPlayerBaseStats(long level)
        {
            var result = new StatDictionary<float>();

            // TODO
            result.SetStat(StatEnum.Str, 2 + level);
            result.SetStat(StatEnum.Hp, 100 + 100 * level);
            return result;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private IUnityLink UnityLink
        {
            get
            {
                return this.link ?? (this.link = this.factory.Resolve<IGame>().UnityLink);
            }
        }
    }
}
