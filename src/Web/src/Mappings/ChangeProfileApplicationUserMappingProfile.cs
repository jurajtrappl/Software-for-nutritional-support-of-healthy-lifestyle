using Application.Infrastructure.Entities;
using Application.Web.Areas.App.Models.Settings;

namespace Application.Web.Mappings
{
    /// <summary>
    /// Defines mapping mechanism between <see cref="ChangeProfileInformationModel" /> and <see cref="ApplicationUser"
    /// />. Works both ways.
    /// </summary>
    public sealed class ChangeProfileApplicationUserMappingProfile : AutoMapper.Profile
    {
        public ChangeProfileApplicationUserMappingProfile()
        {
            CreateMap<ChangeProfileInformationModel, ApplicationUser>().ReverseMap();
        }
    }
}