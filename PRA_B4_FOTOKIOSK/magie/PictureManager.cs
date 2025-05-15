using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class PictureManager
    {
        public static Home Instance { get; set; }

        public static void UpdatePictures(List<KioskPhoto> picturesToDisplay)
        {
            if (Instance == null || Instance.spPictures == null)
                return;

            Instance.spPictures.Children.Clear();

            foreach (KioskPhoto picture in picturesToDisplay)
            {
                BitmapImage bitmap = PathToImage(picture.Source);
                if (bitmap == null)
                    continue;

                Image image = new Image
                {
                    Source = bitmap,
                    Width = 550,  // 1920 / 3.5
                    Height = 308, // 1080 / 3.5
                    Margin = new System.Windows.Thickness(5)
                };

                Instance.spPictures.Children.Add(image);
                Console.WriteLine(picture);
            }
        }

        public static BitmapImage PathToImage(string path)
        {
            try
            {
                var stream = new MemoryStream(File.ReadAllBytes(path));
                var img = new BitmapImage();

                img.BeginInit();
                img.StreamSource = stream;
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze(); // voor performance en thread safety

                return img;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Image Error] {ex.Message}");
                return null;
            }
        }
    }
}
