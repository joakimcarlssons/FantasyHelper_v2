using AutoMapper;
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
            CreateMap<Player, PlayerPriceChangeDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(p => p.Team!.Name));
            CreateMap<Player, PlayerNewsDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(p => p.Team!.Name))
                .ForMember(dest => dest.News, opt => opt.MapFrom(p => String.IsNullOrEmpty(p.News) ? "Available" : p.News));
            CreateMap<Player, PlayerTopPerformerDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(p => p.Team!.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(p => p.Price / 10));
            CreateMap<Player, PlayerTransferDto>()
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(p => p.Team!.Name));
        }
    }
}
