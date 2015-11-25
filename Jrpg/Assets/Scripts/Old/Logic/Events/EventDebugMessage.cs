namespace Jrpg.Game.Logic.Events
{
    public class EventDebugMessage
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public EventDebugMessage(string message, params object[] args)
        {
            this.Message = args == null || args.Length <= 0 ? message : string.Format(message, args);
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Message { get; private set; }
    }
}
