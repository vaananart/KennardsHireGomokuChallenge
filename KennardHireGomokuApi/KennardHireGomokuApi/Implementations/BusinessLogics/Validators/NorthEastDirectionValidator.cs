using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class NorthEastDirectionValidator : IDirectionalLogicValidator
	{
		public ValidatorType Type => ValidatorType.NorthEast;

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var matchedstones = from t in stones
								from u in newStone
										.NorthEastFiveSteps()
								where t.Row == u.row
										&& t.Column == u.col
								select t;

			if (matchedstones.Count() == 4)
				return true;

			return false;
		}
	}
}
