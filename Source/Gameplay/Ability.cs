namespace Jrpg.Game.Gameplay
{
    using Jrpg.Game.Contracts.Gameplay;
    using Jrpg.Game.Gameplay.Enums;
    using Jrpg.Game.Logic;

    public class Ability : GameComponent, IAbility
    {
        private float lastExecutionStartTime;
        private float lastExecutionEndTime;
        
        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        public Ability(string name)
        {
            this.Name = name;
            
            // Set some sensible defaults
            this.Cooldown = 1.0f;
            this.ExecutionTime = 0f;
            this.Multiplier = 1.0f;
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public string Name { get; private set; }

        public float Multiplier { get; set; }

        public float ExecutionTime { get; set; }
        public float ExecutionTimeRemaining { get; private set; }

        public float Cooldown { get; set; }
        public float CooldownRemaining { get; private set; }

        public AbilityState State { get; private set; }

        public bool Execute()
        {
            if (this.State != AbilityState.Idle)
            {
                return false;
            }

            this.SwitchState(AbilityState.ExecuteBegin);
            return true;
        }

        public override void Update(float currentTime)
        {
            base.Update(currentTime);

            switch (this.State)
            {
                case AbilityState.ExecuteBegin:
                    {
                        this.lastExecutionStartTime = currentTime;
                        this.SwitchState(AbilityState.Execute);
                        break;
                    }

                case AbilityState.Execute:
                    {
                        this.ExecutionTimeRemaining = this.ExecutionTime - (currentTime - this.lastExecutionStartTime);
                        if (this.ExecutionTimeRemaining <= 0f)
                        {
                            this.ExecutionTimeRemaining = 0f;
                            this.SwitchState(AbilityState.ExecuteEnd);
                        }

                        break;
                    }

                case AbilityState.ExecuteEnd:
                    {
                        this.lastExecutionEndTime = currentTime;
                        this.SwitchState(AbilityState.Cooldown);
                        break;
                    }

                case AbilityState.Cooldown:
                    {
                        this.CooldownRemaining = this.Cooldown - (currentTime - this.lastExecutionEndTime);
                        if (this.CooldownRemaining <= 0f)
                        {
                            this.CooldownRemaining = 0f;
                            this.SwitchState(AbilityState.Idle);
                        }

                        break;
                    }
            }
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void SwitchState(AbilityState newState)
        {
            System.Diagnostics.Trace.TraceInformation("Ability StateChange: {0} - {1}", this.Name, newState);
            this.State = newState;
        }
    }
}
