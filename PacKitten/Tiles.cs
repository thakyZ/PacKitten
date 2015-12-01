using Microsoft.Xna.Framework;
using System.Collections.Generic;
using VoidEngine;

namespace PacKitten
{
	public class Tiles : Sprite
	{
		Game1 myGame;

		public Tiles(Vector2 position, string hp, Color color, List<AnimationSet> animationSetList, Game1 myGame)
			: base(position, color, animationSetList)
		{
			this.myGame = myGame;
			if (hp == "P")
			{
				//hp = ".";
			}
			SetAnimation(hp);
		}
	}
}
