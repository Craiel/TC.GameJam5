namespace Assets.Scripts.Systems.Map
{
    using System;

    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Compat.IO;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public class MapRenderer
    {
        private readonly GameMapLayer layer;

        private readonly SpriteRenderer spriteRenderer;

        private readonly MapTileRegistry tileRegistry;

        private readonly Texture2D target;

        private bool needUpdate;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapRenderer(GameMapLayer layer, SpriteRenderer spriteRenderer, MapTileRegistry tileRegistry)
        {
            this.layer = layer;
            this.spriteRenderer = spriteRenderer;
            this.tileRegistry = tileRegistry;

            // Todo: get the size from data
            this.target = new Texture2D(layer.SizeInPixel.X, layer.SizeInPixel.Y, TextureFormat.ARGB32, false);

            this.needUpdate = true;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public void SetViewport(Vector2 offset, Vector2 size)
        {
            // Sets the viewport that we are rendering
            this.needUpdate = true;
        }

        public void SetPosition(Vector2 position)
        {
            // Sets the current view position
            this.needUpdate = true;
        }

        public void Update()
        {
            if (this.needUpdate)
            {
                try
                {
                    this.RenderTexture();
                }
                catch (Exception e)
                {
                    Diagnostic.Error("Failed to render Map: {0}", this.layer.Name);
                }
                
                this.needUpdate = false;
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void RenderTexture()
        {
            // Todo: use the viewport instead of full-size
            this.ClearTarget(new Color(0, 0, 0, 0));

            // Iterate over all data points
            ushort x = 0;
            ushort y = (ushort)(this.layer.SizeInPixel.Y - this.layer.TileSize.Y);
            foreach (ushort id in this.layer.Data)
            {
                if (id > 0)
                {
                    Vector2US tileOffset;
                    GameTileSet tileSet = this.tileRegistry.GetTile((ushort)(id - 1), out tileOffset);

                    // Blit the tile onto our texture
                    Color[] data = tileSet.Texture.GetPixels(
                        tileOffset.X,
                        tileOffset.Y,
                        tileSet.TileSize.X,
                        tileSet.TileSize.Y);
                    this.target.SetPixels(x, y, tileSet.TileSize.X, tileSet.TileSize.Y, data);
                }

                x += this.layer.TileSize.X;
                if (x >= this.layer.SizeInPixel.X)
                {
                    x = 0;
                    y -= this.layer.TileSize.Y;
                }
            }

            // Apply the pixel changes
            this.target.Apply();

            /*byte[] pngData = this.target.EncodeToPNG();
            var file = new CarbonFile(string.Format("TEST_{0}.png", this.layer.Name));
            using (var stream = file.OpenWrite())
            {
                stream.Write(pngData, 0, pngData.Length);
            }*/

            Sprite sprite = Sprite.Create(
                this.target,
                new Rect(0, 0, this.target.width, this.target.height),
                new Vector2(0, 1),
                100);
            sprite.name = this.layer.Name;

            this.spriteRenderer.sprite = sprite;
            this.spriteRenderer.transform.localScale = new Vector3(100, 100, 1);
        }

        private void ClearTarget(Color targetColor)
        {
            for (var x = 0; x < this.target.width; x++)
            {
                for (var y = 0; y < this.target.height; y++)
                {
                    this.target.SetPixel(x, y, targetColor);
                }
            }
        }
    }
}
