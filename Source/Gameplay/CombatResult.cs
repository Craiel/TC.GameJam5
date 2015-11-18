namespace Jrpg.Game.Gameplay
{
    using Jrpg.Game.Contracts.Logic;

    public class CombatResult
    {
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public CombatResult(IActor source, IActor target)
        {
            this.Source = source;
            this.Target = target;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public IActor Source { get; private set; }

        public IActor Target { get; private set; }

        public int HitCount { get; private set; }

        public float Damage { get; private set; }

        public void AddHit(float damage)
        {
            this.HitCount++;
            this.Damage += damage;
        }
    }
}
