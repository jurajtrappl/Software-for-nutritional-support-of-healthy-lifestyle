using Application.Infrastructure.Entities;
using Application.Web.Areas.Account.Models.Home;

namespace Application.Web.Mappings
{
    /// <summary>
    /// Defines mapping mechanism from <see cref="RegistrationModel" /> to <see cref="ApplicationUser" />.
    /// </summary>
    public sealed class RegistrationModelApplicationUserMappingProfile : AutoMapper.Profile
    {
        public RegistrationModelApplicationUserMappingProfile()
        {
            CreateMap<RegistrationModel, ApplicationUser>();
        }
    }
}