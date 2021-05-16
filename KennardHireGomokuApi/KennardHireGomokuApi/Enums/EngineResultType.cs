using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KennardHireGomokuApi.Enums
{
	public enum EngineResultType
	{
		StoneAccepted,
		StoneRejected,
		ProposedStoneLocationOccupied,
		BlackWon,
		WhiteWon,
		IncorrectPlayer,
	}
}
