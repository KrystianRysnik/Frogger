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

namespace Frogger.Helpers
{
    public class AudioManager
    {
         public AudioManager(ContentManager theContent)
        {
            loadAudio(theContent);
        }

        private void loadAudio(ContentManager theContent)
        {

        }
    }
}