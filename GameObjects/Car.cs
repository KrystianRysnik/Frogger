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
    class Car
    {
        int Row { set; get; }
        Vector2 Speed { set; get; }
        Texture2D Texture { set; get; }
        public Rectangle Location { set; get; }

        bool move = false;

        double elapsedTime, timeToUpdate = 8000, timeToMove;

        public Car(int row, int delay)
        {
            Row = row;
            timeToMove = delay;
            switch (Row)
            {
                case 1:
                    Texture = Game1.textureManager.carFirst;
                    Location = new Rectangle(Game1.WIDTH + Texture.Width, 13 * 52, Texture.Width, Texture.Height);
                    Speed = new Vector2(-2, 0);
                    break;
                case 2:
                    Texture = Game1.textureManager.carSecond;
                    Location = new Rectangle(0 - Texture.Width, 12 * 52, Texture.Width, Texture.Height);
                    Speed = new Vector2(2, 0);
                    break;
                case 3:
                    Texture = Game1.textureManager.carThird;
                    Location = new Rectangle(Game1.WIDTH + Texture.Width, 11 * 52, Texture.Width, Texture.Height);
                    Speed = new Vector2(-2, 0);
                    break;
                case 4:
                    Texture = Game1.textureManager.carFourth;
                    Location = new Rectangle(0 - Texture.Width, 10 * 52, Texture.Width, Texture.Height);
                    Speed = new Vector2(2, 0);
                    break;
                case 5:
                    Texture = Game1.textureManager.carFifth;
                    Location = new Rectangle(Game1.WIDTH + Texture.Width, 9 * 52, Texture.Width, Texture.Height);
                    Speed = new Vector2(-2, 0);
                    break;
            }
        }

        public void Update(GameTime theTime)
        {
            elapsedTime += theTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToMove)
            {
                move = true;
            }

            if (move)
            {
                if (elapsedTime > timeToUpdate + timeToMove)
                {
                    elapsedTime -= timeToUpdate;

                    if (Speed.X < 0)
                    {
                        Location = new Rectangle(Game1.WIDTH + Texture.Width, Location.Y, Texture.Width, Texture.Height);
                    }
                    else
                    {
                        Location = new Rectangle(0 - Texture.Width, Location.Y, Texture.Width, Texture.Height);
                    }
                }

                Location = new Rectangle(Location.X + (int)Speed.X, Location.Y, Texture.Width, Texture.Height);
            }




        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(Texture, Location, Color.White);
        }
    }
}