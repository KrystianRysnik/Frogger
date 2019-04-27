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
    class Meta
    {
        public Rectangle Location { set; get; }
        Texture2D Texture { set; get; }
        public bool IsShow { set; get; }

        public Meta(Vector2 _position)
        {
            Texture = Game1.textureManager.meta;
            Location = new Rectangle((int)_position.X, (int)_position.Y, Texture.Width / 2, Texture.Height);
            IsShow = false;
        }

        public void Update(GameTime theTime)
        {

        }

        public void Draw(SpriteBatch theBatch)
        {
            if (IsShow)
            {
                theBatch.Draw(Texture, new Rectangle(Location.X + Texture.Width / 2, Location.Y + Texture.Height / 2, Location.Width, Location.Height), new Rectangle(0, 0, Texture.Width / 2, Texture.Height), Color.White, 0f, new Vector2(Texture.Width / 2, Texture.Height / 2), SpriteEffects.None, 1);
            }
        }
    }
}