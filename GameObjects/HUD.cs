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
    class HUD
    {
        string hiScore = "00000";
        string firstUp = "00000";
        public long Score { set; get; }
        public long Level { set; get; }
        public long Life { set; get; }
        int Time { set; get; }

        double elapsedTime, timeToUpdate = 1000;

        int Fill { set; get; }

        public HUD()
        {
            Time = 60;
            Score = 0;
            Life = 4;
            Level = 1;
            Fill = Game1.timeCounter.Width - (int)((float)Time / 60f * (float)Game1.timeCounter.Width);
        }

        public void Update(GameTime theTime)
        {
            if (Time > 0)
            {
                UpdateTime(theTime);
            }
            else
            {
                // TODO: Game Over / Play Again
            }
            
        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.DrawString(Game1.eightBitFont, "1-UP", new Vector2((Game1.WIDTH / 4) - (2.5f * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, firstUp, new Vector2((Game1.WIDTH / 4) - (3.5f * 28), 40), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "HI-SCORE", new Vector2((Game1.WIDTH / 2) - (4 * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, hiScore, new Vector2((Game1.WIDTH / 2) - (2.5f * 28), 40), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "TIME", new Vector2(Game1.WIDTH - (4 * 28), 15.5f * 52), Color.Yellow);
            for (int i = 0; i < Life; i++)
            {
                theBatch.Draw(Game1.textureManager.life, new Vector2(i * Game1.textureManager.life.Width + ((i + 1) * 3) , 15 * 52 + 3), Color.White);
            }
            for (int i = 0; i < 15; i++)
            {
                theBatch.Draw(Game1.textureManager.level, new Vector2(Game1.WIDTH - ((i + 1) * Game1.textureManager.life.Width + ((i + 1) * 3)), 15 * 52 + 3), Color.White);
                if (i >= Level)
                {
                    break;
                }
            }
            theBatch.Draw(Game1.timeCounter, new Vector2(4.5f * 52, 15.5f * 52 + 8), Color.White);
        }

        public void UpdateScore()
        {
            firstUp = "00000";
            firstUp += Score.ToString();
            firstUp = firstUp.Substring(firstUp.Length - 5);

            if (Int64.Parse(hiScore) < Score)
            {
                hiScore = "00000" + Score.ToString();
                hiScore = hiScore.Substring(hiScore.Length - 5);
                if (Int64.Parse(hiScore) > 99990)
                {
                    hiScore = "99990";
                }
            }
        }

        private void UpdateTime(GameTime theTime)
        {
            elapsedTime += theTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToUpdate)
            {
                elapsedTime -= timeToUpdate;
                Time -= 1;

                Fill = Game1.timeCounter.Width - (int)((float)Time / 60f * (float)Game1.timeCounter.Width);
                
                Color[] data = new Color[Game1.timeCounter.Width * Game1.timeCounter.Height];
                for (int j = 0; j < Game1.timeCounter.Height; ++j)
                {
                    for (int b = 0; b < Fill; b++)
                    {
                        data[j * Game1.timeCounter.Width + b] = new Color(0, 0, 0);
                    }
                    for (int g = Fill; g < Game1.timeCounter.Width; g++)
                    {
                        data[j * Game1.timeCounter.Width + g] = new Color(20, 190, 0);
                    }
                }
                Game1.timeCounter.SetData(data);
            }

         
        }
    }
}