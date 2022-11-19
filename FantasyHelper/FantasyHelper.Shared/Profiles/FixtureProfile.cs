using AutoMapper;
using FantasyHelper.Data.Models;
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
        }
    }
}
