using AutoMapper;
using FantasyHelper.Data.Models;
using FantasyHelper.Shared.Dtos;
using FantasyHelper.Shared.Dtos.External.Allsvenskan;
using FantasyHelper.Shared.Dtos.External.FPL;

namespace FantasyHelper.Shared.Profiles
{
    public class FixtureProfile : Profile
    {
        public FixtureProfile()
        {
            CreateMap<FPLFixtureDto, Fixture>();
            CreateMap<ASFixtureDto, Fixture>();
            CreateMap<Fixture, FixtureUpcomingDto>()
                .ForMember(dest => dest.HomeTeamShortName, opt => opt.MapFrom(f => f.HomeTeam!.ShortName))
                .ForMember(dest => dest.AwayTeamShortName, opt => opt.MapFrom(f => f.AwayTeam!.ShortName));
        }
    }
}
