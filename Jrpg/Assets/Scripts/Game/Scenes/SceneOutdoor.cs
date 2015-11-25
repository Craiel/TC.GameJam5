namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    public class SceneOutdoor : GameScene
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneOutdoor()
            : base("Outdoor", "Outdoor")
        {
        }

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
    }
}
