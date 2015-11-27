namespace Assets.Scripts.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CombatActionPanel : BasePanel
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SerializeField]
        public Button AttackButton;
        
        [SerializeField]
        public Button MagicButton;

        [SerializeField]
        public Button ItemButton;

        [SerializeField]
        public Button FleeButton;

        [SerializeField]
        public Button BackButton;

        [SerializeField]
        public AttackPanel AttackPanel;

        [SerializeField]
        public MagicPanel MagicPanel;

        [SerializeField]
        public ItemPanel ItemPanel;

        public void Awake()
        {
            this.BackButton.onClick.AddListener(this.onBack);
            this.AttackButton.onClick.AddListener(this.onAttack);
            this.MagicButton.onClick.AddListener(this.onMagic);
            this.ItemButton.onClick.AddListener(this.onItem);
            this.FleeButton.onClick.AddListener(this.onFlee);
            this.currentCombatMenu = CombatMenu.Action;
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------

        private enum CombatMenu
        {
            Action,
            Attack,
            Magic,
            Item
        }

        private CombatMenu currentCombatMenu;

        private void onBack()
        {
            switch (this.currentCombatMenu)
            {
                case (CombatMenu.Action):
                    this.onFlee();
                    break;
                case (CombatMenu.Attack):
                    this.AttackPanel.Hide();
                    this.Show();
                    this.currentCombatMenu = CombatMenu.Action;
                    break;
                case (CombatMenu.Magic):
                    this.MagicPanel.Hide();
                    this.Show();
                    this.currentCombatMenu = CombatMenu.Action;
                    break;
                case (CombatMenu.Item):
                    this.ItemPanel.Hide();
                    this.Show();
                    this.currentCombatMenu = CombatMenu.Action;
                    break;

            }
        }

        private void onFlee()
        {
            Debug.Log("Fleeing");
        }

        private void onAttack()
        {
            Debug.Log("AttackMenu");
            this.Hide();
            this.AttackPanel.Show();
            this.currentCombatMenu = CombatMenu.Attack;
        }

        private void onMagic()
        {
            Debug.Log("MagicMenu");
            this.Hide();
            this.MagicPanel.Show();
            this.currentCombatMenu = CombatMenu.Magic;
        }

        private void onItem()
        {
            Debug.Log("ItemMenu");
            this.Hide();
            this.ItemPanel.Show();
            this.currentCombatMenu = CombatMenu.Item;
        }
    }
}
