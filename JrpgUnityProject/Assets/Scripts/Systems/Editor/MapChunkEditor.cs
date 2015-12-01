namespace Assets.Scripts.Systems.Editor
{
    using Assets.Scripts.Systems.MapLogic;

    using UnityEditor;

    using UnityEngine;

    [CustomEditor(typeof(MapChunk))]
    public class MapChunkEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapChunk chunk = (MapChunk)this.target;

            if (GUILayout.Button("Invalidate"))
            {
                chunk.Invalidate();
            }
        }
    }
}
