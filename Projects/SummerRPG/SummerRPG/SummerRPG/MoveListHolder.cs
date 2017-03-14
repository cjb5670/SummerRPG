using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// C:\Users\Cameron\Documents\Visual_Studio_2015\Projects\SummerRPG\SummerRPG\Texts
namespace SummerRPG
{
	/// <summary>
	/// Holds all lists of moves (via file Read) for each class and specilization.
	/// </summary>
	class MoveListHolder
	{
		/// <summary>
		/// Saves the stats of the move in an array.
		/// [0] damage, [1] defense, [2] accuracy
		/// </summary>
		/// <param name="moveName"></param>
		/// <returns></returns>
		public int[] CollectMoveStats(string moveName)
		{
			int[] stats = new int[3];
			string line;
			int[] errorArray = { 0,0,0 };

			
				StreamReader file = new StreamReader(@"./..\..\..\..\Content/MasterMoveList.txt");

			// loops through the master move list until it finds the name Movename
			while ((line = file.ReadLine()) != ("end" + moveName))
			{
				if (line.Contains(moveName + ":"))
				{
					// locates damage
					string predamage = file.ReadLine().Trim().Replace("Damage:", "");
						int damage;
						// adds damage to array[0]
						int.TryParse(predamage, out damage);
						stats[0] = damage;

						// locates defense
						string predefense = file.ReadLine().Trim().Replace("Defense:", "");
						int defense;
						// adds defense to array [1]
						int.TryParse(predefense, out defense);
						stats[1] = defense;

						// locates accuracy
						string preaccuracy = file.ReadLine().Trim().Replace("Accuracy:", "");
						int accuracy;
						// adds accuracy to array
						int.TryParse(preaccuracy.Trim(), out accuracy);
						stats[2] = accuracy;
					
					file.Close();
					
						return stats;
					}
				}
			
			file.Close();
			return errorArray;
		}

		public List<string> MoveInfo(string moveName)
		{
			List<string> info = new List<string>();
			string line;
			List<string> errorArray = new List<string>()
			{	"ERROR", "ERROR", "ERROR", "ERROR", "ERROR"};


			StreamReader file = new StreamReader(@"./..\..\..\..\Content/MasterMoveList.txt");
			// loops through the master move list until it finds the name Movename
			while ((line = file.ReadLine()) != null)
				if (line.Contains(moveName + ":"))
				{
					info.Add(moveName);
					while ((line = file.ReadLine()) != "end" + moveName)
					{
						info.Add(line);
					}
				

				}
			return string.IsNullOrEmpty(info[0]) ? errorArray : info;
		}
	}

}
