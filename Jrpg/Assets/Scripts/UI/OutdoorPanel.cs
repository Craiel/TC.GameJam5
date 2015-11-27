namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;
    using Assets.Scripts.InputSystem;

    using UnityEngine;
    using UnityEngine.UI;

    public class OutdoorPanel : ScenePanel
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override GameSceneType Type
        {
            get
            {
                return GameSceneType.Outdoor;
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
            if (InputHandler.Instance.GetState(Controls.Exit).IsPressed)
            {
                this.OnQuit();
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void OnQuit()
        {
            GameSaveLoad.Save();

            GameSystem.Instance.Transition(GameSceneType.MainMenu);
        }
    }
}
