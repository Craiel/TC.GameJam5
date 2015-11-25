namespace Jrpg.Game.Contracts.GamePlay
{
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay.Enums;

    public interface IPlayer : IActor
    {
        string Name { get; set; }

        long ExperienceRequired { get; }

        void GainExperience(long value, GainSourceEnum source);

        void GainGold(long value, GainSourceEnum source);

        void GainLevel(long value, GainSourceEnum source);
    }
}
