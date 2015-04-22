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
