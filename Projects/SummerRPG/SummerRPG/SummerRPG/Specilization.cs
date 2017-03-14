using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SummerRPG
{
	/// <summary>
	/// Will take in the modifiers that differenciate each Specilization
	/// and apply them to whatever data needs to be held in the Specilization
	/// </summary>
	public class Specilization
	{
		// fields
		public double healthmod { get; set; }
		public double speedmod { get; set; }
		// Allows for special instructions for evasive moves.
		public bool isEvas { get; set; }
		// THE CONNECTION TO THE MOVELIST

		public Specilization(double _healthmod, double _speedmod, bool _isEvas)
		{
			healthmod = _healthmod;
			isEvas = _isEvas;
			speedmod = _speedmod;
		}
		


	}
}
