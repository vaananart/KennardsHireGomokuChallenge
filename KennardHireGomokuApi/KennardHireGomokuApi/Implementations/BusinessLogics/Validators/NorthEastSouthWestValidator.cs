
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class NorthEastSouthWestValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.NorthEastSouthWest;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var stonesFromNorthEast = from t in stones
								 from u in newStone.NorthEastFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var stonesFromSouthWest = from t in stones
								 from u in newStone.SouthWestFiveSteps()
								 where t.Row == u.row
										 && t.Column == u.col
								 select t;

			var conbinedStoneCollection = stonesFromNorthEast.ToList();

			conbinedStoneCollection.AddRange(stonesFromSouthWest);
			conbinedStoneCollection.Add(newStone);

			foreach (StoneModel stone in conbinedStoneCollection)
			{
				var filteredStones = from t in conbinedStoneCollection
									 from u in stone.NorthEastFiveSteps()
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
