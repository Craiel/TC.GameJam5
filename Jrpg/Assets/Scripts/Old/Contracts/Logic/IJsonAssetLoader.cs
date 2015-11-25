namespace Jrpg.Game.Contracts.Logic
{
    using System;

    public interface IJsonAssetLoader
    {
        void LoadJson(string path, Action<string> callback);
    }
}
