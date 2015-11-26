namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;

    using UnityEngine;
    using UnityEngine.UI;

    public class MainMenuPanel : BasePanel
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.MainMenu;
            }
        }

        [SerializeField]
        public Button StartGameButton;

        [SerializeField]
        public Button QuitGameButton;

        public void Awake()
        {
            this.StartGameButton.onClick.AddListener(this.OnStartGame);
            this.QuitGameButton.onClick.AddListener(this.OnQuitGame);
        }

        public void Update()
        {
            // Todo: use input controller
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.OnQuitGame();
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnStartGame()
        {
            GameSystem.Instance.Transition(GameSceneType.Outdoor);
        }

        private void OnQuitGame()
        {
            Application.Quit();
        }
    }
}
