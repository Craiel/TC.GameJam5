namespace Jrpg.Game.Contracts.Logic
{
    using Jrpg.Game.Gameplay.Enums;

    public interface IActor : IGameComponent
    {
        long Level { get; }

        long Gold { get; }

        long Experience { get; }

        void SetBaseStats(IStatDictionary<float> stats);

        float GetCurrentStat(StatEnum key);

        float GetMaxStat(StatEnum key);

        void SetCurrentStat(StatEnum key, float value);

        void ModifyCurrentStat(StatEnum key, float modifier);

        void ResetCurrentStats();
    }
}
