using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using VoidEngine;

namespace PacKitten
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D tileTexture;
		List<Sprite.AnimationSet> tileAnimationSetList;
		List<Tiles> tileList;
		public List<string> map;

		Texture2D playerTexture;
		public Player player;
		List<Sprite.AnimationSet> playerAnimationSetList;

		Texture2D enemyTexture;
		public List<Enemy> enemies;
		List<Sprite.AnimationSet> enemyAnimationSetList;

		Texture2D areaTexture;

		public List<int> Timer = new List<int>();

		List<bool> updateScore = new List<bool>();

		public int buffActive = 0;

		Texture2D smallFoodTexture;
		List<Food> food;
		List<Sprite.AnimationSet> smallFoodAnimationSetList;

		Texture2D superFoodTexture;
		List<Superfood> superFood;
		List<Sprite.AnimationSet> superFoodAnimationSetList;

		SpriteFont segoeuimonodebug;
		Label debugLabel;
		string[] debugStrings = new string[10];

		List<Label> ScoreAddition;

		public int score;

		public KeyboardState keyboardState, previousKeyboardState;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			tileTexture = Content.Load<Texture2D>(@"images\other\tiles");
			tileAnimationSetList = new List<Sprite.AnimationSet>();
			tileAnimationSetList.Add(new Sprite.AnimationSet(".", tileTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600, false));
			tileAnimationSetList.Add(new Sprite.AnimationSet("1", tileTexture, new Point(30, 30), new Point(0, 0), new Point(30, 0), 1600, false));
			tileList = new List<Tiles>();
			map = MapGen();

			areaTexture = Content.Load<Texture2D>(@"images\gui\area");

			playerTexture = Content.Load<Texture2D>(@"images\player\player");
			playerAnimationSetList = new List<Sprite.AnimationSet>();
			playerAnimationSetList.Add(new Sprite.AnimationSet("IDLE", playerTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600, false));

			enemyTexture = Content.Load<Texture2D>(@"images\enemies\enemy");
			enemyAnimationSetList = new List<Sprite.AnimationSet>();
			enemyAnimationSetList.Add(new Sprite.AnimationSet("IDLE", enemyTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600, false));
			enemies = new List<Enemy>();

			smallFoodTexture = Content.Load<Texture2D>(@"images\pickups\pickup");
			smallFoodAnimationSetList = new List<Sprite.AnimationSet>();
			smallFoodAnimationSetList.Add(new Sprite.AnimationSet("IDLE", playerTexture, new Point(10, 10), new Point(0, 0), new Point(0, 0), 1600, false));
			food = new List<Food>();

			superFoodTexture = Content.Load<Texture2D>(@"images\pickups\superpickup");
			superFoodAnimationSetList = new List<Sprite.AnimationSet>();
			superFoodAnimationSetList.Add(new Sprite.AnimationSet("IDLE", superFoodTexture, new Point(20, 20), new Point(0, 0), new Point(0, 0), 1600, false));
			superFood = new List<Superfood>();

			segoeuimonodebug = Content.Load<SpriteFont>(@"fonts\segoeuibold");
			debugLabel = new Label(new Vector2(600, 15), segoeuimonodebug, 1.0f, Color.Black, "");
			ScoreAddition = new List<Label>();

			for (int i = 0; i < map.Count; i++)
			{
				for (int j = 0; j < map[0].Length; j++)
				{
					if (map[i][j] == 'P')
					{
						player = new Player(new Vector2(j * 30, i * 30), Color.White, playerAnimationSetList, this);
						map[i] = map[i].Substring(0, j) + '.' + map[i].Substring(j + 1);
					}

					if (map[i][j] == '.')
					{
						food.Add(new Food(new Vector2(j * 30 + 10, i * 30 + 10), Color.White, smallFoodAnimationSetList, this));
					}

					if (map[i][j] == 'S')
					{
						superFood.Add(new Superfood(new Vector2(j * 30 + 5, i * 30 + 5), Color.White, superFoodAnimationSetList, this));
						map[i] = map[i].Substring(0, j) + '.' + map[i].Substring(j + 1);
					}

					if (map[i][j] == 'E')
					{
						enemies.Add(new Enemy(new Vector2(j * 30, i * 30), Color.White, enemyAnimationSetList, this));
						map[i] = map[i].Substring(0, j) + '.' + map[i].Substring(j + 1);
					}

					tileList.Add(new Tiles(new Vector2(j * 30, i * 30), map[i][j].ToString(), Color.White, tileAnimationSetList, this));
				}
			}

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

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			UpdateScore(gameTime);
			keyboardState = Keyboard.GetState();

			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				this.Exit();
			}

			player.Update(gameTime);

			for (int i = 0; i < food.Count; i++)
			{
				if (food[i].DeleteMe)
				{
					food.RemoveAt(i);
					i--;
				}
				else
				{
					food[i].Update(gameTime);
				}
			}

			for (int i = 0; i < superFood.Count; i++)
			{
				if (superFood[i].DeleteMe)
				{
					buffActive = 3000;
					superFood.RemoveAt(i);
					i--;
				}
				else
				{
					superFood[i].Update(gameTime);
				}
			}
			if (buffActive > 0)
			{
				buffActive -= gameTime.ElapsedGameTime.Milliseconds;
				if (buffActive <= 0)
				{
					buffActive = 0;
				}
			}

			foreach (Enemy e in enemies)
			{
				e.Update(gameTime);
			}


			previousKeyboardState = keyboardState;

			// TODO: Add your update logic here

			debugLabel.text = debugStrings[0];/* + "\n" +
										debugStrings[1] + "\n" +
										debugStrings[2] + "\n" +
										debugStrings[3] + "\n" +
										debugStrings[4] + "\n" +
										debugStrings[5] + "\n" +
										debugStrings[6] + "\n" +
										debugStrings[7] + "\n" +
										debugStrings[8] + "\n" +
										debugStrings[9]);*/

			debugLabel.Update(gameTime);

			debugStrings[1] = "buffActive=" + buffActive;
			debugStrings[0] = "Score = " + score;
			debugStrings[2] = "timer=" + enemies[0].timer;
			if (Alpha.Count > 0)
			{
				debugStrings[3] = "Alpha=" + Alpha[0] + " Count=" + Alpha.Count;
			}
			if (updateScore.Count > 0)
			{
				debugStrings[4] = "updateScore=" + updateScore[0] + " Count=" + updateScore.Count;
			}
			if (Timer.Count > 0)
			{
				debugStrings[5] = "Timer=" + Timer[0] + " Count=" + Timer.Count;
			}
			if (ScoreAddition.Count > 0)
			{
				debugStrings[6] = "Color=(" + ScoreAddition[0].color.R + "," + ScoreAddition[0].color.G + "," + ScoreAddition[0].color.B + "," + ScoreAddition[0].color.A + ")";
				debugStrings[7] = "Text=" + ScoreAddition[0].text + " Count=" + ScoreAddition.Count;
				debugStrings[8] = "Y=" + ScoreAddition[0].position.Y;
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin();
			{
				foreach (Tiles t in tileList)
				{
					t.Draw(gameTime, spriteBatch);
				}

				player.Draw(gameTime, spriteBatch);

				foreach (Food f in food)
				{
					f.Draw(gameTime, spriteBatch);
				}

				foreach (Enemy e in enemies)
				{
					e.Draw(gameTime, spriteBatch);
				}

				foreach (Superfood s in superFood)
				{
					s.Draw(gameTime, spriteBatch);
				}

				spriteBatch.Draw(areaTexture, new Vector2(570, 0), Color.White);

				debugLabel.Draw(gameTime, spriteBatch);

				foreach (Label a in ScoreAddition)
				{
					a.Draw(gameTime, spriteBatch);
				}
			}
			spriteBatch.End();

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}

		protected List<string> MapGen()
		{
			List<string> tmpList = new List<string>();
			tmpList.Add("S.................S");
			tmpList.Add(".11111.1.1.1.11111.");
			tmpList.Add(".1P....1.1.1.....1.");
			tmpList.Add(".1.111.1.1.1.111.1.");
			tmpList.Add(".1.1S.........S1.1.");
			tmpList.Add(".1.1.1111.1111.1.1.");
			tmpList.Add(".....1E..E..E1.....");
			tmpList.Add(".1.1.1.1.1.1.1.1.1.");
			tmpList.Add(".1.1.1.1.1.1.1.1.1.");
			tmpList.Add(".....1E..E..E1.....");
			tmpList.Add(".1.1.1111.1111.1.1.");
			tmpList.Add(".1.1S.........S1.1.");
			tmpList.Add(".1.111.1.1.1.111.1.");
			tmpList.Add(".1.....1.1.1.....1.");
			tmpList.Add(".11111.1.1.1.11111.");
			tmpList.Add("S.................S");
			return tmpList;
		}

		public List<int> Alpha = new List<int>();

		public void AddScore(int scoreAddition)
		{
			score += scoreAddition;
			//ScoreAddition.Add(new Label(new Vector2(600, 15), segoeuimonodebug, 1.0f, new Color(0, 0, 255, 255), scoreAddition.ToString()));
			//Alpha.Add(255);
			//Timer.Add(2000);
		}

		public void UpdateScore(GameTime gameTime)
		{

			foreach (int j in Timer)
			{
				foreach (int a in Alpha)
				{
					foreach (Label i in ScoreAddition)
					{
						i.position.Y -= (float)gameTime.ElapsedGameTime.Milliseconds;
						Timer[Timer.IndexOf(j)] -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;


						int test = Alpha.IndexOf(a);
						Alpha[Alpha.IndexOf(a)] -= (int)(gameTime.ElapsedGameTime.TotalMilliseconds * 0.2f);

						test = Alpha.IndexOf(a);
						Alpha[Alpha.IndexOf(a) + 1] = (int)MathHelper.Clamp(a, 0, 255);

						i.color = new Color(0, 0, 255, a);
						if (j <= 0)
						{
							i.position = new Vector2(600, 15);
							Timer.Remove(0);
							Alpha.RemoveAt(0);
							ScoreAddition.RemoveAt(0);
						}
					}
				}
			}
		}
	}
}
