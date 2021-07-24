using Application.Core.Nutrition.Attributes;
using Application.Core.Nutrition.MealComponents.Base;
using System;
using System.Linq;

namespace Application.Core.Nutrition.Extensions
{
    /// <summary>
    /// Defines extension methods for <seealso cref="MealComponent" />.
    /// </summary>
    internal static class MealComponentExtensions
    {
        /// <summary>
        /// Indicates whether the meal component amount can change.
        /// </summary>
        /// <param name="mealComponent">meal component to check.</param>
        /// <returns>True if can be modified; otherwise False.</returns>
        public static bool CanBeModified(this MealComponent mealComponent)
        {
            if (mealComponent is null)
            {
                throw new ArgumentNullException(nameof(mealComponent));
            }

            return Attribute.GetCustomAttribute(mealComponent.GetType(), typeof(NonModifiableComponentAttribute)) is null;
        }

        /// <summary>
        /// Returns organizational unit name of the meal component type.
        /// </summary>
        /// <param name="mealComponent">meal component for which is the name being searched.</param>
        /// <returns>Name of the component.</returns>
        public static string GetCollectionName(this MealComponent mealComponent)
        {
            object[] attr = mealComponent.GetType()
                .GetCustomAttributes(typeof(CollectionNameAttribute), false);
            if (attr.Length == 0)
            {
                throw new InvalidOperationException(nameof(attr));
            }

            return ((CollectionNameAttribute[])attr).First()
                .Name;
        }
    }
}