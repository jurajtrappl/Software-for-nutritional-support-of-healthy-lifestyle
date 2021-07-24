using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Infrastructure;
using Application.Core.Interfaces;
using Application.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Core.Services
{
    /// <summary>
    /// Provides methods that checks whether the given user is suitable for the selected application plan.
    /// </summary>
    public sealed class ApplicationPlansSuitabilityService : IApplicationPlansSuitabilityService
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="applicationPlan"><inheritdoc /></param>
        /// <param name="measurement"><inheritdoc /></param>
        /// <returns><inheritdoc /></returns>
        public bool IsMeetingRequirements(ApplicationPlan applicationPlan, IMeasurement measurement, bool isFrequentlyExercising)
            => LogicProviderSelector<ApplicationPlanSuitabilityFilter>.GetInstance(applicationPlan)
                .IsSuitable(measurement, isFrequentlyExercising);

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="measurement"><inheritdoc /></param>
        public IEnumerable<ApplicationPlan> FindSuitablePlans(IMeasurement measurement, bool isFrequentlyExercising)
            => Enum.GetValues(typeof(ApplicationPlan))
                .Cast<ApplicationPlan>()
                .Where(p => IsMeetingRequirements(p, measurement, isFrequentlyExercising))
                .Select(p => p);
    }
}