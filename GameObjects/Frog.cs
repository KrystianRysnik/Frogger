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
using Microsoft.Xna.Framework.Input;

namespace Frogger.GameObjects
{
    class Frog
    {
        Vector2 StartPosition { set; get; }
        Vector2 Position { set; get; }
        Vector2 Move { set; get; }
        Texture2D Texture { set; get; }
    
        public Rectangle Location { set; get; }

        KeyboardState keyboardState;
        KeyboardState previousState;

        Vector2 moveHorizontal;
        Vector2 moveVertical;

        public bool IsHit { set; get; }
        public bool IsStick { set; get; }
        public bool isCollision { set; get; }

        float angle = 0f;

        public Frog(string name, Vector2 position)
        {
            IsHit = false;
            IsStick = false;
            Texture = Game1.textureManager.frogGreen;
            StartPosition = position;

            Location = new Rectangle(
                (int)position.X,
                (int)position.Y,
                Texture.Width / 6,
                Texture.Height);

            Position = position;

            moveHorizontal = new Vector2(Texture.Width / 6, 0);
            moveVertical = new Vector2(0, Texture.Height);
        }

        public void Update(GameTime theTime)
        {
            KeyboardControl();
            UpdateLocation();
        }

        public void Draw(SpriteBatch theBatch)
        {
           theBatch.Draw(Texture, new Rectangle(Location.X + Texture.Width/12, Location.Y + Texture.Height/2, Location.Width, Location.Height), new Rectangle(0, 0, Texture.Width / 6, Texture.Height), Color.White, angle, new Vector2(Texture.Width / 12, Texture.Height / 2), SpriteEffects.None, 1);


        }

        private void KeyboardControl()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) & !previousState.IsKeyDown(Keys.Up))
            {
                Position -= moveVertical;
                angle = 0f;
            }

            if (keyboardState.IsKeyDown(Keys.Down) & !previousState.IsKeyDown(Keys.Down))
            {
                Position += moveVertical;
                angle = (float)Math.PI;
            }

            if (keyboardState.IsKeyDown(Keys.Left) & !previousState.IsKeyDown(Keys.Left))
            {
                Position -= moveHorizontal;
                angle = (float)Math.PI * 1.5f;
            }

            if (keyboardState.IsKeyDown(Keys.Right) & !previousState.IsKeyDown(Keys.Right))
            {
                Position += moveHorizontal;
                angle = (float)Math.PI / 2;
            }

            previousState = Keyboard.GetState();
        }

        private void UpdateLocation()
        {
            if (IsHit == true)
            {
                RestartLocation();
                IsHit = false;
            }
            if (IsStick == true)
            {
                Position += new Vector2((int)Move.X, Move.Y);
                IsStick = false;
            }
            else if (Location.Y >= 3 * 52 && Location.Y <= 7 * 52 && !IsStick)
            {
                RestartLocation();
            }

            Location = new Rectangle(
              (int)Position.X,
              (int)Position.Y,
              Texture.Width / 6,
              Texture.Height);         
        }

        public bool ShouldIStickToThisObject(Log log) 
        {
            if (log.Location.X - 10 < Location.X
                && log.Location.X + log.Texture.Width - Texture.Width/6 + 10 > Location.X)
            {
                return true;
            }
            return false;
        }

        public bool ShouldIStickToThisObject(Turtle turtle)
        {
            if (turtle.Location.X - 10 < Location.X
                && turtle.Location.X + 10  > Location.X)
            {
                return true;
            }
            return false;
        }

        public void StickMove(Vector2 speed) 
        {
            Move = speed;
        }
             
        public void RestartLocation()
        {
            Position = StartPosition;
            Location = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, Texture.Width / 6, Texture.Height);
        }
    }
}