namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using Assets.Scripts.Data;

    using CarbonCore.Utils.Unity.Data;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;

    public static class MapRenderer
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        /*public static MapLayerMaterial BuildLayerMaterial(GameMapLayer layer, MapTileRegistry tileRegistry)
        {
            Material instance;
            using (var resource = ResourceProvider.Instance.AcquireResource<Material>(AssetResourceKeys.MaterialMapAssetKey))
            {
                instance = new Material(resource.Data);
            }

            // Todo: Create the texture
            IList<ushort> uniqueDataPoints = new List<ushort>();
            foreach (ushort dataPoint in layer.Data)
            {
                if (dataPoint == 0 || uniqueDataPoints.Contains(dataPoint))
                {
                    continue;
                }

                uniqueDataPoints.Add(dataPoint);
            }

            var tileCount = Mathf.CeilToInt(uniqueDataPoints.Count / 2.0f);
            var texture = new Texture2D(tileCount * layer.TileSize.X, tileCount * layer.TileSize.Y);
            ClearBuffer(texture, Constants.MapClearColor);

            int x = 0;
            int y = 0;
            foreach (ushort dataPoint in uniqueDataPoints)
            {
                Vector2I tileOffset;
                GameTileSet tileSet = tileRegistry.GetTile(dataPoint - 1, out tileOffset);

                // Blit the tile onto our texture
                Color[] data = tileSet.Texture.GetPixels(
                    tileOffset.X,
                    tileOffset.Y,
                    tileSet.TileSize.X,
                    tileSet.TileSize.Y);
                texture.SetPixels(
                    x * tileSet.TileSize.X,
                    y * tileSet.TileSize.Y,
                    tileSet.TileSize.X,
                    tileSet.TileSize.Y,
                    data);

                x++;

                if (x == tileCount)
                {
                    x = 0;
                    y++;
                }
            }
            
            texture.Apply();
            instance.mainTexture = texture;
            return new MapLayerMaterial(instance);
        }
        
        public static Sprite RenderMap(GameMapLayer layer, Vector2I size, Vector2I offset, MapTileRegistry tileRegistry)
        {
            // Make the buffer texture
            int pixelWidth = size.X * layer.TileSize.X;
            int pixelHeight = size.Y * layer.TileSize.Y;

            Texture2D buffer = new Texture2D(pixelWidth, pixelHeight, TextureFormat.ARGB32, false);

            ClearBuffer(buffer, Constants.MapClearColor);

            // Iterate over all data points
            int x = 0;
            int y = 0;
            int reverseOffset = (size.Y - 1) * layer.TileSize.Y;
            while (x < size.X)
            {
                while (y < size.Y)
                {
                    int dataOffset = ((offset.Y + y) * layer.Size.Y) + x + offset.X;
                    var id = layer.Data[dataOffset];
                    if (id > 0)
                    {
                        Vector2I tileOffset;
                        GameTileSet tileSet = tileRegistry.GetTile(id - 1, out tileOffset);

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
        }*/
    }
}
