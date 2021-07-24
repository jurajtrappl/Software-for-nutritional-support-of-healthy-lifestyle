using Application.Core.Common.Constants;
using System;

namespace Application.Core.Nutrition.Attributes
{
    /// <summary>
    /// Name of the storage organizational unit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class CollectionNameAttribute : Attribute
    {
        /// <summary>
        /// Gets name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializes a new instance of <seealso cref="CollectionNameAttribute" /> with the given name.
        /// </summary>
        /// <param name="name">Name of the organization unit.</param>
        public CollectionNameAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.StringIsNullOrEmpty, nameof(name)));
            }

            Name = name;
        }
    }
}