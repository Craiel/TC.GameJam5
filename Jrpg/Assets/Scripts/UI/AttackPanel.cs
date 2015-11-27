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
        public AttackButton AttackButton1;

        [SerializeField]
        public AttackButton AttackButton2;

        [SerializeField]
        public AttackButton AttackButton3;

        [SerializeField]
        public AttackButton AttackButton4;

    }
}
