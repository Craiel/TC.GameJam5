namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    using UnityEngine;

    public class MagicPanel : BasePanel
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
        public SpellButton SpellButton1;

        [SerializeField]
        public SpellButton SpellButton2;

        [SerializeField]
        public SpellButton SpellButton3;

        [SerializeField]
        public SpellButton SpellButton4;


    }
}
