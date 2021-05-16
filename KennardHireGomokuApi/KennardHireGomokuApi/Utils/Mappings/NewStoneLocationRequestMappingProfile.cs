
using AutoMapper;

using KennardHireGomokuApi.Controllers.RequestModels;
using KennardHireGomokuApi.DataModels;

namespace KennardHireGomokuApi.Utils.Mappings
{
	public class NewStoneLocationRequestMappingProfile : Profile
	{
		public NewStoneLocationRequestMappingProfile() => CreateMap<NewStoneLocationRequest, StoneModel>();
	}
}
