using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using VoidEngine;

namespace PacKitten
{
	public class Enemy : Sprite
	{
		Game1 myGame;

		public Enemy(Vector2 position, Color color, List<AnimationSet> animationSetList, Game1 myGame) : base(position, color, animationSetList)
		{
			this.myGame = myGame;
			SetAnimation("IDLE");
			RotationCenter = new Vector2(15, 15);
			Speed = 1.5f;
		}

		public override void Update(GameTime gameTime)
		{
			if(myGame.buffActive == 0)
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

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}
	}
}
