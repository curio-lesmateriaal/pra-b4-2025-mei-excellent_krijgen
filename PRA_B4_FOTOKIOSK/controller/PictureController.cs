using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        public static Home Window { get; set; }
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        public void Start()
        {
            PicturesToDisplay.Clear();

            int todayDayNumber = (int)DateTime.Now.DayOfWeek;
            DateTime now = DateTime.Now;

            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                string folderName = Path.GetFileName(dir);
                string[] folderParts = folderName.Split('_');

                if (folderParts.Length > 0 && int.TryParse(folderParts[0], out int folderDayNumber))
                {
                    if (folderDayNumber == todayDayNumber)
                    {
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            DateTime created = File.GetCreationTime(file);

                            TimeSpan verschil = now - created;
                            if (verschil.TotalMinutes >= 5 && verschil.TotalMinutes <= 30)
                            {
                                PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                            }
                        }
                    }
                }
            }
        }

        public void RefreshButtonClick()
        {
            PicturesToDisplay.Clear();
            Start();
        }
    }
}
