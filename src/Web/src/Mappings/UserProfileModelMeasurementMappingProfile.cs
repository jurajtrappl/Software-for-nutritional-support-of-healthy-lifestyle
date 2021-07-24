using Application.Infrastructure.Entities;
using Application.Web.Areas.App.Models.Home;

namespace Application.Web.Mappings
{
    /// <summary>
    /// Defines mapping mechanism from <see cref="UserProfileModel" /> to <see cref="Measurement" />.
    /// </summary>
    public sealed class UserProfileModelMeasurementMappingProfile : AutoMapper.Profile
    {
        public UserProfileModelMeasurementMappingProfile()
        {
            CreateMap<UserProfileModel, Measurement>();
        }
    }
}