namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Game;

    using UnityEngine;
    using UnityEngine.UI;

    public class OutdoorPanel : BasePanel
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
