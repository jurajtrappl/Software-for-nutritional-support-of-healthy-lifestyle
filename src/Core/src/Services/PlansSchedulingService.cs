using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Infrastructure;
using Application.Core.Interfaces;
using Application.Core.Services.Base;
using Application.Core.Services.LogicProviders;
using System;

namespace Application.Core.Services
{
    /// <summary>
    /// Implementation of UI plan scheduling service contract.
    /// </summary>
    public sealed class PlansSchedulingService : IPlansSchedulingService
    {
        /// <summary>
        /// Logic provider for creating plans.
        /// </summary>
        private readonly ApplicationPlansCreator _generatingLogic;

        /// <summary>
        /// Initializes a new instance of <seealso cref="PlansSchedulingService" /> with the given logic provider.
        /// </summary>
        /// <param name="generatingLogic">logic provider for creating plans.</param>
        public PlansSchedulingService(ApplicationPlansCreator generatingLogic)
        {
            _generatingLogic = generatingLogic;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <param name="user">user for whom the plans are being created.</param>
        public void CreatePlansFor(IApplicationUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            LogicProviderSelector<ApplicationPlansGenerator>.GetInstance((ApplicationPlan)user.AppPlan!)
                .CreateFor(_generatingLogic, user);
        }
    }
}