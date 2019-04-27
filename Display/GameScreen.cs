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
        List<Meta> meta = new List<Meta>();

        int Level { set; get; } // Show max 15
        int Life { set; get; }
        int MetaReach = 0;
        
        public GameScreen(ContentManager theContent, EventHandler theScreenEvent) : base(theScreenEvent)
        {
            hud = new HUD();
            player = new Frog("green", new Vector2(8 * (Game1.textureManager.frogGreen.Width/6), 14 * 52));

            for (int i = 0; i < 5; i++)
            {
                meta.Add(new Meta(new Vector2(23 + (Game1.textureManager.meta.Width/2 * i + 98 * i), 52+49)));
            }

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
                hud.Level++;
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
            foreach (Meta m in meta)
            {
                if (m.Location.Intersects(player.Location) && !m.IsShow && player.Location.Y < 3 * 52)
                {
                    m.IsShow = true;
                    MetaReach++;
                    hud.Score += 200;
                    hud.Time = 60.0f;
                    player.RestartLocation();
                }
                if (MetaReach == 5)
                {
                    NewStage();
                }
            }
        }

        public override void Draw(SpriteBatch theBatch)
        {
            walls.ForEach(wall => wall.Draw(theBatch));
            cars.ForEach(car => car.Draw(theBatch));
            logs.ForEach(log => log.Draw(theBatch));
            meta.ForEach(meta => meta.Draw(theBatch));

            foreach (Turtle[] turtles in groupOfTurtles)
            {
                foreach (Turtle turtle in turtles)
                {
                    turtle.Draw(theBatch);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                theBatch.Draw(Game1.textureManager.forest, new Vector2(i * Game1.textureManager.forest.Width, 52), Color.White);
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

        private void NewStage()
        {            
            hud.Level++;
            player.RestartLocation();
            foreach (Meta m in meta)
            {
                m.IsShow = false;
            }
            MetaReach = 0;
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