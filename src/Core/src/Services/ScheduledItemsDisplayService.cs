using Application.Core.Common.Entities;
using Application.Core.Common.Enums;
using Application.Core.Infrastructure;
using Application.Core.Interfaces;
using Application.Core.Services.Base;
using System;

namespace Application.Core.Services
{
    /// <summary>
    /// Implementation of UI scheduled items displayer contract.
    /// </summary>
    public sealed class ScheduledItemsDisplayService : IScheduledItemsDisplayService
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public void SelectScheduledItemsForDate(IApplicationUser user, DateTime date, dynamic viewBag)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            LogicProviderSelector<ScheduledItemsDisplayer>.GetInstance((ApplicationPlan)user.AppPlan!)
                .SetItems(user, date, viewBag);
        }
    }
}