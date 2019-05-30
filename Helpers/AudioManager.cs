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
using Microsoft.Xna.Framework.Media;

namespace Frogger.Helpers
{
    public class AudioManager
    {
        public SoundEffect coin, extra, hop, plunk, squash, time, theme, score;
        public SoundEffectInstance themeInstance, scoreInstance, squashInstance, plunkInstance, extraInstance;

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
            theme = theContent.Load<SoundEffect>("Audio/theme");
            score = theContent.Load<SoundEffect>("Audio/score");

            themeInstance = theme.CreateInstance();
            scoreInstance = score.CreateInstance();
            squashInstance = squash.CreateInstance();
            plunkInstance = plunk.CreateInstance();
            extraInstance = extra.CreateInstance();

            themeInstance.IsLooped = true;
        }
    }
}