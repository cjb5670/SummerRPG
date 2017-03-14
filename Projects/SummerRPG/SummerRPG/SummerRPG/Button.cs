using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace SummerRPG
{
	/// <summary>
	/// <3 <3 <3
	/// </summary>
	public class Button
	{

	
		public Rectangle ButtonPos { get; set; }
		MouseState ms;
		MouseState msPrev;
		public string moveName { get; set; }
		public Enemy target { get; set; }


		public Button(Rectangle _buttonPos, string _moveName)
		{

			ButtonPos = _buttonPos;
			moveName = _moveName;
		}

		public Button(Rectangle _buttonPos, Enemy _target, string _name)
		{
			ButtonPos = _buttonPos;
			target = _target;
			moveName = _name;
		}
		
			
		/// <summary>
		/// Returns true if mouse is inside button 
		/// </summary>
		/// <returns></returns>
		public bool enterButton()
		{
			ms = Mouse.GetState();
			if (ms.X <= (ButtonPos.X + ButtonPos.Width) &&
				ms.X >= ButtonPos.X &&
				ms.Y <= (ButtonPos.Y + ButtonPos.Height) &&
				ms.Y >= ButtonPos.Y)

			{ return true; }
			return false;
		}

		/// <summary>
		/// Returns true if button has been pressed
		/// </summary>
		/// <returns></returns>

		public bool ButtonClicked()
		{
			msPrev = ms;
			ms = Mouse.GetState();
			if (ms.X <= (ButtonPos.X + ButtonPos.Width) &&
			    ms.X >= ButtonPos.X &&
			    ms.Y <= (ButtonPos.Y + ButtonPos.Height) &&
			    ms.Y >= ButtonPos.Y &&
			    msPrev.LeftButton == ButtonState.Pressed &&
			    ms.LeftButton == ButtonState.Released)				
			{
				return true;
			}
				
			return false;
		
		}
		
		
	}
}

