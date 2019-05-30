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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Frogger.GameObjects
{
    class HUD
    {
        string hiScore = "00000";
        string firstUp = "00000";
        string timeString = "00";
        public long Score { set; get; }
        public long Level { set; get; }
        public long Life { set; get; }
        public float Time { set; get; }
        public bool isGameOver = false;
        public bool isGameStarted = true;
        public bool isReachMeta = false;
        public bool isTimeEnd = false;
        public bool isSlideBackground = false;
        float slideEffect = 1.3f;
        public Vector2 slidePosition;


        float elapsedTime, timeToUpdate = 500, showTimeDelay = 4000, gameOverDelay = 50;

        int Fill { set; get; }

        public HUD()
        {
            hiScore = "00000" + Game1.scoreManager.scores.Max().ToString();
            hiScore = hiScore.Substring(hiScore.Length - 5);
            slidePosition = new Vector2(Game1.WIDTH * slideEffect, 0);
            Time = 60.0f;
            Score = 0;
            Life = 4;
            Level = 1;
            Fill = Game1.timeCounter.Width - (int)((float)Time / 60f * (float)Game1.timeCounter.Width);
        }

        public void Update(GameTime theTime)
        {
            if (Time > 0.0f)
            {
                if (isReachMeta)
                {
                    ShowTime(theTime);
                }
                else
                {
                    UpdateTime(theTime);
                }
                UpdateScore();
            }
            else
            {
                isTimeEnd = true;
            }

            if (Life <= 0 )
            {
                isGameOver = true;
                Game1.audioManager.themeInstance.Stop();

                if (Game1.audioManager.scoreInstance.State != SoundState.Playing)
                {
                    Game1.audioManager.scoreInstance.Stop();
                    Game1.audioManager.scoreInstance.Play();
                }
            }

            if (isGameOver)
            {
                GameOverTime(theTime);
            }
        }

        public void Draw(SpriteBatch theBatch)
        {
            theBatch.DrawString(Game1.eightBitFont, "1-UP", new Vector2((Game1.WIDTH / 4) - (2.5f * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, firstUp, new Vector2((Game1.WIDTH / 4) - (3.5f * 28), 30), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "HI-SCORE", new Vector2((Game1.WIDTH / 2) - (4 * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, hiScore, new Vector2((Game1.WIDTH / 2) - (2.5f * 28), 30), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "TIME", new Vector2(Game1.WIDTH - (4 * 28), 15.5f * 52), Color.Yellow);
            for (int i = 0; i < Life; i++)
            {
                theBatch.Draw(Game1.textureManager.life, new Vector2(i * Game1.textureManager.life.Width + ((i + 1) * 3) , 15 * 52 + 3), Color.White);
            }
            for (int i = 0; i < 15; i++)
            {
                theBatch.Draw(Game1.textureManager.level, new Vector2(Game1.WIDTH - ((i + 1) * Game1.textureManager.life.Width + ((i + 1) * 3)), 15 * 52 + 3), Color.White);
                if (Level - 1 <= i)
                {
                    break;
                }
            }
            theBatch.Draw(Game1.timeCounter, new Vector2(4.5f * 52, 15.5f * 52 + 8), Color.White);
            if (isReachMeta)
            {
                theBatch.Draw(Game1.timeBackground, new Vector2(Game1.WIDTH / 2 - Game1.timeBackground.Width / 2, 8.5f * 52), Color.White);
                theBatch.DrawString(Game1.eightBitFont, "TIME " + (timeString += (((int)Time).ToString())).Substring(timeString.Length - 2), new Vector2(Game1.WIDTH/2 - (3.5f * 28), 8.5f * 52), Color.Red);
            }
            if (isGameOver)
            {
                theBatch.Draw(Game1.gameOverBackground, new Vector2(Game1.WIDTH / 2 - Game1.timeBackground.Width / 2, 8.5f * 52), Color.White);
                theBatch.DrawString(Game1.eightBitFont, "GAME OVER", new Vector2(Game1.WIDTH / 2 - (3.5f * 28), 8.5f * 52), Color.Red);
                theBatch.Draw(Game1.blackBackground, slidePosition, Color.White);
                theBatch.Draw(Game1.waterBackground, slidePosition, Color.White);
            }
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

        private void ShowTime(GameTime theTime)
        {
            elapsedTime += (float)theTime.ElapsedGameTime.TotalMilliseconds;
            if (showTimeDelay > 0)
            {
                showTimeDelay -= (float)theTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                isReachMeta = false;
                showTimeDelay = 4000;
                Time = 60.0f;
            }
        }

        private void GameOverTime(GameTime theTime)
        {
            elapsedTime += (float)theTime.ElapsedGameTime.TotalMilliseconds;
            if (gameOverDelay > 0)
            {
                gameOverDelay -= (float)theTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                if (slideEffect > 0)
                {
                    gameOverDelay = 75;
                    slideEffect -= 0.05f;
                    slidePosition = new Vector2(Game1.WIDTH * slideEffect, 0);
                    if (slidePosition.X < 0)
                    {
                        slidePosition = new Vector2(0, 0);
                    }
                }
            }
        }

        private void UpdateTime(GameTime theTime)
        {
            elapsedTime += (float)theTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToUpdate)
            {
                elapsedTime -= timeToUpdate;
                Time -= 0.5f;

                Fill = Game1.timeCounter.Width - (int)((float)Time / 60f * (float)Game1.timeCounter.Width);
                
                Color[] data = new Color[Game1.timeCounter.Width * Game1.timeCounter.Height];
                for (int j = 0; j < Game1.timeCounter.Height; ++j)
                {
                    for (int b = 0; b < Fill; b++)
                    {
                        data[j * Game1.timeCounter.Width + b] = new Color(0, 0, 0);
                    }
                    if (Time <= 10)
                    {
                        for (int g = Fill; g < Game1.timeCounter.Width; g++)
                        {
                            data[j * Game1.timeCounter.Width + g] = Color.Red;
                        }
                    }
                    else
                    {
                        for (int g = Fill; g < Game1.timeCounter.Width; g++)
                        {
                            data[j * Game1.timeCounter.Width + g] = new Color(20, 190, 0);
                        }
                    }
                }
                Game1.timeCounter.SetData(data);
            }

         
        }
    }
}