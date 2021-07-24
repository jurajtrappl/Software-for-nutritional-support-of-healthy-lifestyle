using Application.Core.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Application.Web.Views.Shared.Components
{
    /// <summary>
    /// UI component that renders flag for the current culture.
    /// </summary>
    public sealed class FlagIconViewComponent : ViewComponent
    {
        /// <summary>
        /// List of supported cultures that web application is translated to.
        /// </summary>
        private readonly static Dictionary<string, string> _cultureToFlagStyles;

        /// <summary>
        /// Map between culture descriptors and css classes that displays flag for the culture.
        /// </summary>
        static FlagIconViewComponent()
        {
            _cultureToFlagStyles = new Dictionary<string, string>()
            {
                { CultureDescriptors.Slovak, "flagSk" },
                { CultureDescriptors.Czech, "flagCz" }
            };
        }

        public IViewComponentResult Invoke(string culture, string returnUrl)
        {
            string flag = _cultureToFlagStyles.Values.First();
            if (_cultureToFlagStyles.ContainsKey(culture))
            {
                flag = _cultureToFlagStyles[culture];
            }

            FlagIconViewModel model = new(flag, culture, returnUrl);
            return View(model);
        }
    }
}