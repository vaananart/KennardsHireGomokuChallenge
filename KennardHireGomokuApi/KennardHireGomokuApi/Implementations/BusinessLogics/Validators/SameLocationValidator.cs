using System.Collections.Generic;
using System.Linq;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;
using KennardHireGomokuApi.Interfaces.RuleLogic;

namespace KennardHireGomokuApi.Implementations.BusinessLogics.Validators
{
	public class SameLocationValidator : IGeneralRuleValidator
	{
		public ValidatorType Type => ValidatorType.SameLocation; 

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null)
		{
			var existingStones = stones.Where(x => x.Column == newStone.Column
												&& x.Row == newStone.Row).FirstOrDefault();
			if (existingStones != null)
				return false;
			return true;
		}
	}
}
