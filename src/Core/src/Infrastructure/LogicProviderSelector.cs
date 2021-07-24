using Application.Core.Attributes;
using Application.Core.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Application.Core.Infrastructure
{
    /// <summary>
    /// Provides a method that selects the correct logic provider for a given application plan.
    /// </summary>
    /// <typeparam name="TBase">Base class of logic providers.</typeparam>
    public static class LogicProviderSelector<TBase> where TBase : class
    {
        /// <summary>
        /// Selects the correct logic provider for a given application plan and returns an instance of that type.
        /// </summary>
        /// <param name="appPlan">application plan of the user needed for selection.</param>
        /// <returns>TBase derived instance of correct logic provider.</returns>
        public static TBase GetInstance(ApplicationPlan appPlan)
        {
            Assembly? assembly = Assembly.GetAssembly(typeof(TBase));
            if (assembly is null)
            {
                throw new NullReferenceException(nameof(assembly));
            }

            var derivedTypes = GetDerivedTypesFromAssembly(assembly);

            object instance = new();
            foreach (Type t in derivedTypes)
            {
                object[] attr = t.GetCustomAttributes(typeof(ForPlanAttribute), false);
                if (attr.Length == 0)
                {
                    throw new InvalidOperationException(nameof(attr));
                }

                ForPlanAttribute forPlanAttr = ((ForPlanAttribute[])attr).First();
                if (forPlanAttr.AppPlan == appPlan)
                {
                    instance = Activator.CreateInstance(t)!;
                }
            }
            return (TBase)instance;
        }

        /// <summary>
        /// Returns collection of derived types from <typeparamref name="TBase" /> in the given assembly.
        /// </summary>
        /// <param name="assembly">assembly where to look for derived types.</param>
        private static IEnumerable<Type> GetDerivedTypesFromAssembly(Assembly assembly)
            => assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(TBase)));
    }
}