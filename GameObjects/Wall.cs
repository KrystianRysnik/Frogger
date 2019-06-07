using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frogger.GameObjects
{
    class Wall
    {
        Vector2 Location { set; get; }
        Texture2D Texture { set; get; }

        public Wall(Vector2 position)
        {
            Texture = FroggerGame.textureManager.wall;
            Location = position;
        }

        public void Update(GameTime theTime)
        {

        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(Texture, Location, Color.White);
        }

    }
}