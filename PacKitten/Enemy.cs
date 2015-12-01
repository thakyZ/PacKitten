using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using VoidEngine;

namespace PacKitten
{
	public class Enemy : Sprite
	{
		Game1 myGame;

		public int timer = 5000;

		public Vector2 Direction;
		public float Speed;

		public bool Dead = false;

		public Enemy(Vector2 position, Color color, List<AnimationSet> animationSetList, Game1 myGame) : base(position, color, animationSetList)
		{
			this.myGame = myGame;
			SetAnimation("IDLE");
			RotationCenter = new Vector2(15, 15);
			Speed = 1.5f;
			Offset = new Vector2(-15, -15);
		}

		public override void Update(GameTime gameTime)
		{
			if (Dead)
			{
				timer -= (int)gameTime.ElapsedGameTime.Milliseconds;

				if (timer <= 0)
				{
					Dead = false;
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
					timer = 5000;
					_Color = Color.White;
				}
			}
			if (myGame.buffActive == 0 && !Dead)
			{
				if (Collision.Magnitude(myGame.player.PositionCenter - (Position + RotationCenter)) <= 30)
				{
					myGame.player.Respawn = true;
				}

				if (Position.X % 30 == 0 && Position.Y % 30 == 0)
				{
					Point gridPos = new Point((int)(Position.X / 30), (int)(Position.Y / 30));
					Direction.X = 0;
					Direction.Y = 0;
					Direction = Pathing.aStar(gridPos, myGame.player.GridPosition, myGame.map);
					Position += Direction * Speed;
				}
				else
				{
					if (Direction.Y == -1)
					{
						Rotation = -90 * (float)Math.PI / 180;
						//Rotation = 0;
						FlipSprite(Axis.NONE);
						if ((int)((Position.Y - Speed) / 30) < (int)(Position.Y / 30))
						{
							Position.Y -= Position.Y % 30;
							Direction.Y = 0;
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
						Rotation = 90 * (float)Math.PI / 180;
						FlipSprite(Axis.NONE);
						if ((int)((Position.X - Speed) / 30) < (int)(Position.X / 30))
						{
							Position.X -= Position.X % 30;
							Direction.X = 0;
						}
						if (Position.X % 30 == 0 && Position.X > 0)
						{
							if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 - 1)] != '.')
							{
								Direction.X = 0;
							}
						}
					}
					if (Direction.X == -1)
					{
						Rotation = 180 * (float)Math.PI / 180;
						FlipSprite(Axis.X);
						if ((int)((Position.X - Speed) / 30) < (int)(Position.X / 30))
						{
							Position.X -= Position.X % 30;
							Direction.X = 0;
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
						//Rotation = 0 * (float)Math.PI / 180;
						Rotation = 0;
						FlipSprite(Axis.NONE);
						if ((int)((Position.X - Speed) / 30) < (int)(Position.X / 30))
						{
							Position.X += Position.X % 30;
							Direction.X = 0;
						}
						if (Position.X % 30 == 0 && Position.X > 540)
						{
							if (myGame.map[(int)(Position.Y / 30)][(int)(Position.X / 30 + 1)] != '.')
							{
								Direction.Y = 0;
							}
						}
					}
				}

				Position += Direction * Speed;

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
			}
			else
			{
				if (Collision.Magnitude(myGame.player.PositionCenter - (Position + RotationCenter)) <= 30)
				{
					Dead = true;
					myGame.AddScore(10);
				}
			}

			if (Dead)
			{
				_Color = Color.Red;
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}
	}
}
