namespace Assets.Scripts.Systems.MapLogic
{
    using System.Collections.Generic;

    using UnityEngine;

    public class MapLayerMaterial
    {
        private readonly IDictionary<int, int> layerDataToMaterialData;

        public MapLayerMaterial(Material material)
        {
            this.Material = material;

            this.layerDataToMaterialData = new Dictionary<int, int>();
        }

        public Material Material { get; private set; }
    }
}
