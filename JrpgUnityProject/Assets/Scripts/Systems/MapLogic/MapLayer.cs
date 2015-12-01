namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public class MapLayer : MonoBehaviour
    {
        private readonly IList<MapChunk> chunks;

        private GameMapLayer layer;

        private MapTileRegistry tileRegistry;

        private ushort chunkSize = Constants.DefaultChunkSize;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapLayer()
        {
            this.chunks = new List<MapChunk>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void Initialize(GameMapLayer layerData, MapTileRegistry registry, ushort chunkSize)
        {
            this.layer = layerData;
            this.tileRegistry = registry;
            this.chunkSize = chunkSize;

            Diagnostic.Assert(this.chunkSize > 0 && this.chunkSize < 50);

            // Destroy just in case
            this.DestroyChunks();
            ushort x = 0;
            ushort y = 0;
            ushort reverseOffset = (ushort)(((layerData.Size.Y - 1) * layerData.TileSize.Y) / Constants.DefaultMapUnit);
            while (x < layerData.Size.X)
            {
                while (y < layerData.Size.Y)
                {
                    var chunkObject = new GameObject(string.Format("{0:#0}x{1:#0}", x / chunkSize, y / chunkSize));
                    chunkObject.transform.SetParent(this.gameObject.transform);
                    chunkObject.transform.position = new Vector3(
                        x * layerData.TileSize.X / Constants.DefaultMapUnit,
                        reverseOffset - (y * layerData.TileSize.Y / Constants.DefaultMapUnit));
                    chunkObject.transform.localScale = new Vector3(1, 1, 1);

                    var chunkInstance = chunkObject.AddComponent<MapChunk>();
                    chunkInstance.Initialize(this.layer, new Vector2US(x, y), new Vector2US(this.chunkSize, this.chunkSize), this.tileRegistry);
                    
                    this.chunks.Add(chunkInstance);
                    y += this.chunkSize;
                }

                y = 0;
                x += this.chunkSize;
            }
        }

        public void Invalidate()
        {
            foreach (MapChunk chunk in this.chunks)
            {
                chunk.Invalidate();
            }
        }

        private void DestroyChunks()
        {
            foreach (MapChunk chunk in this.chunks)
            {
                Destroy(chunk.gameObject);
            }

            this.chunks.Clear();
        }
    }
}
