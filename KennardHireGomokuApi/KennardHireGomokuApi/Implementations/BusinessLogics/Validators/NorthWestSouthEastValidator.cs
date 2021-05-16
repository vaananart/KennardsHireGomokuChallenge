
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class NorthWestSouthEastValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.NorthWestSouthEast;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var stonesFromNorthWest = from t in stones
								 from u in newStone.NorthWestFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var stonesFromSouthEast = from t in stones
								 from u in newStone.SouthEastFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var conbinedStoneCollection = stonesFromNorthWest.ToList();

			conbinedStoneCollection.AddRange(stonesFromSouthEast);
			conbinedStoneCollection.Add(newStone);
			foreach (StoneModel stone in conbinedStoneCollection)
			{
				var filteredStones = from t in conbinedStoneCollection
									 from u in stone.NorthWestFiveSteps()
									 where t.Row == u.row && t.Column == u.col
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
