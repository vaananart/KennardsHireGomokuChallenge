
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Controllers.ReponseModels
{
	public class StoneInfoResponseModel
	{
		public string Colour { get; set; }
		public int Column { get; set; }
		public int Row { get; set; }
		public int Index { get; set; }
	}
}
