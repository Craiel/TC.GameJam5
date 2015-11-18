namespace Jrpg.Game.Contracts.Logic
{
    using Jrpg.Game.Data;

    public interface IGameComponent
    {
        bool IsInitialized { get; }

        void Initialize();

        void Update(float currentTime);

        void Save(SaveData target);

        void Load(SaveData source);
    }
}
