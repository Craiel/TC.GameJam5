namespace Jrpg.Game.Gameplay.Modules
{
    using CarbonCore.Utils.Compat.Contracts;
    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts.Gameplay;
    using Jrpg.Game.Contracts.Gameplay.Modules;
    using Jrpg.Game.Logic;

    public class CombatModule : GameModule, ICombatModule
    {
        private readonly IFactory factory;
        private readonly IEventRelay eventRelay;
        private readonly ICombatSystems combatSystems;

        private float updateTime;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CombatModule(IFactory factory)
        {
            this.factory = factory;
            this.eventRelay = factory.Resolve<IEventRelay>();
            this.combatSystems = factory.Resolve<ICombatSystems>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Update(float currentTime)
        {
            base.Update(currentTime);
            
            this.updateTime = currentTime;
        }
    }
}
