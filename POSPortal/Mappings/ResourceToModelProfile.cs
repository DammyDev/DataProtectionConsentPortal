using AutoMapper;
using POSPortal.Data;
using POSPortal.Resources;

namespace POSPortal.Mappings
{
    public class ResourceToModelProfile :Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SavePOSRequestResource, POSRequest>()
                .ForMember(src => src.Status, opt => opt.MapFrom(src => (EStatus)src.Status));
        }
    }
}
