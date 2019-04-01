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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Frogger.Helpers
{
    public class TextureManager
    {
        // Frogs
        public Texture2D frogGreen;

        // Cars
        public Texture2D carFirst,
            carSecond,
            carThird,
            carFourth,
            carFifth;

        // Wall
        public Texture2D wall;

        // Water
        public Texture2D water;
        public Texture2D turtle;
        public Texture2D log;
        
        public TextureManager(ContentManager theContent)
        {
            loadTextures(theContent);
        }

        private void loadTextures(ContentManager theContent)
        {
            frogGreen = theContent.Load<Texture2D>("Texture/frogGreen");

            carFirst = theContent.Load<Texture2D>("Texture/car_1");
            carSecond = theContent.Load<Texture2D>("Texture/car_2");
            carThird = theContent.Load<Texture2D>("Texture/car_3");
            carFourth = theContent.Load<Texture2D>("Texture/car_4");
            carFifth = theContent.Load<Texture2D>("Texture/car_5");

            wall = theContent.Load<Texture2D>("Texture/wall");
            turtle = theContent.Load<Texture2D>("Texture/turtle");
            log = theContent.Load<Texture2D>("Texture/log");

           
        }
    }
}