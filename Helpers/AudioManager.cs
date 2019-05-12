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
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Frogger.Helpers
{
    public class AudioManager
    {
        public SoundEffect coin, extra, hop, plunk, squash, time;

         public AudioManager(ContentManager theContent)
        {
            loadAudio(theContent);
        }

        private void loadAudio(ContentManager theContent)
        {
            coin = theContent.Load<SoundEffect>("Audio/coin");
            extra = theContent.Load<SoundEffect>("Audio/extra");
            hop = theContent.Load<SoundEffect>("Audio/hop");
            plunk = theContent.Load<SoundEffect>("Audio/plunk");
            squash = theContent.Load<SoundEffect>("Audio/squash");
            time = theContent.Load<SoundEffect>("Audio/time");
        }
    }
}