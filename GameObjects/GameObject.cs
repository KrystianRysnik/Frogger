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
    class GameObject
    {

        public Texture2D Texture { set; get; }
        public Vector2 Position { set; get; }
        public Vector2 Speed { set; get; }
        public Rectangle Location { set; get; }


        public virtual void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(Texture, Location, Color.White);
        }        
    }
}