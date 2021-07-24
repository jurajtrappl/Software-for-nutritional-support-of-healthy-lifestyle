using Application.Core.Common.Constants;
using Application.Web.Areas.App.Models.Settings;

namespace Application.Web.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ChangeEatingTimeConfigModel" />.
    /// </summary>
    public static class ChangeEatingTimeConfigModelExtensions
    {
        /// <summary>
        /// Indicates whether the order of times is correct.
        /// </summary>
        /// <param name="model">submitted form.</param>
        /// <returns>True if times are in order; otherwise False.</returns>
        public static bool AreTimesFollowingEachOther(this ChangeEatingTimeConfigModel model)
        {
            if (model is null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }

            return model.Breakfast < model.MidMorningSnack &&
                model.MidMorningSnack < model.Lunch &&
                model.Lunch < model.AfternoonSnack &&
                model.AfternoonSnack < model.Dinner &&
                model.Dinner < model.Supper;
        }

        /// <summary>
        /// Indicates whether mandatory intervals are observed between times.
        /// </summary>
        /// <param name="model">contains times to check.</param>
        /// <returns>True if mandatory intervals are observed; otherwise False.</returns>
        public static bool AreIntervalsBetweenMeals(this ChangeEatingTimeConfigModel model)
        {
            if (model is null)
            {
                throw new System.ArgumentNullException(nameof(model));
            }

            return model.Breakfast - model.MidMorningSnack >= EatingTimeDefaults.MinimumIntervalBetweenMeals &&
                model.MidMorningSnack - model.Lunch >= EatingTimeDefaults.MinimumIntervalBetweenMeals &&
                model.Lunch - model.AfternoonSnack >= EatingTimeDefaults.MinimumIntervalBetweenMeals &&
                model.AfternoonSnack - model.Dinner >= EatingTimeDefaults.MinimumIntervalBetweenMeals &&
                model.Dinner - model.Supper >= EatingTimeDefaults.MinimumIntervalBetweenMeals;
        }
    }
}