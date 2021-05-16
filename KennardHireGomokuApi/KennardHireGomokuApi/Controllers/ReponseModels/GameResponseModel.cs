using System;

namespace KennardHireGomokuApi
{
	public class GameResponseModel
	{
		public Guid GameBoardGuid { get; set; }
		public Guid WPlayerGuid { get; set; }
		public Guid BPlayerGuid { get; set; }

	}
}
