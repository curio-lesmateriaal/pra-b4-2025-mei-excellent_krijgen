using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Haal het dagnummer van vandaag op
            int todayDayNumber = (int)DateTime.Now.DayOfWeek;

            // Initializeer de lijst met fotos
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                // Haal het mapnummer op (eerste deel van de mapnaam)
                string folderName = Path.GetFileName(dir);
                string[] folderParts = folderName.Split('_');

                if (folderParts.Length > 0 && int.TryParse(folderParts[0], out int folderDayNumber))
                {
                    // Controleer of het mapnummer overeenkomt met het dagnummer van vandaag
                    if (folderDayNumber == todayDayNumber)
                    {
                        // Voeg alle foto's uit deze map toe
                        foreach (string file in Directory.GetFiles(dir))
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                        }
                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Herlaad de foto's
            PicturesToDisplay.Clear();
            Start();
        }
    }
}
