using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KennardHireGomokuApi.DataModels;
using KennardHireGomokuApi.Enums;

namespace KennardHireGomokuApi.Interfaces.RuleLogic
{
	public interface IGeneralRuleValidator
	{
		public ValidatorType Type { get; }

		public bool Validate(StoneModel newStone, IEnumerable<StoneModel> stones = null);
	}
}
