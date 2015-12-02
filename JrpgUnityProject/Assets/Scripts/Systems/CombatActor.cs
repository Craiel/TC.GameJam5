namespace Assets.Scripts.Systems
{
    using Assets.Scripts.Enums;

    using CarbonCore.Utils.Unity.Data;

    public class CombatActor : BaseActor
    {
        public Vector2I CombatPosition { get; private set; }
        public bool IsActive { get; private set; }
        public int Initiative { get; private set; }
        public CombatInput CombatInput { get; private set; }
    }
}
