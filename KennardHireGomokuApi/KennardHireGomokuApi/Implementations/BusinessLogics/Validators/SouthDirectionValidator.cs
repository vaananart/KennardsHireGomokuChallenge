using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class SouthDirectionValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.South;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var matchedstones = from t in stones
								from u in newStone
										.SouthFiveSteps()
								where t.Row == u.row
										&& t.Column == u.col
								select t;

			if (matchedstones.Count() == 4)
				return true;

			return false;
		}
	}
}
