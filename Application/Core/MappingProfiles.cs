using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Maps fields from one activity to another
            CreateMap<Activity, Activity>();
        }
    }
}