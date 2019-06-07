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

        int RestartPosition { set; get; }

        public Car(int row, Vector2 position, int restartPosition)
        {
            Row = row;
            RestartPosition = restartPosition;
            switch (Row)
            {
                case 13:
                    Texture = FroggerGame.textureManager.carFirst;
                    Speed = new Vector2(-1.5f, 0);
                    break;
                case 12:
                    Texture = FroggerGame.textureManager.carSecond;
                    Speed = new Vector2(1.5f, 0);
                    break;
                case 11:
                    Texture = FroggerGame.textureManager.carThird;
                    Speed = new Vector2(-1.5f, 0);
                    break;
                case 10:
                    Texture = FroggerGame.textureManager.carFourth;
                    Speed = new Vector2(1.4f, 0);
                    break;
                case 9:
                    Texture = FroggerGame.textureManager.carFifth;
                    Speed = new Vector2(-1.3f, 0);
                    break;
                default:
                    break;
            }

            Location = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        public void Update(GameTime theTime)
        {
            if (Location.X >= RestartPosition && RestartPosition >= FroggerGame.WIDTH)
            {
                Location = new Rectangle(-Texture.Width, Location.Y, Texture.Width, Texture.Height);
            }
            else if (Location.X <= RestartPosition && RestartPosition <= 0)
            {
                Location = new Rectangle(FroggerGame.WIDTH + Texture.Width, Location.Y, Texture.Width, Texture.Height);
            }
            Location = new Rectangle(Location.X + (int)Speed.X, Location.Y, Texture.Width, Texture.Height);
        }   

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.Draw(Texture, Location, Color.White);
        }
    }
}