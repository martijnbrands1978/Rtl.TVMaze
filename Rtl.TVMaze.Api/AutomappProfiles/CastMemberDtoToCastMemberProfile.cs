using AutoMapper;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Api.AutomappProfiles
{
    public class CastMemberDtoToCastMemberProfile : Profile
    {
        public CastMemberDtoToCastMemberProfile()
        {
            CreateMap<TvMazeCastMemberDto, CastMember>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.Person.BirthDay != null ? (DateTime?)DateTime.Parse(src.Person.BirthDay) : null));
        }
    }
}
