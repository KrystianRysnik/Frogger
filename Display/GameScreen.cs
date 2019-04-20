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
        HUD hud;
        Frog player;
        List<Wall> walls = new List<Wall>();
        List<Car> cars = new List<Car>();
        List<Turtle[]> groupOfTurtles = new List<Turtle[]>();
        List<Log> logs = new List<Log>();
        
        public GameScreen(ContentManager theContent, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            hud = new HUD();
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

            hud.Update(theTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                hud.Score += 1000;
                hud.UpdateScore();          
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
            foreach (Turtle[] turtles in groupOfTurtles)
            {
                foreach (Turtle turtle in turtles)
                {
                    turtle.Update(theTime);
                    if (turtle.Location.Intersects(player.Location))
                    {
                        player.isCollision = true;
                    }
                }
                if (player.isCollision)
                {
                    if (turtles[0].Location.X < player.Location.X + 15 && turtles[0].Location.Y == player.Location.Y
                        && player.Location.X - 15 < turtles[turtles.Length - 1].Location.X)
                    {
                        player.IsStick = true;
                        player.StickMove(turtles[0].Position);
                        player.isCollision = false;
                    }
                }            
            }
            foreach (Log log in logs)
            {
                log.Update(theTime);
                if (log.Location.Intersects(player.Location))
                { 
                    if (player.ShouldIStickToThisObject(log))
                    {
                        player.IsStick = true;
                        player.StickMove(log.Position);
                    }
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
            foreach (Turtle[] turtles in groupOfTurtles)
            {
                foreach (Turtle turtle in turtles)
                {
                    turtle.Draw(theBatch);
                }
            }
            foreach (Log log in logs)
            {
                log.Draw(theBatch);
            }
            player.Draw(theBatch);

            hud.Draw(theBatch);

            base.Draw(theBatch);
        }
        public void StartGame()
        {
            isGameStarted = true;
            isGameOver = false;
            player.RestartLocation();
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
            Turtle[] turtles = new Turtle[length];
            for (int i = 0; i < length; i++)
            {
                turtles[i] = new Turtle("diver", new Vector2(startFrom, row * 52), restart);
                startFrom += 52;
            }
            groupOfTurtles.Add(turtles);
            turtles = new Turtle[length];
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles[i] = new Turtle("normal", new Vector2(startFrom, row * 52), restart);
                startFrom += 52;
            }
            groupOfTurtles.Add(turtles);
            turtles = new Turtle[length];
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles[i] = new Turtle("normal", new Vector2(startFrom, row * 52), restart);
                startFrom += 52;
            }
            groupOfTurtles.Add(turtles);
            turtles = new Turtle[length];
            startFrom += spaceBetween;
            for (int i = 0; i < length; i++)
            {
                turtles[i] = new Turtle("normal", new Vector2(startFrom, row * 52), restart);
                startFrom += 52;
            }
        }    
    }
}