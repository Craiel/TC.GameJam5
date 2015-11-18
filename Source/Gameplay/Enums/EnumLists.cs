namespace Jrpg.Game.Gameplay.Enums
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public static class EnumLists
    {
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
        public static IList<StatEnum> StatList = System.Enum.GetValues(typeof(StatEnum)).Cast<StatEnum>().ToList();
    }
}
