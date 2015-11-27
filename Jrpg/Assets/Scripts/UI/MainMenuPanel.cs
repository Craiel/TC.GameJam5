namespace Assets.Scripts.UI
{
    using Assets.Scripts.Data;
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.InputSystem;

    using CarbonCore.Utils.Unity.Logic.Resource;

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
        public Button NewGameButton;

        [SerializeField]
        public Button LoadGameButton;

        [SerializeField]
        public Button QuitGameButton;

        public void Awake()
        {
            this.NewGameButton.onClick.AddListener(this.OnNewGame);
            this.LoadGameButton.onClick.AddListener(this.OnLoadGame);
            this.QuitGameButton.onClick.AddListener(this.OnQuitGame);
        }

        public void Update()
        {
            // Todo: use input controller
            if (InputHandler.Instance.GetState(Controls.Exit).IsPressed)
            {
                this.OnQuitGame();
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnNewGame()
        {
            // Play the accept sound
            Components.Instance.Audio.PlayOneShot(AssetResourceKeys.SfxAcceptAssetKey, GameAudioType.Sfx);

            // Reset all game data
            GameSaveLoad.Reset();

            // Transition to the default for a new game
            GameSystem.Instance.Transition(GameSceneType.Outdoor);
        }

        private void OnLoadGame()
        {
            GameSaveLoad.Load();

            // Todo: transition based on the load state
            GameSystem.Instance.Transition(GameSceneType.Outdoor);
        }

        private void OnQuitGame()
        {
            Application.Quit();
        }
    }
}
