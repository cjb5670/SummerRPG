using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SummerRPG
{
	// Each Friendly has
	// Their own health and a way to display it
	// Their own Kingdom
	// Their own Specilization
	// Their own level
	// Their own ammount of XP
	// Their own movelist
	// Their own set of moves

	public class Friendly : Person
	{
		
		
		public int XP { get; set; }



		public Friendly(string _name, string _purpose, int _level, Kingdom _kingdom, Specilization _spec)
		{
			level = _level;
			kingdom = _kingdom;
			spec = _spec;
			health = health * level;
			currentHealth = health;
			speed = speed * level;
			name = _name;
			purpose = _purpose;
		}

		
	}

    }

