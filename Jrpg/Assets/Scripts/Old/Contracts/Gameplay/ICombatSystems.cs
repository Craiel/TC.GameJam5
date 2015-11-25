namespace Jrpg.Game.Contracts.Gameplay
{
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay;

    public interface ICombatSystems
    {
        CombatResult ResolveCombat(IAbility ability, IActor source, IActor target);
    }
}
