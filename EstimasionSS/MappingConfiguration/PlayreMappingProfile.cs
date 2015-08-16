using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EstimasionSS.DataContext;
using EstimasionSS.Models;

namespace EstimasionSS.MappingConfiguration
{
    public class PlayreMappingProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();


            AutoMapper.Mapper.CreateMap<Models.PlayerModel, Player>();
            AutoMapper.Mapper.CreateMap<Player, PlayerModel>();
        }

    }
}