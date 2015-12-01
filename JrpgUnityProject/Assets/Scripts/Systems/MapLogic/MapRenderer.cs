﻿namespace Assets.Scripts.Systems.MapLogic
{
    using Assets.Scripts.Systems.Map;

    using CarbonCore.Utils.Compat.Diagnostics;
    using CarbonCore.Utils.Unity.Data;

    using UnityEngine;

    public static class MapRenderer
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static Sprite RenderMap(GameMapLayer layer, Vector2US size, Vector2US offset, MapTileRegistry tileRegistry)
        {
            // Make the buffer texture
            ushort pixelWidth = (ushort)(size.X * layer.TileSize.X);
            ushort pixelHeight = (ushort)(size.Y * layer.TileSize.Y);

            Texture2D buffer = new Texture2D(pixelWidth, pixelHeight, TextureFormat.ARGB32, false);

            ClearBuffer(buffer, Constants.MapClearColor);

            // Iterate over all data points
            ushort x = 0;
            ushort y = 0;
            ushort reverseOffset = (ushort)((size.Y - 1) * layer.TileSize.Y);
            while (x < size.X)
            {
                while (y < size.Y)
                {
                    ushort dataOffset = (ushort)(((offset.Y + y) * layer.Size.Y) + x + offset.X);
                    var id = layer.Data[dataOffset];
                    if (id > 0)
                    {
                        Vector2US tileOffset;
                        GameTileSet tileSet = tileRegistry.GetTile((ushort)(id - 1), out tileOffset);

                        // Blit the tile onto our texture
                        Color[] data = tileSet.Texture.GetPixels(
                            tileOffset.X,
                            tileOffset.Y,
                            tileSet.TileSize.X,
                            tileSet.TileSize.Y);
                        buffer.SetPixels(
                            x * tileSet.TileSize.X,
                            reverseOffset - (y * tileSet.TileSize.Y),
                            tileSet.TileSize.X,
                            tileSet.TileSize.Y,
                            data);
                    }

                    y++;
                }

                y = 0;
                x++;
            }

            // Apply the pixel changes
            buffer.Apply();

            Sprite sprite = Sprite.Create(
                buffer,
                new Rect(0, 0, buffer.width, buffer.height),
                new Vector2(0, 1),
                100);
            sprite.name = layer.Name;

            return sprite;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private static void ClearBuffer(Texture2D buffer, Color targetColor)
        {
            for (var x = 0; x < buffer.width; x++)
            {
                for (var y = 0; y < buffer.height; y++)
                {
                    buffer.SetPixel(x, y, targetColor);
                }
            }
        }
    }
}
