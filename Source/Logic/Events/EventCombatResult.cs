namespace Jrpg.Game.Logic.Events
{
    using Jrpg.Game.Gameplay;

    public class EventCombatResult
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public EventCombatResult(CombatResult data)
        {
            this.Data = data;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public CombatResult Data { get; private set; }
    }
}
