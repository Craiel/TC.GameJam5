namespace Jrpg.Game.Logic
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Data;
    using Jrpg.Game.Gameplay.Enums;

    public abstract class Actor : GameComponent, IActor
    {
        private static readonly IList<string> IdCheck = new List<string>();
 
        private readonly ICoreSystems systems;

        private readonly StatDictionary<float> statsBase;

        private readonly StatDictionary<float> statsCurrent;

        private readonly StatDictionary<float> statsMax;

        private bool needStatUpdate = true;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected Actor(IFactory factory, string id)
        {
            if (IdCheck.Contains(id))
            {
                throw new InvalidOperationException("Duplicate Actor ID: " + id);
            }

            // Register the id
            this.Id = id;
            IdCheck.Add(id);

            this.systems = factory.Resolve<ICoreSystems>();

            this.statsBase = new StatDictionary<float>();
            this.statsCurrent = new StatDictionary<float>();
            this.statsMax = new StatDictionary<float>();

            this.Level = 1;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Id { get; private set; }

        public long Level { get; protected set; }

        //public long Gold { get; protected set; }

        public long Experience { get; protected set; }

        public override void Load(SaveData source)
        {
            base.Load(source);

            if (source.ActorData == null || !source.ActorData.ContainsKey(this.Id))
            {
                return;
            }

            SaveDataActor actorData = source.ActorData[this.Id];
            this.Level = actorData.Level;
            //this.Gold = actorData.Gold;
            this.Experience = actorData.Experience;
        }

        public override void Save(SaveData target)
        {
            base.Save(target);

            if (target.ActorData == null)
            {
                target.ActorData = new Dictionary<string, SaveDataActor>();
            }

            SaveDataActor actorData = new SaveDataActor
                                          {
                                              Level = this.Level,
                                              //Gold = this.Gold,
                                              Experience = this.Experience
                                          };

            target.ActorData.Add(this.Id, actorData);
        }

        public void SetBaseStats(StatDictionary<float> stats)
        {
            this.statsBase.Clear();
            this.statsBase.SetStats(stats);

            this.needStatUpdate = true;
        }

        public float GetCurrentStat(StatEnum key)
        {
            this.UpdateStats();

            return this.statsCurrent.GetStat(key);
        }

        public float GetMaxStat(StatEnum key)
        {
            this.UpdateStats();

            return this.statsMax.GetStat(key);
        }

        public void SetCurrentStat(StatEnum key, float value)
        {
            this.UpdateStats();

            this.statsCurrent.SetStat(key, value);
        }

        public void ModifyCurrentStat(StatEnum key, float modifier)
        {
            this.UpdateStats();
            
            this.statsCurrent.SetStat(key, this.statsCurrent.GetStat(key) + modifier);
        }

        public void ResetCurrentStats()
        {
            this.UpdateStats();

            this.statsCurrent.Clear();
            this.statsCurrent.SetStats(this.statsMax);
        }
        
        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void UpdateStats()
        {
            if (!this.needStatUpdate)
            {
                return;
            }

            this.statsMax.Clear();
            this.statsMax.SetStats(this.statsBase);

            // Todo: Merge other stats into the base

            this.needStatUpdate = false;
        }
    }
}
