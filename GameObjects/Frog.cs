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
using Microsoft.Xna.Framework.Input.Touch;

namespace Frogger.GameObjects
{
    class Frog : GameObject
    {
        Vector2 StartPosition { set; get; }
        public Vector2 Move { set; get; }    

        Vector2 moveHorizontal;
        Vector2 moveVertical;
        public Vector2 moveVector;

        public bool IsMoving { set; get; }
        public bool IsHit { set; get; }
        public bool IsStick { set; get; }
        public bool IsCollision { set; get; }  
        public bool IsDead { set; get; }
        public bool IsDrown { set; get; }

        float angle = 0f;
        float elapsedTime, keyDelay = 0, timeToUpdate = 300;
        int textureIdx = 2, deadIdx = 0;

        public Frog(string name, Vector2 position)
        {
            IsHit = false;
            IsStick = false;
            IsDead = false;
            IsDrown = false;
            Texture = FroggerGame.textureManager.frogGreen;
            StartPosition = position;

            Location = new Rectangle(
                (int)position.X,
                (int)position.Y,
                Texture.Width / 6,
                Texture.Height);

            Position = position;
            moveVector = position;

            moveHorizontal = new Vector2(Texture.Width / 6, 0);
            moveVertical = new Vector2(0, Texture.Height);
        }

        public void Update(GameTime theTime)
        {          
            HandleTime(theTime);
            if (!IsDead)
            {
                TouchControl();
                UpdateLocation(theTime);
            }
        }

        public override void Draw(SpriteBatch theBatch)
        {
            if (!IsDead)
            {
                theBatch.Draw(Texture, new Rectangle(Location.X + Texture.Width / 12, Location.Y + Texture.Height / 2, Location.Width, Location.Height), new Rectangle(Texture.Width / 6 * textureIdx, 0, Texture.Width / 6, Texture.Height), Color.White, angle, new Vector2(Texture.Width / 12, Texture.Height / 2), SpriteEffects.None, 1);
            }
            else if (IsDrown)
            {
                theBatch.Draw(FroggerGame.textureManager.frogDrown, new Rectangle(Location.X + FroggerGame.textureManager.frogDrown.Width / 8, Location.Y + FroggerGame.textureManager.frogDrown.Height / 2, Location.Width, Location.Height), new Rectangle(FroggerGame.textureManager.frogDrown.Width / 4 * deadIdx, 0, FroggerGame.textureManager.frogDrown.Width / 4, FroggerGame.textureManager.frogDrown.Height), Color.White, 0f, new Vector2(Texture.Width / 8, Texture.Height / 2), SpriteEffects.None, 1);
            }
            else
            {
                theBatch.Draw(FroggerGame.textureManager.frogDead, new Rectangle(Location.X + FroggerGame.textureManager.frogDead.Width / 8, Location.Y + FroggerGame.textureManager.frogDead.Height / 2, Location.Width, Location.Height), new Rectangle(FroggerGame.textureManager.frogDead.Width / 4 * deadIdx, 0, FroggerGame.textureManager.frogDead.Width / 4, FroggerGame.textureManager.frogDead.Height), Color.White, 0f, new Vector2(Texture.Width / 8, Texture.Height / 2), SpriteEffects.None, 1);
            }

        }

        private void HandleTime(GameTime theTime)
        {
            elapsedTime += (float)theTime.ElapsedGameTime.TotalMilliseconds;
            if (keyDelay > 0)
            {
                keyDelay -= (float)theTime.ElapsedGameTime.TotalMilliseconds;
            }
            else
            {
                IsMoving = false;
            }

            if (IsDead && timeToUpdate > 0)
            {
                timeToUpdate -= (float)theTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (IsDead)
            {
                if (deadIdx > 3)
                {
                    IsDead = false;
                    IsDrown = false;
                    keyDelay = 150;
                    RestartLocation();
                    IsMoving = false;
                    deadIdx = 0;
                }
                timeToUpdate = 300;
                deadIdx++;
            }
        }
    
        public void TouchControl()
        {
            var gesture = default(GestureSample);

            while (TouchPanel.IsGestureAvailable)
            {
                gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.VerticalDrag && keyDelay <= 0)
                {
                    
                    if (gesture.Delta.Y < 0)
                    {
                        moveVector = Position - moveVertical;
                        angle = 0f;
                    }
                    if (gesture.Delta.Y > 0 && Location.Y < StartPosition.Y)
                    {
                        moveVector = Position + moveVertical;
                        angle = (float)Math.PI;                      
                    }

                    keyDelay = 150;
                    FroggerGame.audioManager.hop.Play();
                    IsMoving = true;
                }

                if (gesture.GestureType == GestureType.HorizontalDrag && keyDelay <= 0)
                {

                    if (gesture.Delta.X < 0 && Location.X > 0)
                    {
                        moveVector = Position - moveHorizontal;
                        angle = (float)Math.PI * 1.5f;

                    }
                    if (gesture.Delta.X > 0 && Location.X < FroggerGame.WIDTH - (Texture.Width / 3))
                    {
                        moveVector = Position + moveHorizontal;
                        angle = (float)Math.PI / 2;
                    }
                    keyDelay = 150;
                    FroggerGame.audioManager.hop.Play();
                    IsMoving = true;
                } 
            }
        }       

        private void UpdateLocation(GameTime theTime)
        {        
            if ((Position.X != moveVector.X || Position.Y != moveVector.Y) && keyDelay > 0 && !IsDead)
            {
                Position = Vector2.Lerp(Position, moveVector, (150 - keyDelay) / 150);

                if (keyDelay > 0 && keyDelay < 20)
                {
                    textureIdx = 0;
                }
                else if (keyDelay > 20) {
                    textureIdx = 1;
                }
            }
            else if (!IsDead)
            {
                IsMoving = false;
                textureIdx = 2;
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
            moveVector = StartPosition;
            angle = 0f;
            Location = new Rectangle((int)StartPosition.X, (int)StartPosition.Y, Texture.Width / 6, Texture.Height);
        }     
    }
}