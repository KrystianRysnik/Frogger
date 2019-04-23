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

        // Wall, forest
        public Texture2D wall,
            forest;

        // Water
        public Texture2D turtle;
        public Texture2D log;
        public Texture2D logLong;
        public Texture2D logLonger;

        // HUD 
        public Texture2D life;
        public Texture2D level;


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

            forest = theContent.Load<Texture2D>("Texture/forest");
            wall = theContent.Load<Texture2D>("Texture/wall");
            turtle = theContent.Load<Texture2D>("Texture/turtle");
            log = theContent.Load<Texture2D>("Texture/log");
            logLong = theContent.Load<Texture2D>("Texture/logLong");
            logLonger = theContent.Load<Texture2D>("Texture/logLonger");

            life = theContent.Load<Texture2D>("Texture/life");
            level = theContent.Load<Texture2D>("Texture/level");
        }
    }
}