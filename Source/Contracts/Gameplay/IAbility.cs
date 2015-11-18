namespace Jrpg.Game.Contracts.Gameplay
{
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay.Enums;

    public interface IAbility : IGameComponent
    {
        string Name { get; }

        float Multiplier { get; set; }

        float ExecutionTime { get; set; }
        float ExecutionTimeRemaining { get; }

        float Cooldown { get; set; }
        float CooldownRemaining { get; }

        AbilityState State { get; }

        bool Execute();
    }
}
