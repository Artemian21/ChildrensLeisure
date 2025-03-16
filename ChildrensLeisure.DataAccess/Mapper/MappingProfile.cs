using AutoMapper;
using ChildrensLeisure.DataAccess.Entity;
using ChildrensLeisure.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderModel>().ReverseMap();

            CreateMap<Zone, ZoneModel>().ReverseMap();

            CreateMap<Attraction, AttractionModel>().ReverseMap();

            CreateMap<EventProgram, EventProgramModel>().ReverseMap();

            CreateMap<FairyCharacter, FairyCharacterModel>().ReverseMap();

            CreateMap<OrderZone, OrderZoneModel>().ReverseMap();

            CreateMap<OrderAttraction, OrderAttractionModel>().ReverseMap();

            CreateMap<OrderFairyCharacter, OrderFairyCharacterModel>().ReverseMap();
        }
    }
}
