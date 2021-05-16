
using AutoMapper;

using KennardHireGomokuApi.DataModels;

namespace KennardHireGomokuApi.Utils.Mappings
{
	public class GameMappingProfile	: Profile
	{
		public GameMappingProfile() => CreateMap<GameModel, GameResponseModel>();
	}
}
