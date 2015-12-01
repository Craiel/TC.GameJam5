namespace Assets.Scripts.Systems.MapLogic
{
    using System;

    using Assets.Scripts.Game;

    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;
    public class MapChunk : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private GameMapLayer layer;

        private MapTileRegistry tileRegistry;

        private bool needUpdate;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public Vector2US Offset { get; private set; }
        public Vector2US WorldOffset { get; private set; }
        public Vector2US Size { get; private set; }

        public void Initialize(GameMapLayer layerData, Vector2US offset, Vector2US size, MapTileRegistry registry)
        {
            this.layer = layerData;
            this.Offset = offset;
            this.WorldOffset = new Vector2US(offset.X, (ushort)(layerData.Size.Y - offset.Y));
            this.Size = size;
            this.tileRegistry = registry;
        }

        public void Invalidate()
        {
            if (this.spriteRenderer != null)
            {
                GameObject.Destroy(this.spriteRenderer);
                this.spriteRenderer = null;
            }

            this.needUpdate = true;
        }

        public void Update()
        {
            // Todo: Refactor
            if ((Components.Instance.Player.OutdoorPosition - this.WorldOffset).magnitude > Constants.DefaultChunkRange)
            {
                this.Invalidate();
                return;
            }

            if (this.needUpdate)
            {
                // TODO: Check that we are in the camera viewport before proceeding
                this.RenderChunk();

                this.needUpdate = false;
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void RenderChunk()
        {
            try
            {
                if (this.spriteRenderer == null)
                {
                    this.spriteRenderer = this.gameObject.AddComponent<SpriteRenderer>();
                }

                this.spriteRenderer.sprite = MapRenderer.RenderMap(this.layer, this.Size, this.Offset, this.tileRegistry);
            }
            catch (Exception e)
            {
                Diagnostic.Error("Failed to render Map {0}: {1}", this.layer.Name, e);
            }
        }
    }
}
