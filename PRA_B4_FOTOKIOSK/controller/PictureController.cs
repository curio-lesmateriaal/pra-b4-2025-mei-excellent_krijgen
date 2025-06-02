using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System.Collections.Generic;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        public static Home Window { get; set; }
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        public void Start()
        {
            PicturesToDisplay.Clear();

            string fotoMap = @"../../../fotos";

            PicturesToDisplay = PictureManager.GetRecenteFotos(fotoMap);

            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        public void RefreshButtonClick()
        {
            Start();
        }
    }
}


