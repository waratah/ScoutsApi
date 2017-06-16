using AutoMapper;
using Scouts.Core.Model;

namespace Scouts.Maps
{
    public class ScoutDtoMap : Profile
    {
        public ScoutDtoMap()
        {
            CreateMap<Scout, ScoutDTO>().ReverseMap();
        }
    }
}
