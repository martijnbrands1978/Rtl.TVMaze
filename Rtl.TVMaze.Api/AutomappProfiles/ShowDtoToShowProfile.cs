using AutoMapper;
using Rtl.TVMaze.Domain.Model;
using Rtl.TVMaze.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rtl.TVMaze.Api.AutomappProfiles
{
    public class ShowDtoToShowProfile : Profile
    {
        public ShowDtoToShowProfile()
        {
            CreateMap<TvMazeShowDto, Show>();
        }
    }
}
