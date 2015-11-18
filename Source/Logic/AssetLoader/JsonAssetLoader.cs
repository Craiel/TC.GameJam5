namespace Jrpg.Game.Logic.AssetLoader
{
    using System;
    using System.Collections.Generic;

    using CarbonCore.Utils.Compat.Contracts.IoC;
    using CarbonCore.Utils.Compat.IO;

    using UnityEngine;

    using Jrpg.Game.Contracts.Logic;

    public class JsonAssetLoader : BaseAssetLoader, IJsonAssetLoader
    {
        private readonly IDictionary<string, string> cache;

        private readonly IDictionary<string, IList<Action<string>>> pendingLoads; 

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public JsonAssetLoader(IFactory factory)
            : base(factory)
        {
            this.cache = new Dictionary<string, string>();
            this.pendingLoads = new Dictionary<string, IList<Action<string>>>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void LoadJson(string path, Action<string> callback)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    {
                        this.LoadJsonAndroid(path, callback);
                        break;
                    }

                default:
                    {
                        this.LoadJsonDirect(path, callback);
                        break;
                    }
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void LoadJsonAndroid(string path, Action<string> callback)
        {
            // Cache first to avoid locks
            if (this.cache.ContainsKey(path))
            {
                callback(this.cache[path]);
                return;
            }

            lock (this.pendingLoads)
            {
                if (this.pendingLoads.ContainsKey(path))
                {
                    // We are already loading this asset, add the callback but don't trigger another load
                    this.pendingLoads[path].Add(callback);
                    return;
                }

                // This is the first load for this asset, add the pending list and don't return
                this.pendingLoads.Add(path, new List<Action<string>> { callback });
            }

            this.LoadAssetAsync(this.OnLoaded);
        }

        private void LoadJsonDirect(string path, Action<string> callback)
        {
            CarbonFile dataFile = new CarbonFile(UnityEngine.Application.streamingAssetsPath + "/" + Constants.DataFile);
            string data = dataFile.ReadAsString();
            this.cache.Add(path, data);
            callback(data);
        }

        private void OnLoaded(string path, WWW www)
        {
            if (string.IsNullOrEmpty(www.error))
            {
                string formattedText;
                if (this.TextContainsBOM(www.bytes))
                {
                    formattedText = System.Text.Encoding.UTF8.GetString(www.bytes, 3, www.bytes.Length - 3);
                }
                else
                {
                    formattedText = System.Text.Encoding.UTF8.GetString(www.bytes, 0, www.bytes.Length);
                }

                // Cache the result
                this.cache.Add(path, formattedText);

                // Notify all pending load operations and clear
                lock (this.pendingLoads)
                {
                    foreach (Action<string> callback in this.pendingLoads[path])
                    {
                        callback(formattedText);
                    }

                    this.pendingLoads.Remove(path);
                }
            }
        }

        private bool TextContainsBOM(byte[] textData)
        {
            // Getting text through WWW will sometimes return the data with BOM
            // 239 187 191 (EF BB BF)
            if (textData.Length < 3)
            {
                return false;
            }

            return textData[0] == 239 && textData[1] == 187 && textData[2] == 191;
        }
    }
}
