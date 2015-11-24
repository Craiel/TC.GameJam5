namespace Jrpg.Game.Gameplay
{
    using System;

    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Logic;

    public class Enemy : Actor, IEnemy
    {
        private static long nextEnemyId = 1;

        private readonly ICoreSystems systems;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Enemy(IFactory factory)
            : base(factory, (nextEnemyId++).ToString())
        {
            this.systems = factory.Resolve<ICoreSystems>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void SetLevel(long level)
        {
            this.Level = level;

            this.SetBaseStats(this.systems.GetEnemyBaseStats(level));
            
            this.Experience = (long)Math.Floor(this.systems.GetEnemyExperienceReward(level));
            //this.Gold = (long)Math.Floor(this.systems.GetEnemyGoldReward(level));

            this.ResetCurrentStats();
        }
    }
}
