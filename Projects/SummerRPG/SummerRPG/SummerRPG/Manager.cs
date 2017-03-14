using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SummerRPG
{
	public class Manager
	{
		

		public Rectangle BattleMenuBack { get; set; }
		public Rectangle TextDisplayBack { get; set; }
		public Rectangle FullScreen { get; set; }


		public Rectangle BattleButton1 { get; set; }
		public Rectangle BattleButton2 { get; set; }
		public Rectangle BattleButton3 { get; set; }
		public Rectangle BattleButton4 { get; set; }

		public Rectangle Com1HP { get; set; }
		public Rectangle Com2HP { get; set; }
		public Rectangle Com3HP { get; set; }
		public Rectangle Com4HP { get; set; }

		public Rectangle Com1HPStat { get; set; }
		public Rectangle Com2HPStat { get; set; }
		public Rectangle Com3HPStat { get; set; }
		public Rectangle Com4HPStat { get; set; }

		public Rectangle Com1Loc { get; set; }
		public Rectangle Com2Loc { get; set; }
		public Rectangle Com3Loc { get; set; }
		public Rectangle Com4Loc { get; set; }

		public Button Move1Select { get; set; }
		public Button Move2Select { get; set; }
		public Button Move3Select { get; set; }
		public Button Move4Select { get; set; }

		public Button Back { get; set; }

		public Button Screen { get; set; }

		public void Intl()
		{
			// Window size is 1300 x 650
			TextDisplayBack = new Rectangle(427, 426, 427, 30);
			BattleMenuBack = new Rectangle(10, 450, 1280, 190);

			FullScreen = new Rectangle(0, 0, 1300, 650);

			// Home locations for the 4 select buttons
			BattleButton1 = new Rectangle(130, 460, 480, 75);
			BattleButton2 = new Rectangle(700, 460, 480, 75);
			BattleButton3 = new Rectangle(50, 550, 560, 75);
			BattleButton4 = new Rectangle(700, 550, 560, 75);

			// Buttons on MoveSelect screen.
			Move1Select = new Button(BattleButton1, "Chop");
			Move2Select = new Button(BattleButton2, "Two Handed Swing");
			Move3Select = new Button(BattleButton3, "Forsee");
			Move4Select = new Button(BattleButton4, "Divine Intervention");

			// Buttons on Target Select screen.
			Back = new Button(BattleButton4, "Back");

			Screen = new Button(FullScreen, "");
			
			// Combat zone is 0, 0 to 1280, 450
			Com1Loc = new Rectangle(295, 56, 151, 223);
			Com2Loc = new Rectangle(835, 56, 151, 223);
			Com3Loc = new Rectangle(245, 190, 151, 223);
			Com4Loc = new Rectangle(935, 190, 151, 223);


			Com1HP = new Rectangle(Com1Loc.X + 58, Com1Loc.Y - 22, 104, 20);
			Com2HP = new Rectangle(Com2Loc.X + 58, Com2Loc.Y - 22, 104, 20);
			Com3HP = new Rectangle(Com3Loc.X - 58, Com3Loc.Y - 22, 104, 20);
			Com4HP = new Rectangle(Com4Loc.X + 58, Com4Loc.Y - 22, 104, 20);

			// Info box to describe moves
			

		}

		public Rectangle fullTexture(Texture2D texture)
		{
			return new Rectangle(0, 0, texture.Width, texture.Height);	
		}

		public Rectangle InfoBox(int ViewPortWidth, int ViewPortHeight)
		{
			var mousePos = Mouse.GetState();
			var boxWidth = 450;
			var boxHeight = 175;
			Rectangle export = new Rectangle(
				mousePos.X + boxWidth < ViewPortWidth ? mousePos.X : ViewPortWidth - boxWidth,
				mousePos.Y + boxHeight < ViewPortHeight ? mousePos.Y : ViewPortHeight - boxHeight, 
				boxWidth, boxHeight);

			return export;
		}

	}
}
