using AutoMapper;
using POSPortal.Data;
using POSPortal.Resources;
using POSPortal.Extensions;

namespace POSPortal.Mappings
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<POSRequest, POSRequestResource>()
                    .ForMember(src => src.Status,
                               opt => opt.MapFrom(src => src.Status.ToDescriptionString()));

        }

    }
}
