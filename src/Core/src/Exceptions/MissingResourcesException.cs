using Application.Core.Common.Constants;
using System;

namespace Application.Core.Exceptions
{
    /// <summary>
    /// Indicates missing resource when required.
    /// </summary>
    public sealed class MissingResourcesException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MissingResourcesException" /> with the given resources and resource key.
        /// </summary>
        /// <param name="resources">name of the resources.</param>
        /// <param name="key">resource key.</param>
        public MissingResourcesException(string resources, string key)
            : base(string.Format(ExceptionMessages.MissingResources, key, resources))
        {
        }
    }
}