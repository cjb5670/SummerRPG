using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SummerRPG
{
	// All people have
	// Health
	// Acess to the entire list of moves
	// A Kingdom
	// A specality
	// A level
	public class Person
	{
		public string name { get; set; }
		public string purpose { get; set; } // Unitl there are symbols for kingdoms and specilizations, just use text.
		public int currentHealth { get; set; }
		public int health { get; set; } = 100;	
		public int speed { get; set; } = 10;
		public Kingdom kingdom { get; set; }
		public Specilization spec { get; set; }
		public int level { get; set; }
		public int damage { get; set; }
		public int defense { get; set; }
		public int accuracy { get; set; }
		public bool evasiveDodgeCheck { get; set; } = false;
		public Person target { get; set; }
		public string attackName { get; set; }
		

		public bool checkAlive()
		{
			return currentHealth >= 0;
		}


	}
}
