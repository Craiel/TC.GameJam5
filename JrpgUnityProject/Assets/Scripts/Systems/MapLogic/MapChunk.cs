namespace Assets.Scripts.Systems.MapLogic
{
    using System;

    using Assets.Scripts.Data;
    using Assets.Scripts.Game;

    using CarbonCore.Utils.Diagnostics;
    using CarbonCore.Utils.MathUtils;
    using CarbonCore.Utils.Unity.Logic.MeshGeneration;
    using CarbonCore.Utils.Unity.Logic.Resource;

    using UnityEngine;
    public class MapChunk : MonoBehaviour
    {
        private CustomPlane plane;

        private SpriteRenderer spriteRenderer;

        private GameMapLayer layer;

        private MapTileRegistry tileRegistry;

        private bool needUpdate;

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public Vector2I Offset { get; private set; }
        public Vector2I WorldOffset { get; private set; }
        public Vector2I Size { get; private set; }

        public void Initialize(GameMapLayer layerData, Vector2I offset, Vector2I size, MapTileRegistry registry)
        {
            this.layer = layerData;
            this.Offset = offset;
            this.WorldOffset = new Vector2I(offset.X, layerData.Size.Y - offset.Y);
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
            if ((Components.Instance.Player.OutdoorPosition - this.WorldOffset).Magnitude > Constants.DefaultChunkRange)
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
                    this.spriteRenderer.sortingOrder = this.layer.Order;
                }

                if (this.plane == null)
                {
                    var planeOptions = new PlaneOptions
                    {
                        Width = this.Size.X * this.layer.TileSize.X / Constants.DefaultMapUnit,
                        Height = this.Size.Y * this.layer.TileSize.Y / Constants.DefaultMapUnit,
                        WidthSegments = this.Size.X,
                        HeightSegments = this.Size.Y,
                        Name = this.name
                    };
                    
                    using (var resource = ResourceProvider.Instance.AcquireResource<GameObject>(AssetResourceKeys.PrefabMapMeshAssetKey))
                    {
                        var instance = Instantiate(resource.Data);
                        this.plane = PlaneGeneration.CreateMeshObject(planeOptions, customObject: instance);

                        // Move the instance into place
                        instance.transform.SetParent(this.gameObject.transform);
                        instance.transform.localScale = new Vector3(1, 1, 1);
                        instance.transform.localPosition = new Vector3(0, 0, -0.1f);
                        instance.transform.rotation = Quaternion.AngleAxis(270, Vector3.right);
                    }

                    // Todo: figure out how we can achieve this..
                    var meshRenderer = this.plane.GetComponent<MeshRenderer>();
                    meshRenderer.material = this.tileRegistry.GetMaterial(this.layer);
                }

                // this.spriteRenderer.sprite = MapRenderer.RenderMap(this.layer, this.Size, this.Offset, this.tileRegistry);
            }
            catch (Exception e)
            {
                Diagnostic.Error("Failed to render Map {0}: {1}", this.layer.Name, e);
            }
        }
    }
}
