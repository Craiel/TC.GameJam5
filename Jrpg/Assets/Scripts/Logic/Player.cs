namespace Jrpg.Game.Gameplay
{
    using System;

    using CarbonCore.Utils.Compat.Contracts;
    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Data;
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;
    using Jrpg.Game.Logic.Events;

    public class Player : Actor, IPlayer
    {
        private readonly IEventRelay eventRelay;

        private readonly ICoreSystems systems;

        private bool needStateUpdate = true;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Player(IFactory factory)
            : base(factory, "Player")
        {
            this.eventRelay = factory.Resolve<IEventRelay>();
            this.systems = factory.Resolve<ICoreSystems>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; set; }

        public long ExperienceRequired { get; private set; }

        public override void Load(SaveData source)
        {
            base.Load(source);

            if (source.Player == null)
            {
                return;
            }

            //this.Name = source.Player.Name;
        }

        public override void Save(SaveData target)
        {
            base.Save(target);

            //target.Player = new SaveDataPlayer { Name = this.Name };
        }

        public void GainExperience(long value, GainSourceEnum source)
        {
            if (value <= 0)
            {
                return;
            }

            this.Experience += value;

            this.eventRelay.Relay(new EventGain(EventGainType.Experience, value, source));
        }

        public void GainGold(long value, GainSourceEnum source)
        {
            if (value <= 0)
            {
                return;
            }

            //this.Gold += value;

            this.eventRelay.Relay(new EventGain(EventGainType.Gold, value, source));
        }

        public void GainLevel(long value, GainSourceEnum source)
        {
            if (value <= 0)
            {
                return;
            }

            // Gaining levels just resets the level for now
            this.Level += value;
            
            this.Experience = 0;

            this.UpdatePlayerState();
            
            this.eventRelay.Relay(new EventGain(EventGainType.Level, value, source));
        }

        public override void Update(float currentTime)
        {
            base.Update(currentTime);

            if (this.needStateUpdate)
            {
                this.UpdatePlayerState();
            }

            if (this.Experience >= this.ExperienceRequired)
            {
                this.GainLevel(1, GainSourceEnum.Experience);
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void UpdatePlayerState()
        {
            this.SetBaseStats(this.systems.GetPlayerBaseStats(this.Level));

            this.ExperienceRequired = (long)Math.Round(this.systems.GetRequiredPlayerExperience(this.Level));

            // Todo: Look into maybe not doing this but just resetting selectively
            this.ResetCurrentStats();

            this.needStateUpdate = false;
        }
    }
}
