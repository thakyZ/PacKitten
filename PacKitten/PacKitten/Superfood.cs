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
	public class Superfood : Food
	{
		public Superfood(Vector2 position, Color color, List<AnimationSet> animationSetList, Game1 myGame)
			: base(position, color, animationSetList, myGame)
		{
		}

		public override void Update(GameTime gameTime)
		{
			if (Collision.Magnitude(Position - myGame.player.PositionCenter) <= 5)
			{
				DeleteMe = true;
				myGame.buffActive = 3000;
				myGame.AddScore(5);
			}

			base.Update(gameTime);
		}
	}
}
