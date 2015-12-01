using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using VoidEngine;

namespace PacKitten
{
	public class Food : Sprite
	{
		protected Game1 myGame;
		public bool DeleteMe = false;

		public Food(Vector2 position, Color color, List<AnimationSet> animationSetList, Game1 game)
			: base(position, color, animationSetList)
		{
			myGame = game;
			SetAnimation("IDLE");
		}

		public override void Update(GameTime gameTime)
		{
			if (Collision.Magnitude(Position - myGame.player.PositionCenter) <= 20)
			{
				DeleteMe = true;
				myGame.AddScore(1);
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}
	}
}
