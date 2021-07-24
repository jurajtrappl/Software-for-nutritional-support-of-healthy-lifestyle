using Application.Core.Common.Entities;
using Application.Core.Common.Interfaces;
using Application.Core.Interfaces;
using System;

namespace Application.Core.Services.LogicProviders
{
    /// <summary>
    /// Provides methods working with schedulers and generate plans.
    /// </summary>
    public sealed class ApplicationPlansCreator
    {
        /// <summary>
        /// Drinking regime scheduler.
        /// </summary>
        private readonly IDrinkingRegimePlanScheduler _drinkingRegimeScheduler;

        /// <summary>
        /// Exercise database service.
        /// </summary>
        private readonly IExercisesService _exercisesService;

        /// <summary>
        /// Exercise plan scheduler.
        /// </summary>
        private readonly IExercisePlanScheduler _exerciseScheduler;

        /// <summary>
        /// Ingredients database service.
        /// </summary>
        private readonly IIngredientsService _ingredientsService;

        /// <summary>
        /// Meal plan scheduler.
        /// </summary>
        private readonly IMealPlanScheduler _mealScheduler;

        /// <summary>
        /// Initializes a new instance of <seealso cref="ApplicationPlansGenerator" /> with the given schedulers and
        /// database services (DI).
        /// </summary>
        /// <param name="drinkingRegimeScheduler">drinking regime scheduler.</param>
        /// <param name="exercisesService">exercise database service.</param>
        /// <param name="exerciseScheduler">exercise plan scheduler.</param>
        /// <param name="ingredientsService">ingredients database service.</param>
        /// <param name="mealScheduler">meal plan scheduler.</param>
        public ApplicationPlansCreator(
            IDrinkingRegimePlanScheduler drinkingRegimeScheduler,
            IExercisesService exercisesService,
            IExercisePlanScheduler exerciseScheduler,
            IIngredientsService ingredientsService,
            IMealPlanScheduler mealScheduler)
        {
            (_drinkingRegimeScheduler, _exercisesService, _exerciseScheduler, _ingredientsService, _mealScheduler) =
                (drinkingRegimeScheduler, exercisesService, exerciseScheduler, ingredientsService, mealScheduler);
        }

        /// <summary>
        /// Schedules an exercise plan for the given user.
        /// </summary>
        /// <param name="user">user for whom the exercise plan is being scheduled.</param>
        public void CreateExercisePlan(IApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var availableExercises = _exercisesService.GetAllExercises();
            user.Exercises = _exerciseScheduler.Configure(user.Measurement!.Weight, availableExercises)
                .Schedule();
        }

        /// <summary>
        /// Schedules a drinking regime plan for the given user.
        /// </summary>
        /// <param name="user">user for whom the drinking regime is being scheduled.</param>
        public void CreateDrinkingRegime(IApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.DrinkingRegime = _drinkingRegimeScheduler.Configure(user.Measurement!.Weight)
                .Schedule();
        }

        /// <summary>
        /// Schedules a meal plan for the given user.
        /// </summary>
        /// <param name="user">user for whom the meal plan is being scheduled.</param>
        public void CreateMealPlan(IApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var filteredIngredients = _ingredientsService.GetAllIngredients(user.Allergens);
            user.Meals = _mealScheduler.Configure(filteredIngredients, user)
                .Schedule();
        }
    }
}