using AutoMapper;
using FantasyHelper.Data.Models;
using FantasyHelper.Shared.Dtos;
using FantasyHelper.Shared.Dtos.External.Allsvenskan;
using FantasyHelper.Shared.Dtos.External.FPL;

namespace FantasyHelper.Shared.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<FPLTeamDto, Team>();
            CreateMap<ASTeamDto, Team>();
            CreateMap<Team, TeamBestFixtureDto>();
        }
    }
}
