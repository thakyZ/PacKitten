using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
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

		Texture2D rectange;

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
            tileAnimationSetList.Add(new Sprite.AnimationSet(".", tileTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600));
            tileAnimationSetList.Add(new Sprite.AnimationSet("1", tileTexture, new Point(30, 30), new Point(0, 0), new Point(30, 0), 1600));
            tileList = new List<Tiles>();
            map = MapGen();

            playerTexture = Content.Load<Texture2D>(@"images\player\player");
            playerAnimationSetList = new List<Sprite.AnimationSet>();
            playerAnimationSetList.Add(new Sprite.AnimationSet("IDLE", playerTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600));

			enemyTexture = Content.Load<Texture2D>(@"images\enemies\enemy");
			enemyAnimationSetList = new List<Sprite.AnimationSet>();
			enemyAnimationSetList.Add(new Sprite.AnimationSet("IDLE", enemyTexture, new Point(30, 30), new Point(0, 0), new Point(0, 0), 1600));
			enemies = new List<Enemy>();

			smallFoodTexture = Content.Load<Texture2D>(@"images\pickups\pickup");
			smallFoodAnimationSetList = new List<Sprite.AnimationSet>();
			smallFoodAnimationSetList.Add(new Sprite.AnimationSet("IDLE", playerTexture, new Point(10, 10), new Point(0, 0), new Point(0, 0), 1600));
			food = new List<Food>();

			superFoodTexture = Content.Load<Texture2D>(@"images\pickups\superpickup");
			superFoodAnimationSetList = new List<Sprite.AnimationSet>();
			superFoodAnimationSetList.Add(new Sprite.AnimationSet("IDLE", superFoodTexture, new Point(20, 20), new Point(0, 0), new Point(0, 0), 1600));
			superFood = new List<Superfood>();

			segoeuimonodebug = Content.Load<SpriteFont>(@"fonts\segoeuimonodebug");
			debugLabel = new Label(new Vector2(585, 15), segoeuimonodebug, 1.0f, Color.Black, "");

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

			debugLabel.Update(gameTime, debugStrings[0] + "\n" +
										debugStrings[1] + "\n" +
										debugStrings[2] + "\n" +
										debugStrings[3] + "\n" +
										debugStrings[4] + "\n" +
										debugStrings[5] + "\n" +
										debugStrings[6] + "\n" +
										debugStrings[7] + "\n" +
										debugStrings[8] + "\n" +
										debugStrings[9]);

			debugStrings[0] = "buffActive=" + buffActive;
			debugStrings[1] = "score=" + score;

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

				spriteBatch.Draw(rectange, new Rectangle(570, 0, 

				debugLabel.Draw(gameTime, spriteBatch);
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
            tmpList.Add(".....1E.....E1.....");
            tmpList.Add(".1.1.1.1.1.1.1.1.1.");
            tmpList.Add(".1.1.1.1.1.1.1.1.1.");
            tmpList.Add(".....1...E...1.....");
            tmpList.Add(".1.1.1111.1111.1.1.");
            tmpList.Add(".1.1S.........S1.1.");
            tmpList.Add(".1.111.1.1.1.111.1.");
            tmpList.Add(".1.....1.1.1.....1.");
            tmpList.Add(".11111.1.1.1.11111.");
            tmpList.Add("S.................S");
            return tmpList;
        }
    }
}
