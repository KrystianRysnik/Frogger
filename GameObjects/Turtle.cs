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
    class Turtle
    {
        public Texture2D Texture { set; get; }
        public Rectangle Location { set; get; }
        public Vector2 Position { set; get; }

        // normal, diver
        string Name { set; get; }
        int Length { set; get; }
        int RestartPosition { set; get; }

        int frameIndex = 0;
        int maxFrameIndex = 0;

        double elapsedTime, timeToUpdate = 250;

        public Turtle(string name, Vector2 position, int restartPosition)
        {
            Name = name;
            RestartPosition = restartPosition;
            Texture = FroggerGame.textureManager.turtle;
            Location = new Rectangle((int)position.X, (int)position.Y, Texture.Width / 2, Texture.Height);
            Position = new Vector2(-1.2f, 0);
            if (Name == "diver")
            {
                maxFrameIndex = 4;
            }
            else
            {
                maxFrameIndex = 2;
            }

        }

        public void Update(GameTime theTime)
        {
            if (Location.X <= RestartPosition)
            {
                Location = new Rectangle(FroggerGame.WIDTH + Texture.Width / 5, Location.Y, Texture.Width / 5, Texture.Height);
            }
            Location = new Rectangle(Location.X + (int)Position.X, Location.Y, Texture.Width / 5, Texture.Height);

            elapsedTime += theTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToUpdate)
            {
                elapsedTime -= timeToUpdate;

                if (frameIndex < maxFrameIndex)
                    frameIndex++;
                else
                    frameIndex = 0;
            }
        }

        public void Draw(SpriteBatch theBatch)
        {
            if (Name == "normal")
            {
                theBatch.Draw(Texture, Location, new Rectangle(Texture.Width / 5 * frameIndex, 0, Texture.Width / 5, Texture.Height), Color.White);
            }
            else if (Name == "diver")
            {
                theBatch.Draw(Texture, Location, new Rectangle(Texture.Width / 5 * frameIndex, 0, Texture.Width / 5, Texture.Height), Color.White);
            }
        }
    }
}