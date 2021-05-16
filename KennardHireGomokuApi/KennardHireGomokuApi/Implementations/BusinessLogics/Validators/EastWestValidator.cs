
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class EastWestValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.EastWest;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var stonesFromEast = from t in stones
								 from u in newStone.EastFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var stonesFromWest = from t in stones
								 from u in newStone.WestFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var conbinedStoneCollection = stonesFromEast.ToList();

			conbinedStoneCollection.AddRange(stonesFromWest);
			conbinedStoneCollection.Add(newStone);

			foreach (StoneModel stone in conbinedStoneCollection)
			{
				var filteredStones = from t in conbinedStoneCollection
									 from u in stone.EastFiveSteps()
									 where t.Row == u.row 
											&& t.Column == u.col
									 select t;
				if (filteredStones.Count() == 4)
				{
					return true;
				}
			}

			return false;
		}
	}
}
