using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;



namespace AddMove
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		string finalKingdom;
		string finalSpec;
		Grid AllTexts = new Grid();
		bool kingdomChecked = false;
		bool specChecked = false;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Kingdom_Checked(object sender, RoutedEventArgs e)
		{
			// ... Get RadioButton reference.
			var button = sender as RadioButton;

			// Save Value
			finalKingdom = button.Content.ToString();

			kingdomChecked = true;
		}

		private void Spec_Checked(object sender, RoutedEventArgs e)
		{
			// ... Get RadioButton reference.
			var button = sender as RadioButton;

			// Save Value
			 finalSpec = button.Content.ToString();

			specChecked = true;
		}





		// Will take all data from above boxes and buttons, open a Streamwriter, and print it to the master list
		private void Save_Click(object sender, RoutedEventArgs e)
		{

			string line;
			string KingdomLoc = finalKingdom;
			string SpecLoc = finalSpec;
			string FullMoveExport =
				Name.Text + ":" + "\n" +
				"Damage: " + Damage.Text + "\n" +
				"Defense: " + Defense.Text + "\n" +
				"Accuracy: " + Accuracy.Text + "\n" +
				"Effect: " + Effect.Text + "\n" +
				"Description: " + Description.Text + "\n" +
				"end" + Name.Text;

			// Prints an error message if there are blank spaces in the forum
			if (kingdomChecked == false || specChecked == false || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Damage.Text)
				|| string.IsNullOrWhiteSpace(Defense.Text) || string.IsNullOrWhiteSpace(Accuracy.Text) || string.IsNullOrWhiteSpace(Effect.Text)
				|| string.IsNullOrWhiteSpace(Description.Text))
			{
				MessageBox.Show("Please make sure there is data in each selection");
			}

			else
			{

				try
				{
					StreamReader file = new StreamReader(@"./..\..\..\..\SummerRPG/SummerRPG/Content/MasterMoveList.txt");

					// lists containing text files before and after the move to be inserted
					List<string> before = new List<string>();
					List<string> after = new List<string>();

					// Searches for Kingdom name and stops, adding each name to a save as it searches


					do
					{
						before.Add(line = file.ReadLine());
					} while (line != (KingdomLoc.ToUpper()));

					// Adds the Kingdom name to he list
					// before.Add(file.ReadLine());

					// Searches for the specilization name and stops, adding each name to the list as it searches
					do
					{
						before.Add(line = file.ReadLine());
					} while (line != (SpecLoc.ToUpper()));

					// Adds the Specilization name to the list
					// before.Add(file.ReadLine());

					// Adds the rest of the txt to a different file
					while ((line = file.ReadLine()) != null)
					{
						after.Add(line);
					}

					file.Close();

					using (StreamWriter newFile = new StreamWriter(@"./..\..\..\..\SummerRPG/SummerRPG/Content/MasterMoveList.txt"))
					{
						foreach (string s in before)
						{
							newFile.WriteLine(s);
						}

						string finalDescript = "Description: " + Description.Text.Trim();
						int findLoc = 65;
						bool isSplit = false;

						newFile.WriteLine();
						newFile.WriteLine(Name.Text.Trim() + ":");
						newFile.WriteLine("Damage: " + Damage.Text.Trim());
						newFile.WriteLine("Defense: " + Defense.Text.Trim());
						newFile.WriteLine("Accuracy: " + Accuracy.Text.Trim());
						newFile.WriteLine("Effect: " + Effect.Text.Trim());
						if (finalDescript.Length >= 65)
						{
							while (!isSplit)
							{
								if (finalDescript[findLoc] == ' ')
								{

									var descStart = finalDescript.Substring(0, findLoc);
									var desEnd = finalDescript.Substring(findLoc + 1, finalDescript.Length - 1);
									newFile.WriteLine(descStart);
									newFile.WriteLine(desEnd);
									isSplit = true;
								}
								else
								{
									findLoc --;
								}
							}
						}
							
						else
						{
							newFile.WriteLine(finalDescript);
						}
						newFile.WriteLine("end" + Name.Text.Trim());
						newFile.WriteLine();

						foreach (string s in after)
						{
							newFile.WriteLine(s);
						}
						newFile.Close();
					}


					MessageBox.Show("Your move was added to the list");


				}
				catch (Exception Error)
				{
					MessageBox.Show("Could not find the file. \n" + Error.Message);
				}
			}
		}
	}
}
