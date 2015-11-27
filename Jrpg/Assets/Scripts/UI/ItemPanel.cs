namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    using UnityEngine;

    public class ItemPanel : BasePanel
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
        public ItemButton ItemButton1;

        [SerializeField]
        public ItemButton ItemButton2;

        [SerializeField]
        public ItemButton ItemButton3;

        [SerializeField]
        public ItemButton ItemButton4;

    }
}