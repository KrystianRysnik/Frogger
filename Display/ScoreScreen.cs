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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Frogger.Display
{
    class ScoreScreen : Screen
    {
        public ScoreScreen(ContentManager theContent, EventHandler theScreenEvent) : base(theScreenEvent)
        {
        }

        public override void Update(GameTime theThime)
        {
            var touchCol = TouchPanel.GetState();

            foreach (var touch in touchCol)
            {
                if (touch.State == TouchLocationState.Pressed)
                {
                    ScreenEvent.Invoke(this, new EventArgs());
                }
            }
        }

        public override void Draw(SpriteBatch theBatch)
        {
            theBatch.DrawString(Game1.eightBitFont, "SCORE RANKING", new Vector2((Game1.WIDTH / 2) - (6.5F * 28), 6f * 52), Color.Yellow);
            theBatch.DrawString(Game1.eightBitFont, "1 ST   00000 PTS", new Vector2((Game1.WIDTH / 2) - (8 * 28), 7.25f * 52), Color.White);
            theBatch.DrawString(Game1.eightBitFont, "2 ND   00000 PTS", new Vector2((Game1.WIDTH / 2) - (8 * 28), 8f * 52), Color.White);
            theBatch.DrawString(Game1.eightBitFont, "3 RD   00000 PTS", new Vector2((Game1.WIDTH / 2) - (8 * 28), 8.75f * 52), Color.White);
            theBatch.DrawString(Game1.eightBitFont, "4 TH   00000 PTS", new Vector2((Game1.WIDTH / 2) - (8 * 28), 9.5f * 52), Color.White);
            theBatch.DrawString(Game1.eightBitFont, "5 TH   00000 PTS", new Vector2((Game1.WIDTH / 2) - (8 * 28), 10.25f * 52), Color.White);
            theBatch.DrawString(Game1.eightBitFont, "KONAMI  (C)  1981", new Vector2((Game1.WIDTH / 2) - (8.5f * 28), 12.25f * 52), Color.White);
        }
    }
}