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
    public class SearchController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }
        

        // Start methode die wordt aangeroepen wanneer de zoek pagina opent.
        public void Start()
        {

        }

        // Pseudocode plan:
        // 1. In SearchButtonClick, find the matching image (assume B3 logic is implemented elsewhere or to be added here later).
        // 2. Extract ID, tijd, datum from the filename of the found image.
        //    - Assume filename format: "<id>_<datum>_<tijd>.ext" (e.g., "1234_20240607_153000.jpg")
        // 3. Parse the filename to get id, datum, tijd.
        // 4. Format the string: $"ID: {id} | Tijd: {tijd} | Datum: {datum}"
        // 5. Call SearchManager.SetSearchImageInfo(info);

        public void SearchButtonClick()
        {
            // Example: Assume we have found a matching image file
            // Replace this with actual search logic as needed
            string foundFileName = "1234_20240607_153000.jpg"; // Example filename

            // Remove extension
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(foundFileName);

            // Split by '_'
            string[] parts = fileNameWithoutExt.Split('_');
            if (parts.Length >= 3)
            {
                string id = parts[0];
                string datumRaw = parts[1];
                string tijdRaw = parts[2];

                // Format datum: YYYYMMDD -> DD-MM-YYYY
                string datum = datumRaw.Length == 8
                    ? $"{datumRaw.Substring(6, 2)}-{datumRaw.Substring(4, 2)}-{datumRaw.Substring(0, 4)}"
                    : datumRaw;

                // Format tijd: HHMMSS -> HH:MM:SS
                string tijd = tijdRaw.Length == 6
                    ? $"{tijdRaw.Substring(0, 2)}:{tijdRaw.Substring(2, 2)}:{tijdRaw.Substring(4, 2)}"
                    : tijdRaw;

                string info = $"ID: {id} | Tijd: {tijd} | Datum: {datum}";

                // Show info using SearchManager
                SearchManager.SetSearchImageInfo(info);
            }
            else
            {
                // Fallback if filename format is unexpected
                SearchManager.SetSearchImageInfo("Geen geldige bestandsnaam gevonden.");
            }
        }

    }
}
