namespace Assets.Scripts.Game.Scenes
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.Systems;

    public class SceneCombat : GameScene
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public SceneCombat()
            : base("Combat", "Combat")
        {
        }

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
    }
}
