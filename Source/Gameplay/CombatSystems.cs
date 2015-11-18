namespace Jrpg.Game.Gameplay
{
    using CarbonCore.Utils.Compat.Contracts.IoC;

    using Jrpg.Game.Contracts;
    using Jrpg.Game.Contracts.Gameplay;
    using Jrpg.Game.Contracts.GamePlay;
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay.Enums;

    public class CombatSystems : ICombatSystems
    {
        private readonly IFactory factory;
        private readonly IGameData gameData;

        private IUnityLink link;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CombatSystems(IFactory factory)
        {
            this.factory = factory;
            this.gameData = factory.Resolve<IGameData>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public CombatResult ResolveCombat(IAbility ability, IActor source, IActor target)
        {
            // Todo: replace with proper ability calculation
            var result = new CombatResult(source, target);

            float mainStatValue = source.GetCurrentStat(StatEnum.Str);
            if (mainStatValue > 0)
            {
                float damage = mainStatValue;
                damage *= ability.Multiplier;
                result.AddHit(damage);
            }

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
