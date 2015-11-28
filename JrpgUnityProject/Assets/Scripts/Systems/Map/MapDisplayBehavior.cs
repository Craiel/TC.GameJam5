namespace Assets.Scripts.Systems.Map
{
    using System.Collections.Generic;

    using UnityEngine;

    public class MapDisplayBehavior : MonoBehaviour
    {
        private readonly IDictionary<string, GameObject> layers;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public MapDisplayBehavior()
        {
            this.layers = new Dictionary<string, GameObject>();
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public SpriteRenderer RegisterLayer(string layerName)
        {
            var layerObject = new GameObject(layerName);
            layerObject.transform.SetParent(this.transform);

            var layer = layerObject.AddComponent<SpriteRenderer>();
            this.layers.Add(layerName, layerObject);
            return layer;
        }

        public void Reset()
        {
            foreach (string key in this.layers.Keys)
            {
                Object.Destroy(this.layers[key]);
            }

            this.layers.Clear();
        }
    }
}
