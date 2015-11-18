namespace Jrpg.Game
{
    using System;
    using System.Reflection;

    using CarbonCore.Utils.Compat.Contracts.IoC;
    using CarbonCore.Utils.Compat.IoC;

    using Jrpg.Game.IoC;

    public static class Core
    {
        private static ICarbonContainer container;
        private static IFactory factory;

        // -------------------------------------------------------------------
        // Constructor
        // -------------------------------------------------------------------
        static Core()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                if (assembly.FullName.StartsWith("nunit.", StringComparison.OrdinalIgnoreCase))
                {
                    IsRunningUnitTest = true;
                    break;
                }
            }
        }

        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public static bool IsRunningUnitTest { get; private set; }

        public static ICarbonContainer Container
        {
            get
            {
                return container ?? (container = CarbonContainerBuilder.BuildQuick<WorldsModule>());
            }
        }

        public static IFactory Factory
        {
            get
            {
                return factory ?? (factory = Container.Resolve<IFactory>());
            }
        }
    }
}
