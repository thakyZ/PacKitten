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
	class Food : Sprite
	{
		Game1 myGame;
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
			}

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			base.Draw(gameTime, spriteBatch);
		}
	}
}
