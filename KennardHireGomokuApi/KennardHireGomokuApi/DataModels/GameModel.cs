using System;
using System.Collections.Generic;

using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.DataModels
{
	public class GameModel
	{	
		public Guid GameBoardGuid { get; set; }
		public string WPlayerName { get; set; }
		public Guid WPlayerGuid { get; set; }
		public string BPlayerName { get; set; }
		public Guid BPlayerGuid { get; set; }
		public IList<StoneModel> BlackStones {get; set;}
		public IList<StoneModel> WhiteStones { get; set; }

		public Guid CurrentPlayer { get; set; }
		public int TurnCount { get; set; }

		public bool IsComplete { get; set; }
		public EngineResultType WhoWon { get; set; }
		public GameModel()
		{
			BlackStones = new List<StoneModel>();
			WhiteStones = new List<StoneModel>();
		}
	}
}
