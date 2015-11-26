namespace Assets.Scripts.UI
{
    using Assets.Scripts.Enums;

    public class CombatPanel : BasePanel
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
    }
}
