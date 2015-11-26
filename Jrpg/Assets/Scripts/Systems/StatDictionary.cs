namespace Assets.Scripts.Systems
{
    using System.Collections.Generic;

    using Assets.Scripts.Enums;

    public class StatDictionary<T>
        where T : struct 
    {
        private readonly IDictionary<StatEnum, T> values;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public StatDictionary()
        {
            this.values = new Dictionary<StatEnum, T>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void Clear()
        {
            this.values.Clear();
        }

        public bool HasStat(StatEnum key)
        {
            return this.values.ContainsKey(key);
        }

        public T GetStat(StatEnum key)
        {
            if (this.values.ContainsKey(key))
            {
                return this.values[key];
            }

            return default(T);
        }

        public void SetStat(StatEnum key, T value)
        {
            if (!this.values.ContainsKey(key))
            {
                this.values.Add(key, value);
                return;
            }

            this.values[key] = value;
        }

        public void SetStats(StatDictionary<T> other)
        {
            foreach (StatEnum @enum in EnumLists.StatList)
            {
                if (other.HasStat(@enum))
                {
                    this.SetStat(@enum, other.GetStat(@enum));
                }
            }
        }

        public void RemoveStat(StatEnum key)
        {
            System.Diagnostics.Trace.Assert(this.values.ContainsKey(key));

            this.values.Remove(key);
        }
    }
}
