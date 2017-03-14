using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerRPG
{
	/// <summary>
	/// Contains all logic for running a battle
	/// </summary>
    class Battle
    {
		public Config config { get; set; }
		public DrawBattle drawBattle { get; set; }
		public Friendly F1 { get; set; }
		public Friendly F2 { get; set; }
		public Enemy E1 { get; set; }
		public Enemy E2 { get; set; }
		public MoveListHolder Reader { get; set; } = new MoveListHolder();
		public int[] F1ADAarray { get; set; }
		public int[] F2ADAarray { get; set; }
		public int[] E1ADAarray { get; set; }
		public int[] E2ADAarray { get; set; }
		List<int[]> ArrayList { get; set; }
		List<Person> CombatantList { get; set; }




		public Battle(Friendly _F1, Friendly _F2, Enemy _E1, Enemy _E2, SpriteBatch sb, SpriteFont sf)
		{
			F1 = _F1;
			F2 = _F2;
			E1 = _E1;
			E2 = _E2;
			config = new Config();
			CombatantList = new List<Person>
				{
					{ F1},
					{ F2},
					{ E1},
					{ E2},
				};
			drawBattle = new DrawBattle(sb, sf);	
			}

		/// <summary>
		/// Returns true if the attack will hit, false if not.
		/// </summary>
		/// <param name="accuracy"></param>
		/// <returns></returns>
		public bool HitCheck(int accuracy)
		{
			// Creates a Random number between 0 and 100
			Random RNG = new Random();
			int numgen = RNG.Next(0, 101);

			if (numgen <= accuracy)
				return true;
			else return false;
		}

		/// <summary>
		/// Finds the arrays of the moves input, and saves them to a dictionary with 
		/// Key == Person, Value== array
		/// </summary>
		/// <param name="F1at"></param>
		/// <param name="F2at"></param>
		/// <param name="E1at"></param>
		/// <param name="E2at"></param>
		public void FindArrays(string F1at, string F2at, string E1at, string E2at)
		{

			// Collects arrays of needed stats. Attack, Defense, Accuracy (ADA)
			F1ADAarray = Reader.CollectMoveStats(F1at);
			F2ADAarray = Reader.CollectMoveStats(F2at);
			E1ADAarray = Reader.CollectMoveStats(E1at);
			E2ADAarray = Reader.CollectMoveStats(E2at);

			// Creates a list holding all arrays.
			ArrayList = new List<int[]>
			{
				{F1ADAarray },
				{F2ADAarray },
				{E1ADAarray },
				{E2ADAarray },
			};

		}


		/// <summary>
		/// Sets damage, defense, and accuracy to the respective person.
		/// </summary>
		/// <param name="combatantList">List of all actors in battle. Order matters.</param>
		/// <param name="arrays">List of the move arrays. Order matters.</param>
		/// <returns></returns>
		public void FinalArraySet(List<Person> combatantList, List<int[]> arrays)
		{
			int i = 0;
			foreach (Person combatant in combatantList)
				{
					
						combatant.damage = config.dubconvert(arrays[i][0], combatant.level, combatant.kingdom.damagemod);
						combatant.defense = config.dubconvert(arrays[i][1], combatant.level, combatant.kingdom.defencemod);
						combatant.accuracy = (config.dubconvert(arrays[i][2], combatant.level, combatant.kingdom.accuracymod))/10;
				i++;
					
				}
		}

		/// <summary>
		/// Returns a stack of all Combatants. Fastest is at the top of the stack.
		/// </summary>
		/// <returns></returns>
		public Stack<Person> ComOrder()
		{
			
			Stack<Person> export = new Stack<Person>();
			//Finds each speed
			int F1Speed = config.dubconvert(F1.speed, F1.level, F1.kingdom.speedmod, F1.spec.speedmod);
			int F2Speed = config.dubconvert(F2.speed, F2.level, F2.kingdom.speedmod, F2.spec.speedmod);
			int E1Speed = config.dubconvert(E1.speed, E1.level, E1.kingdom.speedmod, E1.spec.speedmod);
			int E2Speed = config.dubconvert(E2.speed, E2.level, E2.kingdom.speedmod, E2.spec.speedmod);

			// Add each speed number to a dictionary as a key with the value the person
			Dictionary<int, Person> speedFind = new Dictionary<int, Person>();

			speedFind.Add(F1Speed, F1);
			speedFind.Add(F2Speed, F2);
			speedFind.Add(E1Speed, E1);
			speedFind.Add(E2Speed, E2);
			

			//Acquire keys and sort them.
			var list = speedFind.Keys.ToList();
			list.Sort();

			// Loop through keys and save values to array
			foreach (var key in list)
			{
				export.Push(speedFind[key]);
				
			}
			return export;
		}

		/// <summary>
		/// Takes the attacker and applies damage to defender.
		/// </summary>
		/// <param name="attacker"></param>
		/// <param name="defender"></param>
		public void PreformAttack(Person attacker, Person defender)
		{
			
			config.BatInfFull += (attacker.name + " uses " + attacker.attackName + " on " + defender.name + ". \n");

			// Checks to see if move hits.

			if (HitCheck(attacker.accuracy))
			{
				// Finds the damage the defender might take
				int finalDamage = attacker.damage - defender.defense;

				// If the dodge is active on the defender, the attack misses, and damage is not applied.
				if (defender.evasiveDodgeCheck == true)
				{
					// dodged
					config.BatInfFull += ("But " + defender.name + " dodged the attack!\n");
					// dodge check resets
					defender.evasiveDodgeCheck = false;
				}
				else
				{
					config.BatInfFull += ("And it hits!\n");
					// makes sure damage isn't added to health if defense outweighs damage.
					if (finalDamage > 0)
						defender.currentHealth = defender.currentHealth - finalDamage;
					else
					{
						config.BatInfFull += ("But " + defender.name + "'s defence is too strong.\n");
					}

					// If the move hits, and the player is evasive, Their dodge activates.
					if (finalDamage > 0 && attacker.spec.isEvas == true)
					{
						config.BatInfFull += (attacker.name + "'s dodge activates!\n");
						attacker.evasiveDodgeCheck = true;
					}
				}
			}
			else
			{
				config.BatInfFull += ("But they missed\n");
			}

		}



		/// <summary>
		/// Runs all necessary methods to ensure a smooth battle
		/// </summary>
		/// <param name="move1">User selected move 1</param>
		/// <param name="move2">Use selected move 2</param>
		/// <param name="move3">Random move3</param>
		/// <param name="move4">Random move4</param>
		/// <param name="Targets">List of user and random targets, in order </param>
		public string RunBattle(string move1, string move2, string move3, string move4)
		{
			FindArrays(move1, move2, move3, move4);
			FinalArraySet(CombatantList, ArrayList);
			config.BatInfFull = "";
			foreach (Person attacker in ComOrder())
			{
				if (attacker.checkAlive() == true)
				{
					PreformAttack(attacker, attacker.target);
					if (attacker.target.checkAlive() == false)
					{
						CombatantList.Remove(attacker.target);
					}
				}
			}
			return config.BatInfFull;
		}
	}
}
