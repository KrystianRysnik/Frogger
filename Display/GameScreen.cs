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
using Frogger.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frogger.Display
{
    class GameScreen : Screen
    {
        Frog player;
        List<Wall> walls = new List<Wall>();
        List<Car> cars = new List<Car>();
        List<Turtle> turtles = new List<Turtle>();
        List<Log> logs = new List<Log>();

        string hiScore = "00000";
        string firstUp = "00000";
        long score = 0;
        int time = 60;

        double elapsedTime, timeToUpdate = 1000;

        public GameScreen(ContentManager theContent, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            player = new Frog("green", new Vector2(8 * (Game1.textureManager.frogGreen.Width/6), 14 * 52));

            for (int x = 0; x < 14; x++)
            {
                walls.Add(new Wall(new Vector2(x * Game1.textureManager.wall.Width, 8 * 52)));
            }

            for (int x = 0; x < 14; x++)
            {
                walls.Add(new Wall(new Vector2(x * Game1.textureManager.wall.Width, 14 * 52)));
            }

            turtlesInRowStageOne(7, 3, 52, 52, -52);
            turtlesInRowStageOne(4, 2, 78, 104, -52);
            logsInRowStageOne(6, 3, 104, 104, Game1.WIDTH + 52*3);
            logsInRowStageOne(3, 4, 52, 0, Game1.WIDTH + 52*4);
            logsInRowStageOne(5, 6, 104, 104, Game1.WIDTH + 52 * 3);

            cars.Add(new Car(1, 0));
            cars.Add(new Car(1, 1700));
            cars.Add(new Car(1, 2*1700));
            cars.Add(new Car(1, 3*1700));

            cars.Add(new Car(2, 0));
            cars.Add(new Car(2, 2000));
            cars.Add(new Car(2, 2*2000));
            cars.Add(new Car(2, 3*2000));

            cars.Add(new Car(3, 0));
            cars.Add(new Car(3, 1200));
            cars.Add(new Car(3, 2 * 1200));
            cars.Add(new Car(3, 3 * 1200));
            cars.Add(new Car(3, 4 * 1200));

            cars.Add(new Car(4, 0));
            cars.Add(new Car(4, 2000));
            cars.Add(new Car(4, 2 * 2000));
            cars.Add(new Car(4, 3 * 2000));

            cars.Add(new Car(5, 0));
            cars.Add(new Car(5, 2000));
            cars.Add(new Car(5, 2 * 2000));
            cars.Add(new Car(5, 3 * 2000));

        }

        public override void Update(GameTime theTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.P) || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenEvent.Invoke(this, new EventArgs());
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                score += 1000;
                UpdateScore();          
            }

            elapsedTime += theTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > timeToUpdate)
            {
                elapsedTime -= timeToUpdate;

                time -= 1;
            }

            player.Update(theTime);
            foreach (Car car in cars)
            {
                car.Update(theTime);
                if (car.Location.Intersects(player.Location))
                {
                    //player.IsHit = true;
                    //player.Update(theTime);
                }
            }
            foreach (Turtle turtle in turtles)
            {
                turtle.Update(theTime);
                if (turtle.Location.Intersects(player.Location))
                {
                    player.IsStick = true;
                    player.StickMove(turtle.Position);                  
                }
            }
            foreach (Log log in logs)
            {
                log.Update(theTime);
                if (log.Location.Intersects(player.Location))
                {
                    player.IsStick = true;
                    player.StickMove(log.Position);
                }
            }
        }
        public override void Draw(SpriteBatch theBatch)
        {
            foreach(Wall wall in walls) 
            {
                wall.Draw(theBatch);
            }
            foreach (Car car in cars)
            {
                car.Draw(theBatch);
            }
            foreach (Turtle turtle in turtles)
            {
                turtle.Draw(theBatch);
            }
            foreach (Log log in logs)
            {
                log.Draw(theBatch);
            }
            player.Draw(theBatch);

            theBatch.DrawString(Game1.eightBitFont, "1-UP", new Vector2((Game1.WIDTH / 4) - (2.5f * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, firstUp, new Vector2((Game1.WIDTH / 4) - (3.5f * 28), 40), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "HI-SCORE", new Vector2((Game1.WIDTH / 2) - (4 * 28), 0), Color.White);
            theBatch.DrawString(Game1.eightBitFont, hiScore, new Vector2((Game1.WIDTH / 2) - (2.5f * 28), 40), Color.Red);
            theBatch.DrawString(Game1.eightBitFont, "TIME", new Vector2(Game1.WIDTH - (4 * 28), 15.5f * 52), Color.Yellow);

            base.Draw(theBatch);
        }
        public void StartGame()
        {
            isGameStarted = true;
            isGameOver = false;
        }

        private void UpdateScore()
        {
            firstUp = "00000";
            firstUp += score.ToString();
            firstUp = firstUp.Substring(firstUp.Length - 5);

            if (Int64.Parse(hiScore) < score)
            {
                hiScore = score.ToString();
                if (Int64.Parse(hiScore) > 99990)
                {
                    hiScore = "99990";
                }
            }            
        }


        private void logsInRowStageOne(int row, int length, int spaceBetween, int startFrom, int restart)
        {
            if (length == 6)
            {
                for (int i = 0; i < 3; i++)
                {
                    logs.Add(new Log(length, new Vector2(startFrom, row * 52), restart));
                    startFrom += (6*52) + spaceBetween;
                }
            }
            else if (length == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    logs.Add(new Log(length, new Vector2(startFrom, row * 52), restart));
                    startFrom += (4 * 52) + spaceBetween;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    logs.Add(new Log(length, new Vector2(startFrom, row * 52), restart));
                    startFrom += (3 * 52) + spaceBetween;
                }
            }
        }

        private void turtlesInRowStageOne(int row, int length, int spaceBetween, int startFrom, int restart)
        {
            for (int i = 0; i < length; i++)
            {
                turtles.Add(new Turtle("normal", new Vector2(startFrom, row * 52), restart));
                startFrom += 52;
            }
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles.Add(new Turtle("normal", new Vector2(startFrom, row * 52), restart));
                startFrom += 52;
            }
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles.Add(new Turtle("normal", new Vector2(startFrom, row * 52), restart));
                startFrom += 52;
            }
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles.Add(new Turtle("normal", new Vector2(startFrom, row * 52), restart));
                startFrom += 52;
            }
        }    
    }
}