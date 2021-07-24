using Application.Core.Common.Entities;
using Application.Core.Services.LogicProviders;

namespace Application.Core.Services.Base
{
    /// <summary>
    /// Base type for application plans generators.
    /// </summary>
    internal abstract class ApplicationPlansGenerator
    {
        /// <summary>
        /// Creates appropriate plans depending on the application plan selected by the user.
        /// </summary>
        /// <param name="creator">common logic for generating plans.</param>
        /// <param name="user">user for who are plans being generated.</param>
        internal virtual void CreateFor(ApplicationPlansCreator creator, IApplicationUser user)
        {
            creator.CreateDrinkingRegime(user);
            creator.CreateMealPlan(user);
        }
    }
}