using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SummerRPG
{
	/// <summary>
	/// Will take in the modifiers that differenciate each Kingdom.
	/// Damage, Defense, Accuracy. 
	/// </summary>
	public class Kingdom
	{
		// fields
		
		//double healthmod;
		public double damagemod { get; set; }
		public double defencemod { get; set; }
		public double accuracymod { get; set; }
		public double speedmod { get; set; }

		/// <summary>
		/// Will take in the modifiers that differenciate each Kingdom
		/// and apply them to whatever data needs to be held in the Kingdom
		/// </summary>
		/// <param name="_healthmod"></param>
		/// <param name="_damagemod"></param>
		/// <param name="_defencemod"></param>
		/// <param name="_accuracymod"></param>
		public Kingdom(double _damagemod, double _defencemod, double _accuracymod, double _speedmod)
		{
			// healthmod = _healthmod;
			damagemod = _damagemod;
			defencemod = _defencemod;
			accuracymod = _accuracymod;
			speedmod = _speedmod;
		}


	}
}
