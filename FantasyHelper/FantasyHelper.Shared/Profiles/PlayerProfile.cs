﻿using AutoMapper;
using FantasyHelper.Data.Models;
using FantasyHelper.Shared.Dtos;
using FantasyHelper.Shared.Dtos.External.Allsvenskan;
using FantasyHelper.Shared.Dtos.External.FPL;

namespace FantasyHelper.Shared.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<FPLPlayerDto, Player>();
            CreateMap<ASPlayerDto, Player>();
            CreateMap<Player, PlayerReadDto>();
            CreateMap<Player, PlayerPriceChangeDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(p => p.Team!.Name));
        }
    }
}