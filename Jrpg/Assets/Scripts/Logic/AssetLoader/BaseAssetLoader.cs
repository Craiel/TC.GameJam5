namespace Jrpg.Game.Logic.AssetLoader
{
    using System;
    using System.Collections;

    using CarbonCore.Utils.Compat.Contracts.IoC;

    using UnityEngine;

    using Jrpg.Game.Contracts;

    public abstract class BaseAssetLoader
    {
        private readonly IFactory factory;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        protected BaseAssetLoader(IFactory factory)
        {
            this.factory = factory;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        protected void LoadAssetAsync(Action<string, WWW> callback)
        {
            IGame game = this.factory.Resolve<IGame>();
            // (this.ReadStreamingAsset(Constants.DataFile, callback));
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private IEnumerator ReadStreamingAsset(string path, Action<string, WWW> onDone)
        {
            string streamingPath = Application.streamingAssetsPath + "/" + path;
            var www = new WWW(streamingPath);
            yield return www;
            onDone(path, www);
        }
    }
}
