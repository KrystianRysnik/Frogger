using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Frogger.Helpers
{
    public class ScoreManager
    {
        IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
        int highscore;

        public string[] list;
        public long[] scores = new long[6];

        public ScoreManager()
        {
            ReadScore();          
        }

        public void ReadScore()
        {
            if (savegameStorage.FileExists("highscore.txt"))
            {
                IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("highscore.txt", FileMode.OpenOrCreate, FileAccess.Read);
                using (StreamReader sr = new StreamReader(isoStream))
                {
                    list = sr.ReadLine().Split(",");
                }
                for (int i = 0; i < 5; i++)
                {
                    scores[i] = Convert.ToInt64(list[i]);
                }
            }
            else
            {
                scores = new long[] { 0, 0, 0, 0, 0, 0 };
                Array.Sort(scores);
                Array.Reverse(scores);
            }
        }

        public void SaveScore(long score)
        {
            scores[5] = score;
            Array.Sort(scores);
            Array.Reverse(scores);
            string line = "";
            for (int i = 0; i < 5; i++)
            {
                line += scores[i] + ",";
             }

            IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("highscore.txt", FileMode.OpenOrCreate, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(isoStream))
            {
                sw.Flush();
                sw.WriteLine(line);
            }
        }
    }
}