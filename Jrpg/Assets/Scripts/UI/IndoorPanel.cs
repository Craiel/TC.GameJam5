namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;

    using UnityEngine;
    using UnityEngine.UI;

    public class IndoorPanel : BasePanel
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Indoor;
            }
        }

        [SerializeField]
        public Button QuitButton;

        public void Awake()
        {
            this.QuitButton.onClick.AddListener(this.OnQuit);
        }

        public void Update()
        {
            // Todo: use input controller
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.OnQuit();
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnQuit()
        {
            GameSystem.Instance.Transition(GameSceneType.MainMenu);
        }
    }
}
