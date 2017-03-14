using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Permissions;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SummerRPG
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Technical
        #region
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		SpriteFont font;
		public MouseState ms { get; set; }
		public MouseState msPrev { get; set; }
		Manager Manager { get; set; }
		DrawBattle drawBattle { get; set; }
		enum GameState
		{
			SelectMove, SelectTarget, PreformAttack, Win, Lose
		}
		GameState state;
		#endregion

		// Kingdoms and Spec and combatants
		#region
		Kingdom Martial;
		Kingdom Mage;
		Kingdom Divine;
		Kingdom TestKingdom;
		Specilization Offensive;
		Specilization Defensive;
		Specilization Evasive;
		Specilization TestSpec;
		Friendly player;
	    Friendly ally;
		Enemy guard1;
		Enemy guard2;
	    private List<Friendly> Party;
	    private int parCou;
			#endregion

		// Images

		// Battle screen
		Texture2D MenuBackground { get; set; }
		Texture2D BattleButtonBackground { get; set; }
		Texture2D HP_Bar { get; set; }
	    private Texture2D GuardTex { get; set; }
	    private Texture2D PlayerTex { get; set; }
	    private Texture2D AllyTex { get; set; }
		private Texture2D InfoBack { get; set; }
	    private Button Enemy1Select { get; set; }
	    private Button Enemy2Select { get; set; }


		// Sounds
	    private Song BattleTheme { get; set; }
		private SoundEffect Hover { get; set; }
		private SoundEffect Click { get; set; }
	    private SoundEffectInstance hover;
		private SoundEffectInstance click;
		bool hoverCheck;

		// Logic needed for Gauntlet Mode
		private int currentRound { get; set; }

		// Battle Data
	    private string battleData { get; set; }
	    private int count { get; set; }
	    private int grow { get; set; }

		public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Adjusts window dimensions
            graphics.PreferredBackBufferWidth = 1300;  
            graphics.PreferredBackBufferHeight = 650;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

			// Logic for Gauntlet
	        currentRound = 1;
			
				// Kingdoms
			#region
			Martial = new Kingdom(0.8, 1.2, 1.0, 0.8);
			Mage = new Kingdom(1.2, 1.0, 0.8, 1.0);
			Divine = new Kingdom(1.0, 0.8, 1.2, 1.2);
			TestKingdom = new Kingdom(1, 1, 1, 1);
			#endregion

			// Specilizations
			#region
			Offensive = new Specilization(0.8, 1.0, false);
			Defensive = new Specilization(1.2, 0.6, false);
			Evasive = new Specilization(0.5, 1.3, true);
			TestSpec = new Specilization(1, 1, false);
			#endregion

			// Combatants
			player = new Friendly("Player", "(Special Unit)", 5, Divine, Evasive);
			ally = new Friendly("Ally", "(Attack Unit)", 5, Divine, Offensive);
			guard1 = new Enemy("Guard 1", "(Attack Unit)", currentRound, Mage, Offensive);
			guard2 = new Enemy("Guard 2", "(Tank Unit)", currentRound, Martial, Defensive);

			Party = new List<Friendly>
			{
				player,
				ally
			};
			parCou = 0;

			// Rectangles for buttons and drawings
			Manager = new Manager();
			Manager.Intl();

            // Shows mouse
            IsMouseVisible = true;

			// TODO: FIGURE OUT HOW TO GET BUTTONS IN MANAGER
			Enemy1Select = new Button(Manager.BattleButton1, guard1, guard1.name);
			Enemy2Select = new Button(Manager.BattleButton3, guard2, guard2.name);
			


			// Handles enemy "AI"
			guard1.AddMove(new List<string>
			{ "Chop", "Ready Guard", "Faint Attack", "Helm Smash", "Shield Charge"});
			guard2.AddMove(new List<string>
			{ "Chop", "Ready Guard", "Faint Attack", "Helm Smash", "Shield Charge"});
			guard1.LoadAI(player, ally);
			guard2.LoadAI(player, ally);

			// Begins game on the select move screen
			state = GameState.SelectMove;

			// data needed for showing 
	        count = 0;
	        grow = 0;

			// Sound effect data
			hoverCheck = false;

			// THIS MUST BE LAST.
			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
			font = Content.Load<SpriteFont>("font");
			// Special Drawing
			drawBattle = new DrawBattle(spriteBatch, font);

			// Menu objects
			MenuBackground = Content.Load<Texture2D>("MenuBase.png");
			BattleButtonBackground = Content.Load<Texture2D>("MoveSelector.png");
			HP_Bar = Content.Load<Texture2D>("HP_Bar.png");
	        InfoBack = Content.Load<Texture2D>("InfoBox.png");
			// Characters
	        GuardTex = Content.Load<Texture2D>("MartialGrunt.png");
	        PlayerTex = Content.Load<Texture2D>("Player.png");
	        AllyTex = Content.Load<Texture2D>("Ally.png");

	        // Songs
	        BattleTheme = Content.Load<Song>("battleground_final");
	        Hover = Content.Load<SoundEffect>("hover2");
	        Click = Content.Load<SoundEffect>("click4");

			// Logic for beginning music (needs to be after music is loaded, but only once)
			//MediaPlayer.Play(BattleTheme);
			//MediaPlayer.IsRepeating = true;
			//MediaPlayer.Volume = 0.7F;
			hover = Hover.CreateInstance();
			click = Click.CreateInstance();
		}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
			ms = new MouseState();

			
			// Makes sure game is only running the logic it needs to.
				switch (state)
	        {
			        
				    case GameState.SelectMove:
					// Has button logic for finding correct moves and saving them each person's Attackname attribute

					// Attempting to catch game-breaking bug with parCou.
			        try
			        {
				        // Removes party member from pool if they die.
				        if (!Party[parCou].checkAlive())
				        {
					        Party.Remove(Party[parCou]);

					        // if Party [1] dies, remove party[1] from the list, and move on with Party[2](Now Party[1])
					        // So do nothing.

					        // if Party [3] dies, remove party[3] from the list, and go to the next state.
					        //  0 base      1 base
					        if (parCou > Party.Count + 1)
					        {
						        state = GameState.PreformAttack;
						        break;
					        }

				        }
			        }
			        catch (Exception e)
			        {
				        throw new Exception("ParCou error. Inform Developer of exact steps to replicate" +
							e.Message);
			        }

					// Has logic for clicking on buttons.
					#region
					if (Manager.Move1Select.ButtonClicked())
				        {
					        Party[parCou].attackName = Manager.Move1Select.moveName;
					        state = GameState.SelectTarget;
					        Click.Play();
				        }
			        
				        if (Manager.Move2Select.ButtonClicked())
				        {
					        Party[parCou].attackName = Manager.Move2Select.moveName;
					        state = GameState.SelectTarget;
							Click.Play();
						}
				        if (Manager.Move3Select.ButtonClicked())
				        {
					        Party[parCou].attackName = Manager.Move3Select.moveName;
					        state = GameState.SelectTarget;
						Click.Play();
					}
				        if (Manager.Move4Select.ButtonClicked())
				        {
					        Party[parCou].attackName = Manager.Move4Select.moveName;
					        state = GameState.SelectTarget;
						Click.Play();
					}
					#endregion


					// Logic purly for sounds.
					#region
					// If the mouse has entered a button[enterbutton], the sound is not playing[hover.state], and the sound has
					// only played once [hovercheck]
					if ((Manager.Move1Select.enterButton() || Manager.Move2Select.enterButton() ||
			             Manager.Move3Select.enterButton() || Manager.Move4Select.enterButton()) 
						 && hover.State != SoundState.Playing && !hoverCheck)
			        {
				        hover.Play();
				        hoverCheck = true;
			        }

					// Makes the hoverCheck false when the mouse leaves the button area.
			        if (!(Manager.Move1Select.enterButton() || Manager.Move2Select.enterButton() ||
			              Manager.Move3Select.enterButton() || Manager.Move4Select.enterButton()))
			        {
				        hoverCheck = false;
			        }
					#endregion

					break;

				        case GameState.SelectTarget:

					// Logic for selecting target and going back to previous screen.
					#region
					if (Enemy1Select.ButtonClicked() && Enemy1Select.target.checkAlive())
				    {
					    Party[parCou].target = Enemy1Select.target;
					    if (parCou == (Party.Count - 1))
					    {
						    state = GameState.PreformAttack;
						    parCou = 0;
						
						}

					    else
					    {
						    parCou ++;
						    state = GameState.SelectMove;
							
						}
						Click.Play();
					}
			        

			        
			        
				    if (Enemy2Select.ButtonClicked() && Enemy2Select.target.checkAlive())
				    {
					    Party[parCou].target = Enemy2Select.target;
					    if (parCou == (Party.Count - 1))
					    {
						    state = GameState.PreformAttack;
						    parCou = 0;
						}
					    else
					    {
						    state = GameState.SelectMove;
						    parCou++;
					    }
						Click.Play();
					}
			        

			        if (Manager.Back.ButtonClicked())
				        {
					        state = GameState.SelectMove;
						Click.Play();
					}
				#endregion

					// Logic stricly for sounds
					if ((Manager.Back.enterButton() || Enemy1Select.enterButton() || Enemy2Select.enterButton())
						&& hover.State != SoundState.Playing && !hoverCheck)
			        {
				        hover.Play();
				        hoverCheck = true;
			        }

			        if (!(Manager.Back.enterButton() || Enemy1Select.enterButton() || Enemy2Select.enterButton()))
				        hoverCheck = false;


					

			        break;
			        
		        case GameState.PreformAttack:

					// Begins the turn
					var test = new Battle(player, ally, guard1, guard2, spriteBatch, font);

					// Running the turn returns a string. If the string is null, it runs the logic.
					if (string.IsNullOrEmpty(battleData))
					battleData = test.RunBattle(player.attackName, ally.attackName, guard1.attackName, guard2.attackName);

					// If the screen is clicked during this state, the timer times out.
					if (Manager.Screen.ButtonClicked())
						count = 600;

					// increments timer if there is time to be counted.
					if (count < 600)
				        count ++;
					
					// If the timer is done or the screen is clicked.
			        else
			        {
				        // Run each stage of the battle until the user clicks.
				        // Lose if your team dies
				        if (!player.checkAlive() && !ally.checkAlive())
					        state = GameState.Lose;

				        // win if the other team dies
				        else if (!guard1.checkAlive() && !guard2.checkAlive())
				        {
							//Reruns all initialize data to reset game(creates entirely new combatant group).
							// Also where the round/level increment.
							#region
							// Increments current round
							currentRound++;

							// Resets all combatants, and levels up guards.
					        player = new Friendly("Player", "(Special Unit)", 5, Divine, Evasive);
					        ally = new Friendly("Ally", "(Attack Unit)", 5, Divine, Offensive);
					        guard1 = new Enemy("Guard 1", "(Attack Unit)", currentRound, Mage, Offensive);
					        guard2 = new Enemy("Guard 2", "(Tank Unit)", currentRound, Martial, Defensive);

							// Brings everyone back to life.
					        Party = new List<Friendly>
					        {
						        player,
						        ally
					        };

							// Begins counter at 0
					        parCou = 0;

					        // TODO: FIGURE OUT HOW TO GET BUTTONS IN MANAGER
							// The problem is that the enemies aren't established in the manager class.
							// The manager class is only for Rectangles and Buttons. Changable logic
							// (such as players, kingdoms, and movelists) stay in the Game1 class for now.
							// Units can't be made public because it messes with the game in scary ways.
					        Enemy1Select = new Button(Manager.BattleButton1, guard1, guard1.name);
					        Enemy2Select = new Button(Manager.BattleButton3, guard2, guard2.name);
					        


							// Handles enemy "AI"
							guard1.AddMove(new List<string>
							{ "Chop", "Ready Guard", "Faint Attack", "Helm Smash", "Shield Charge"});
							guard2.AddMove(new List<string>
							{ "Chop", "Ready Guard", "Faint Attack", "Helm Smash", "Shield Charge"});
							guard1.LoadAI(player, ally);
							guard2.LoadAI(player, ally);


							// Resets Data Box logic
							battleData = "";
					        count = 0;
					        grow = 0;
							#endregion

							state = GameState.SelectMove;
				        }

						
				        // if someone on both teams is alive, goes back to select move.
				        else
				        {
							// text display box is reset
							battleData = "";
							count = 0;
							grow = 0;
							// State is reverted.
							state = GameState.SelectMove;
				        }
			        }
			        break;				
	        }
			// Saves previous ms state.
			msPrev = ms;
			base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Window size is 1300 x 650
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

			// Holds data for things to be drawn across all battle states (combatant textures, health bars,
			// and the trapazoid back)
			#region
			if (state == GameState.SelectMove || state == GameState.SelectTarget || state == GameState.PreformAttack)
	        {	
				// Gauntlet Information
				spriteBatch.DrawString(font, "Round: " + currentRound, new Vector2(5, 5), Color.Black);

				// Menu Back and game descriptor box
				spriteBatch.Draw(InfoBack, Manager.TextDisplayBack, Color.White);
				spriteBatch.Draw(MenuBackground, Manager.BattleMenuBack, Color.White);
		        

				// person Drawing
				if (guard1.checkAlive())
		        spriteBatch.Draw(GuardTex, Manager.Com1Loc, Color.White);
				if (ally.checkAlive())
				spriteBatch.Draw(AllyTex, Manager.Com2Loc, Color.White);
				if (guard2.checkAlive())
				spriteBatch.Draw(GuardTex, Manager.Com3Loc, Color.White);
				if (player.checkAlive())
				spriteBatch.Draw(PlayerTex, Manager.Com4Loc, Color.White);
				
				// HP Drawing
				drawBattle.DrawHealth(HP_Bar, Manager.Com1HP, guard1);
				drawBattle.DrawHealth(HP_Bar, Manager.Com2HP, ally);
				drawBattle.DrawHealth(HP_Bar, Manager.Com3HP, guard2);
				drawBattle.DrawHealth(HP_Bar, Manager.Com4HP, player);
			}
			#endregion

			// Holds data for state specific drawings, like buttons and their color logic.
			switch (state)
	        {
		        case GameState.SelectMove:

					// Select move buttons, button info boxes, and feedback box 
					#region
					// Top left button
					drawBattle.DrawButtonWithText(BattleButtonBackground, Manager.BattleButton1,
				        !Manager.Move1Select.enterButton() ? Color.White : Color.Gray ,
				        Manager.Move1Select.moveName, 75, 25, Color.Black);
					// Top right button
			        drawBattle.DrawFlippedButtonWithText(BattleButtonBackground, Manager.BattleButton2,
				        Manager.fullTexture(BattleButtonBackground), !Manager.Move2Select.enterButton() ? Color.White : Color.Gray,
				        Manager.Move2Select.moveName, 75, 25, Color.Black);
					// Bottom left button
			        drawBattle.DrawButtonWithText(BattleButtonBackground, Manager.BattleButton3,
				        !Manager.Move3Select.enterButton() ? Color.White : Color.Gray,
				        Manager.Move3Select.moveName, 75, 25, Color.Black);
					// Bottom right button
			        drawBattle.DrawFlippedButtonWithText(BattleButtonBackground, Manager.BattleButton4,
				        Manager.fullTexture(BattleButtonBackground), !Manager.Move4Select.enterButton() ? Color.White : Color.Gray,
				        Manager.Move4Select.moveName, 75, 25, Color.Black);

					// Draws feedback box and data (Who is selecting their move)
					drawBattle.DrawInfo("Select " + Party[parCou].name + "'s move");

					// Draws move info boxes

					// Top left move
					if (Manager.Move1Select.enterButton())
						drawBattle.DrawInfoBox(InfoBack, Manager.Move1Select.moveName);
					// Top right move
					if (Manager.Move2Select.enterButton())
						drawBattle.DrawInfoBox(InfoBack, Manager.Move2Select.moveName);
					// Bottom left move
					if (Manager.Move3Select.enterButton())
						drawBattle.DrawInfoBox(InfoBack, Manager.Move3Select.moveName);
					// Bottom right move
					if (Manager.Move4Select.enterButton())
						drawBattle.DrawInfoBox(InfoBack, Manager.Move4Select.moveName);
					break;
				#endregion

				case GameState.SelectTarget:

					// Select target buttons and feedback box
					#region
					// Top left box
					if (Enemy1Select.target.checkAlive())
			        drawBattle.DrawButtonWithText(BattleButtonBackground, Manager.BattleButton1,
				        !Enemy1Select.enterButton() ? Color.White : Color.Gray,
				        Enemy1Select.moveName, 75, 25, Color.Black);
					// Bottom left box
					if (Enemy2Select.target.checkAlive())
			        drawBattle.DrawButtonWithText(BattleButtonBackground, Manager.BattleButton3,
				        !Enemy2Select.enterButton() ? Color.White : Color.Gray,
				        Enemy2Select.moveName, 75, 25, Color.Black);
					// Back button
			        drawBattle.DrawFlippedButtonWithText(BattleButtonBackground, Manager.BattleButton4,
				        Manager.fullTexture(BattleButtonBackground), !Manager.Back.enterButton() ? Color.White : Color.Gray,
				        Manager.Back.moveName, 75, 25, Color.Black);
					// feedback box
					drawBattle.DrawInfo("Select " + Party[parCou].name + "'s target");
					break;
                    #endregion

				case GameState.PreformAttack:

					// Holds for growing the feedback box, and 'animates' it.
					#region
					// The rate at which the feedback box grows
					const int additive = 2;
			        Rectangle moveBox;

					// if not finished growing
			        if (grow <= 350)
				        grow += additive;
					// begin timer doubles in countdown.
					else
						count ++;			
					// 'animates' feedback box.
					spriteBatch.Draw(InfoBack, moveBox = new Rectangle(
						Manager.TextDisplayBack.X, Manager.TextDisplayBack.Y - grow,
						Manager.TextDisplayBack.Width, Manager.TextDisplayBack.Height + grow), Color.White);
					// Makes sure there is data, then runs the basis of a character by character input into the box.
					// TODO: Fully implement character-by-character display of combat data. 
			        if (!string.IsNullOrEmpty(battleData))
			        {
				        var xOffset = moveBox.X/3;
				        var yOffset = 4;
				        //string output = "";

				        //foreach (var letter in info)
				        //{

				        //output = output + letter;
				        spriteBatch.DrawString(font, battleData, new Vector2(
					        moveBox.X + xOffset,
					        moveBox.Y + yOffset), Color.Black);
			        }
					// Makes sure the menu background is on top of the feedback box, making it look like it's sliding up
					// from behind it.
			        spriteBatch.Draw(MenuBackground, Manager.BattleMenuBack, Color.White);
					#endregion

					break;
				case GameState.Win:
					// Placeholder win state (Game currently unwinable by design)
					spriteBatch.DrawString(font, "You Win!",
						new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.Black);
					break;

				case GameState.Lose:
					// Tells the user how many rounds they survived.
					spriteBatch.DrawString(font, "You made it " + currentRound + " rounds.",
						new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.Black);
					break;
			}
			

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
