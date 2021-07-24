using Application.Core.Common.Entities;

namespace Application.Core.Interfaces
{
    /// <summary>
    /// UI service contract that supports scheduling appropriate plans depending on the application plan.
    /// </summary>
    public interface IPlansSchedulingService
    {
        /// <summary>
        /// Assigns appropriate plans to the user.
        /// </summary>
        /// <param name="user">user to whom the plans are assigned.</param>
        void CreatePlansFor(IApplicationUser user);
    }
}