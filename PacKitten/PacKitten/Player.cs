using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Player : Sprite
    {
        Game1 myGame;
		public Point GridPosition;
		public Vector2 PositionCenter;
		public bool Respawn;

        public Player(Vector2 position, Color color, List<AnimationSet> animationSetList, Game1 myGame)
            : base(position, color, animationSetList)
        {
            this.myGame = myGame;
            RotationCenter = new Vector2(15, 15);
            Speed = 3;
            Direction = Vector2.Zero;
            SetAnimation("IDLE");
			Offset = new Vector2(-15, -15);
        }

        public override void Update(GameTime gameTime)
        {
			if (Respawn)
			{
				Respawn = false;
				switch (new Random().Next(4))
				{
					case 0:
						Position = new Vector2(0, 0);
						break;
					case 1:
						Position = new Vector2(540, 0);
						break;
					case 2:
						Position = new Vector2(0, 450);
						break;
					case 3:
						Position = new Vector2(540, 450);
						break;
				}
			}

            if (Direction.Y == -1)
            {
                if ((int)((Position.Y - Speed) / 30) < (int)(Position.Y / 30))
                {
                    Position.Y -= Position.Y % 30;
                }
                if (Position.Y % 30 == 0 && Position.Y > 0)
                {
					if (myGame.map[(int)(Position.Y / 30 - 1)][(int)(Position.X / 30)] != '.')
                    {
                        Direction.Y = 0;
                    }
                }
            }
            if (Direction.Y == 1)
            {
                if ((int)((Position.Y - Speed) / 30) > (int)(Position.Y / 30))
                {
                    Position.Y += Position.Y % 30;
					Direction.Y = 0;
                }
                if (Position.Y % 30 == 0 && Position.Y < 450)
                {
					if (myGame.map[(int)(Position.Y / 30 + 1)][(int)(Position.X / 30)] != '.')
                    {
                        Direction.Y = 0;
                    }
                }
            }
            if (Direction.X == -1)
            {
                if ((int)((Position.X - Speed) / 30) < (int)(Position.X / 30))
                {
                    Position.X -= Position.X % 30;
                }
                if (Position.X % 30 == 0 && Position.X > 0)
                {
                    if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 - 1)] != '.')
                    {
                        Direction.X = 0;
                    }
                }
            }
            if (Direction.X == 1)
            {
                if ((int)((Position.X - Speed) / 30) > (int)(Position.X / 30))
                {
					Position.X += Position.X % 30;
					Direction.X = 0;
                }
                if (Position.X % 30 == 0 && Position.X < 540)
                {
					if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 + 1)] != '.')
                    {
                        Direction.X = 0;
                    }
                }
			}

			if (myGame.keyboardState.IsKeyDown(Keys.W))
			{
				if (Position.X % 30 == 0 && Position.Y % 30 == 0 && Position.Y > 0)
				{
					if (myGame.map[(int)(Position.Y / 30 - 1)][(int)(Position.X / 30)] == '.')
					{
						Rotation = -90 * (float)Math.PI / 180;
						//Rotation = 0;
						FlipSprite(Axis.NONE);
						Direction.Y = -1;
						Direction.X = 0;
					}
				}
			}
			if (myGame.keyboardState.IsKeyDown(Keys.S))
			{
				if (Position.X % 30 == 0 && Position.Y % 30 == 0 && Position.Y < 450)
				{
					if (myGame.map[(int)(Position.Y / 30 + 1)][(int)(Position.X / 30)] == '.')
					{
						Rotation = 90 * (float)Math.PI / 180;
						FlipSprite(Axis.NONE);
						Direction.Y = 1;
						Direction.X = 0;
					}
				}
			}
			if (myGame.keyboardState.IsKeyDown(Keys.A))
			{
				if (Position.Y % 30 == 0 && Position.X % 30 == 0 && Position.X > 0)
				{
					if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 - 1)] == '.')
					{
						Rotation = 180 * (float)Math.PI / 180;
						FlipSprite(Axis.X);
						Direction.X = -1;
						Direction.Y = 0;
					}
				}
			}
			if (myGame.keyboardState.IsKeyDown(Keys.D))
			{
				if (Position.Y % 30 == 0 && Position.X % 30 == 0 && Position.X < 540)
				{
					if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 + 1)] == '.')
					{
						//Rotation = 0 * (float)Math.PI / 180;
						Rotation = 0;
						FlipSprite(Axis.NONE);
						Direction.X = 1;
						Direction.Y = 0;
					}
				}
			}

			Position += (Direction * Speed);

            if (Position.X < 0)
            {
                Position.X = 0;
                Direction.X = 0;
            }
            if (Position.Y < 0)
            {
                Position.Y = 0;
                Direction.Y = 0;
            }
            if (Position.X > 540)
            {
                Position.X = 540;
                Direction.X = 0;
            }
            if (Position.Y > 450)
            {
                Position.Y = 450;
                Direction.Y = 0;
            }

			GridPosition = new Point((int)(Position.X / 30), (int)(Position.Y / 30));

			base.Update(gameTime);

			PositionCenter = Position + RotationCenter;
        }

        /// <summary>
        /// Put inbetween the spriteBatch.Begin and spriteBatch.End
        /// </summary>
        /// <param name="gameTime">The main GameTime</param>
        /// <param name="spriteBatch">The main SpriteBatch</param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
