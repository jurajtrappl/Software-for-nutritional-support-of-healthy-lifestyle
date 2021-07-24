using Application.Core.Common.Entities;
using System.Collections.Generic;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// Infrastructure service contract for ingredients database.
    /// </summary>
    public interface IIngredientsService
    {
        /// <summary>
        /// Gets all ingredients that are appropriate for patients meal plan.
        /// </summary>
        /// <param name="allergens">List of prohibited allergens.</param>
        /// <returns>All appropriate ingredients from the database.</returns>
        IReadOnlyDictionary<string, List<IIngredient>> GetAllIngredients(List<int> allergens);
    }
}