using AutoMapper;
using FantasyHelper.Data.Models;
using FantasyHelper.Shared.Dtos.External.Allsvenskan;
using FantasyHelper.Shared.Dtos.External.FPL;

namespace FantasyHelper.Shared.Profiles
{
    public class GameweekProfile : Profile
    {
        public GameweekProfile()
        {
            CreateMap<FPLGameweekDto, Gameweek>();
            CreateMap<ASGameweekDto, Gameweek>();
        }
    }
}
