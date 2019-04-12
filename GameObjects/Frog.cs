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
            theBatch.Draw(Texture, Location, new Rectangle(0, 0, Texture.Width / 6, Texture.Height), Color.White);
            //theBatch.Draw(Texture, Position, Color.White);
        }

        private void KeyboardControl()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) & !previousState.IsKeyDown(Keys.Up))
            {
                Position -= moveVertical;
            }

            if (keyboardState.IsKeyDown(Keys.Down) & !previousState.IsKeyDown(Keys.Down))
            {
                Position += moveVertical;
            }

            if (keyboardState.IsKeyDown(Keys.Left) & !previousState.IsKeyDown(Keys.Left))
            {
                Position -= moveHorizontal;
            }

            if (keyboardState.IsKeyDown(Keys.Right) & !previousState.IsKeyDown(Keys.Right))
            {
                Position += moveHorizontal;
            }

            previousState = Keyboard.GetState();
        }

        private void UpdateLocation()
        {
            if (IsHit == true)
            {
                Position = StartPosition;
                IsHit = false;
            }
            if (IsStick == true)
            {
                Position += new Vector2((int)Move.X, Move.Y);
                IsStick = false;
            }
           
            Location = new Rectangle(
              (int)Position.X,
              (int)Position.Y,
              Texture.Width / 6,
              Texture.Height);
            
        }

        public void StickMove(Vector2 speed) 
        {
            Move = speed;
        }
    }
}