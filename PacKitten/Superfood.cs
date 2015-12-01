using Microsoft.Xna.Framework;
using System.Collections.Generic;
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
