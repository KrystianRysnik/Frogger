﻿using System;
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
    class Log
    {
        public Texture2D Texture { set; get; }
        public Rectangle Location { set; get; }
        public Vector2 Position { set; get; }

        int Length { set; get; }
        int RestartPosition { set; get; }

        public Log(int length, Vector2 position, int restartPosition)
        {
            Length = length;
            RestartPosition = restartPosition;
            if (Length == 6)
            {
                Texture = Game1.textureManager.logLonger;
                Position = new Vector2(2.4f, 0);
            }
            else if (Length == 4)
            {
                Texture = Game1.textureManager.logLong;
                Position = new Vector2(1.4f, 0);
            }
            else
            {
                Texture = Game1.textureManager.log;
                Position = new Vector2(1.2f, 0);
            }

            Location = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        public void Update(GameTime theTime)
        {
            if (Location.X >= RestartPosition)
            {
                Location = new Rectangle(-Texture.Width, Location.Y, Texture.Width, Texture.Height);
            }
            Location = new Rectangle(Location.X + (int)Position.X, Location.Y, Texture.Width, Texture.Height);
        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(Texture, Location, Color.White);
        }
    }
}