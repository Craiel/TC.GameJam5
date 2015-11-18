namespace Jrpg.Game.Contracts.Logic
{
    public interface IGameModule : IGameComponent
    {
        bool IsActive { get; }

        void Activate();

        void Deactivate();
    }
}
