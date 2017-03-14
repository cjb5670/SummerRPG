using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerRPG
{
	class Config
		
	{
		public double caster { get; set; }
		public int export { get; set; }

		//TEMPORARY BATTLE INFO STRING.
		public string BatInfFull { get; set; }

		/// <summary>
		/// Modifies basnum
		/// </summary>
		/// <param name="basenum">The number to be modified</param>
		/// <param name="mod1">a modifier</param>
		/// <returns></returns>
		public int dubconvert(int basenum, double level, double mod1)
		{
			caster = basenum * (level * 0.5) * mod1 * 10;
			export = (int)caster;
			return export;
		}

		/// <summary>
		/// Modifies basnum
		/// </summary>
		/// <param name="basenum">The number to be modified</param>
		/// <param name="mod1">a modifier</param>
		/// <param name="mod2">a modifier</param>
		/// <returns></returns>
		public int dubconvert(int basenum, double level, double mod1, double mod2)
		{
			caster = basenum * (level * 0.5) * mod1 * mod2 * 10;
			export = (int)caster;
			return export;
		}

		/// <summary>
		/// Modifies basnum
		/// </summary>
		/// <param name="basenum">The number to be modified</param>
		/// <param name="mod1">a modifier</param>
		/// <param name="mod2">a modifier</param>
		/// <param name="mod3">a modifier</param>
		/// <returns></returns>
		public int dubconvert(int basenum, double level, double mod1, double mod2, double mod3)
		{
			caster = basenum * (level*0.5) * mod1 * mod2 * mod3 * 10;
			export = (int)caster;
			return export;
		}

		public int getRandom(int lowest, int highest)
		{
			Random RNG = new Random();
			return RNG.Next(lowest, highest + 1);
		}

		
	}
}
