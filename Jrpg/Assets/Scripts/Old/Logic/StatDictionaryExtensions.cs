namespace Jrpg.Game.Logic
{
    using Jrpg.Game.Contracts.Logic;
    using Jrpg.Game.Gameplay.Enums;

    public static class StatDictionaryExtensions
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static void Merge(this StatDictionary<float> target, StatDictionary<float> source)
        {
            foreach (StatEnum @enum in EnumLists.StatList)
            {
                // If the source does not have the stat we have nothing to merge
                if (!source.HasStat(@enum))
                {
                    continue;
                }

                // Get the current target value
                float value = target.GetStat(@enum);

                // Add the source value
                value += source.GetStat(@enum);

                // Set the value back to the target
                target.SetStat(@enum, value);
            }
        }
    }
}
