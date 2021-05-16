using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using KennardHireGomokuApi.Controllers.ReponseModels;
using KennardHireGomokuApi.DataModels;

namespace KennardHireGomokuApi.Utils.Mappings
{
	public class GameInformationMappingProfile : Profile
	{
		public GameInformationMappingProfile() => CreateMap<StoneModel,StoneInfoResponseModel>();
	}
}
