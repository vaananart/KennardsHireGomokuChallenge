using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using KennardHireGomokuApi.Controllers.RequestModels;
using KennardHireGomokuApi.DataModels;

namespace KennardHireGomokuApi.Utils.Mappings
{
	public class GameRequestMappingProfile : Profile
	{
		public GameRequestMappingProfile() => CreateMap<NewGameRequest, GameModel>();
	}
}
