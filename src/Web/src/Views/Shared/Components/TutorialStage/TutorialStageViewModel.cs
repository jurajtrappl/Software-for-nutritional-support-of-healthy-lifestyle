using Application.Core.Common.Constants;
using Microsoft.AspNetCore.Mvc.Localization;
using System;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// View model for tutorial stage component.
    /// </summary>
    public sealed class TutorialStageViewModel
    {
        /// <summary>
        /// Gets or initializes name of the step in the tutorial.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or initializes localizer.
        /// </summary>
        public IViewLocalizer Localizer { get; init; }

        /// <summary>
        /// Gets or initializes number before header.
        /// </summary>
        public int OrderNum { get; init; }

        /// <summary>
        /// Gets or initializes path of the image.
        /// </summary>
        public string ImgPath { get; init; }

        /// <summary>
        /// Gets or initializes image alt.
        /// </summary>
        public string ImgAlt { get; init; }

        /// <summary>
        /// Initialize a new instance of <see cref="TutorialStageViewModel"/> with the given name, localizer, 
        /// ordering number, image path and image alt.
        /// </summary>
        /// <param name="name">name of the step in the tutorial.</param>
        /// <param name="localizer">resource localizer.</param>
        /// <param name="orderNum">number before header.</param>
        /// <param name="imgPath">path of the image to display next to the content.</param>
        /// <param name="imgAlt">html attribute alt of the image.</param>
        public TutorialStageViewModel(string name, IViewLocalizer localizer, int orderNum, string imgPath, string imgAlt)
        {
            (Name, ImgPath, ImgAlt, Localizer, OrderNum) =
                (name, imgPath, imgAlt, localizer, orderNum);
        }
    }
}