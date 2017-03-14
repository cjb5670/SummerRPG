using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SummerRPG
{
	/// <summary>
	/// Has all data needed for drawing the battle
	/// </summary>
	class DrawBattle
	{
        public SpriteBatch spriteBatch;
		public SpriteFont font;
		private Manager manager;
		private Color HPStatColor;
		private MoveListHolder moveList;
		private Timer timer;
		private int percHealth;
		private double doublePerc;
		private int framecount = 1;
        public DrawBattle(SpriteBatch _spriteBatch, SpriteFont _font)
        {
            spriteBatch = _spriteBatch;
			font = _font;
			manager = new Manager();
			manager.Intl();
			moveList = new MoveListHolder();
			timer = new Timer(15);
        }

        public void DrawButtonWithText(Texture2D texture, Rectangle sizeAndPosition, Color buttonColor, 
			string text, int xOffset, int yOffset, Color fontColor)
        {
			spriteBatch.Draw(texture, sizeAndPosition, buttonColor);
			spriteBatch.DrawString(font, text, new Vector2((sizeAndPosition.X + xOffset), (sizeAndPosition.Y + yOffset)), fontColor);
        }

		

		public void DrawFlippedButtonWithText(Texture2D texture, Rectangle sizeAndPosition, Rectangle textureSource, Color buttonColor,
			string text, int xOffset, int yOffset, Color fontColor)
		{
			spriteBatch.Draw(texture, sizeAndPosition, textureSource, buttonColor, 
			0.0f, new Vector2(0,0), SpriteEffects.FlipHorizontally, 0.0F);
			spriteBatch.DrawString(font, text, new Vector2((sizeAndPosition.X + xOffset), (sizeAndPosition.Y + yOffset)), fontColor);
		}

		/// <summary>
		/// Draws health bar with logic.
		/// </summary>
		/// <param name="backTexture"></param>
		/// <param name="loc">NEEDS TO HAVE A WIDTH OF 104</param>
		/// <param name="comb"></param>
		public void DrawHealth(Texture2D backTexture, Rectangle loc, Person comb)
		{
			if (loc.Width != 104)
				throw new Exception("Position Rectangle does not have a width of 104.");
			// Draws Back
			spriteBatch.Draw(backTexture, loc, Color.Black);
			// Finds Stat as percent
			doublePerc = comb.currentHealth*100/comb.health;
			if (doublePerc >= 0)
			{
				percHealth = (int) doublePerc;
			}
			else
				percHealth = 0;
			// picks color of stat
			if (comb.currentHealth >= comb.health/2)
				HPStatColor = Color.Green;
			else if (comb.currentHealth >= comb.health/3 && comb.currentHealth < comb.health/2)
				HPStatColor = Color.Yellow;
			else if (comb.currentHealth >= 0 && comb.currentHealth < comb.health/3)
				HPStatColor = Color.Red;

			// Draws stat
			spriteBatch.Draw(backTexture, new Rectangle(loc.X + 2, loc.Y + 2, percHealth - 4, loc.Height - 4), HPStatColor);

			// Draws Text on top of stat
			spriteBatch.DrawString(font, comb.name + " " + comb.purpose, new Vector2(loc.X, loc.Y - 14), Color.Black);

		}


		// Window size is 1300 x 650
		public void DrawInfoBox(Texture2D boxBack, string moveName)
		{
			spriteBatch.Draw(boxBack, manager.InfoBox(1300, 650), Color.White);
			var i = 1;
			foreach (var moveline in moveList.MoveInfo(moveName))
			{
				spriteBatch.DrawString(font, moveline, 
					new Vector2(manager.InfoBox(1300, 650).X + 10, 
					manager.InfoBox(1300, 650).Y + i * 20), Color.Black);
				i++;
			}
			
		}
		// Needs to return each letter in the string. and then call itslef again.
		public void DrawInfo(string info)
		{
			var xOffset = manager.TextDisplayBack.X / 3 - 10;
			var yOffset = 4;
			//string output = "";
			
			//foreach (var letter in info)
			//{
				
				//output = output + letter;
				spriteBatch.DrawString(font, info, new Vector2(
						manager.TextDisplayBack.X + xOffset,
						manager.TextDisplayBack.Y + yOffset), Color.Black);
			//}	
		}
	}
}
