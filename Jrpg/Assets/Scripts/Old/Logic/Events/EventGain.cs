namespace Jrpg.Game.Logic.Events
{
    using Jrpg.Game.Gameplay.Enums;

    public class EventGain
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public EventGain(EventGainType type, long value, GainSourceEnum source)
        {
            this.Type = type;
            this.Value = value;
            this.Source = source;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public EventGainType Type { get; private set; }

        public long Value { get; private set; }

        public GainSourceEnum Source { get; private set; }
    }
}
