namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    public class SceneMainMenu : GameScene
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneMainMenu()
            : base("Main Menu", "MainMenu")
        {
        }

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
    }
}
