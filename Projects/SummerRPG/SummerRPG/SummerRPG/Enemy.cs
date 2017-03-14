using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SummerRPG
{
    // All Enemys have
        // Their own health (way to display it?)
		// Their own kingdom
		// Their own specilization
		// Their own level
		// Thier own abbrivated move list moves (based on kingdom and specilization)
		// Their own set of moves (randomly selected from list)
		 
    public class Enemy : Person
    {
		// Fields
	     public List<string> MoveNames { get; set; }



		public Enemy(string _name, string _purpose, int _level, Kingdom _kingdom, Specilization _spec)
		{
			level = _level;
			kingdom = _kingdom;
			spec = _spec;
			health = health * level;
			currentHealth = health;
			speed = speed * level;
			name = _name;
			purpose = _purpose;
			MoveNames = new List<string>();
		}

	     public void AddMove(string move1)
	     {
		     MoveNames.Add(move1);
	     }

	    public void AddMove(List<string> inMoves)
	    {
		    foreach (var movNam in inMoves)
		    {
			    MoveNames.Add(movNam);
		    }
	    }

	     public void SelectRandomMove()
	     {
		     var RNG = new Random();
		     attackName = MoveNames[RNG.Next(0, MoveNames.Count)];
	     }

	     public void SelectRandomPerson(Friendly targ1, Friendly targ2)
	     {
		     var hold = new List<Person>()
		     {
			     targ1,
			     targ2
		     };

			var RNG = new Random();
		     target = hold[RNG.Next(0, hold.Count)];
	     }
		/// <summary>
		/// Runs all necesary logic for choosing attacks and targets.
		/// ADDMOVE MUST BE CALLED FIRST.
		/// </summary>
		/// <param name="target1"></param>
		/// <param name="target2"></param>
	    public void LoadAI(Friendly target1, Friendly target2)
	    {
			SelectRandomMove();
			SelectRandomPerson(target1, target2);   
	    }

    }
}