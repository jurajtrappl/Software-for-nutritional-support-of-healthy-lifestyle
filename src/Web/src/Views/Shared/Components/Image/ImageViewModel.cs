using Application.Core.Common.Constants;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for the image component.
    /// </summary>
    public sealed class ImageViewModel
    {
        /// <summary>
        /// Gets or initializes image path.
        /// </summary>
        public string Path { get; init; }

        /// <summary>
        /// Gets or initializes image alt html attribute.
        /// </summary>
        public string Alt { get; init; }

        /// <summary>
        /// Gets or initializes css class.
        /// </summary>
        public string Class { get; init; }

        /// <summary>
        /// Initializes a new instance of <see cref="ImageViewModel"/>
        /// with the given image path, alt and class.
        /// </summary>
        /// <param name="path">path of the image directing to wwwroot folder (static files).</param>
        /// <param name="alt">image alt.</param>
        /// <param name="class">image css class.</param>
        public ImageViewModel(string path, string alt, string @class)
        {
            (Path, Alt, Class) = (path, alt, @class);
        }
    }
}