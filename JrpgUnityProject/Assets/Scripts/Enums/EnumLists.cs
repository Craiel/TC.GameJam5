namespace Assets.Scripts.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
    public static class EnumLists
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static IList<StatEnum> StatList = Enum.GetValues(typeof(StatEnum)).Cast<StatEnum>().ToList();

        public static IList<Controls> Controls = Enum.GetValues(typeof(Controls)).Cast<Controls>().ToList();

        public static IList<GameAudioType> AudioTypes = Enum.GetValues(typeof(GameAudioType)).Cast<GameAudioType>().ToList();
    }
}
