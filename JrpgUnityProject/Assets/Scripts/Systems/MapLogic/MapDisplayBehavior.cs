namespace Assets.Scripts.Systems.MapLogic
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
        public void RegisterLayer(GameObject layerObject, MapLayer instance)
        {
            layerObject.transform.SetParent(this.transform);
            layerObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            
            this.layers.Add(layerObject.name, layerObject);
        }

        public void Reset()
        {
            foreach (string key in this.layers.Keys)
            {
                Object.Destroy(this.layers[key]);
            }

            this.layers.Clear();

            this.transform.localScale = new Vector3(Constants.DefaultMapUnit, Constants.DefaultMapUnit, 1.0f);
            this.transform.position = Vector3.zero;
        }
    }
}
