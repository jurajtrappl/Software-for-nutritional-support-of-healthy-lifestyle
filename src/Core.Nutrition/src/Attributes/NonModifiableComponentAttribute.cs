using System;

namespace Application.Core.Nutrition.Attributes
{
    /// <summary>
    /// Marks a meal component so that the amount of its ingredients can not be modified once computed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class NonModifiableComponentAttribute : Attribute
    {
    }
}