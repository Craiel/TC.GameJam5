namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    using UnityEngine;
    using UnityEngine.UI;

    public class CombatPanel : ScenePanel
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Combat;
            }
        }
    }
}
