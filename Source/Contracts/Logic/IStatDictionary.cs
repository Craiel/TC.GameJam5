namespace Jrpg.Game.Contracts.Logic
{
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;

    public interface IStatDictionary<T> where T : struct
    {
        void Clear();

        bool HasStat(StatEnum key);

        T GetStat(StatEnum key);
        
        void SetStat(StatEnum key, T value);

        void SetStats(IStatDictionary<T> other);
        
        void RemoveStat(StatEnum key);
    }
}
