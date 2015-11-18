namespace Jrpg.Game.Logic
{
    using Jrpg.Game.Contracts.Logic;

    public abstract class GameModule : GameComponent, IGameModule
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public bool IsActive { get; private set; }

        public virtual void Activate()
        {
            this.IsActive = true;
        }

        public virtual void Deactivate()
        {
            this.IsActive = false;
        }
    }
}
