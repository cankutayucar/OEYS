using AutoMapper;
using OEYS.WEB.Models.Dtos.Activities;
using OEYS.WEB.Models.Entities;

namespace OEYS.WEB.Mappers
{
    public class DtoToEntityMapper : Profile
    {
        public DtoToEntityMapper()
        {
            CreateMap<ActivityUpdateDto, Activity>().ReverseMap();
        }
    }
}
