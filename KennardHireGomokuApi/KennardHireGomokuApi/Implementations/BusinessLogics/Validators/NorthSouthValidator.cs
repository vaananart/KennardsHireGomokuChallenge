
using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class NorthSouthValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.NorthSouth;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var stonesFromNorth = from t in stones
								  from u in newStone.NorthFiveSteps()
								  where t.Row == u.row
										  && t.Column == u.col
								  select t;

			var stonesFromSouth = from t in stones
								  from u in newStone.SouthFiveSteps()
								  where t.Row == u.row
										  && t.Column == u.col
								  select t;

			var conbinedStoneCollection = stonesFromNorth.ToList();

			conbinedStoneCollection.AddRange(stonesFromSouth);
			conbinedStoneCollection.Add(newStone);

			foreach (StoneModel stone in conbinedStoneCollection)
			{
				var filteredStones = from t in conbinedStoneCollection
									 from u in stone.NorthFiveSteps()
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
