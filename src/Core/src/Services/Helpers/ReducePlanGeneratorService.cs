using Application.Core.Attributes;
using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Services.Base;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides logic for generating scheduled items for user with the reduce application plan.
    /// </summary>
    [ForPlan(ApplicationPlan.Reduce)]
    internal sealed class ReducePlanGeneratorService : ApplicationPlansGenerator
    {
        /// <summary>
        /// Creates a drinking regime, a meal plan and an exercise plan for the given user.
        /// </summary>
        /// <param name="creator">contains logic on creating plans.</param>
        /// <param name="user">user for whom plans are being generated.</param>
        internal override void CreateFor(ApplicationPlansCreator creator, IApplicationUser user)
        {
            creator.CreateDrinkingRegime(user);
            creator.CreateExercisePlan(user);
            creator.CreateMealPlan(user);
        }
    }
}