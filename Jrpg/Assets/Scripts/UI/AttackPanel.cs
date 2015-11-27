namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    using UnityEngine;
    using UnityEngine.UI;

    public class AttackPanel : BasePanel
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

        [SerializeField]
        public Button AttackButton1;

        [SerializeField]
        public Button AttackButton2;

        [SerializeField]
        public Button AttackButton3;

        [SerializeField]
        public Button AttackButton4;

    }
}
