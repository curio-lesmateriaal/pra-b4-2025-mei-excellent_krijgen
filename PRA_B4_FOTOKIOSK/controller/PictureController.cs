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

        // De lijst met foto's die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent
        public void Start()
        {
            try
            {
                // Initializeer de lijst met foto's
                // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
                PicturesToDisplay.Clear(); // Clear the previous list of pictures

                foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
                {
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        // Filter for image files if necessary (e.g., only .jpg, .png)
                        if (file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                        {
                            PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                        }
                    }
                }

                // Update de foto's
                PictureManager.UpdatePictures(PicturesToDisplay);
            }
            catch (Exception ex)
            {
                // Log the exception (could be expanded for specific file errors, etc.)
                Console.WriteLine($"Error while loading photos: {ex.Message}");
            }
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {
            // Reload the pictures when the refresh button is clicked
            Start();
        }
    }
}
