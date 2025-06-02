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

        public static List<KioskPhoto> GetRecenteFotos(string rootMap)
        {
            List<KioskPhoto> recenteFotos = new List<KioskPhoto>();

            DateTime now = DateTime.Now;
            DateTime minTime = now.AddMinutes(-30);
            DateTime maxTime = now.AddMinutes(-2);

            int todayDayNumber = (int)now.DayOfWeek;

            foreach (string dagMap in Directory.GetDirectories(rootMap))
            {
                string folderName = Path.GetFileName(dagMap);
                string[] parts = folderName.Split('_');

                if (parts.Length > 0 && int.TryParse(parts[0], out int folderDayNumber))
                {
                    if (folderDayNumber == todayDayNumber)
                    {
                        foreach (string fotoPad in Directory.GetFiles(dagMap, "*.jpg"))
                        {
                            string fileName = Path.GetFileName(fotoPad);
                            string[] fileParts = fileName.Split('_', '-');

                            if (fileParts.Length >= 6)
                            {
                                try
                                {
                                    DateTime fotoTijd = new DateTime(
                                        int.Parse(fileParts[0]),
                                        int.Parse(fileParts[1]),
                                        int.Parse(fileParts[2]),
                                        int.Parse(fileParts[3]),
                                        int.Parse(fileParts[4]),
                                        int.Parse(fileParts[5].Split('.')[0])
                                    );

                                    if (fotoTijd >= minTime && fotoTijd <= maxTime)
                                    {
                                        recenteFotos.Add(new KioskPhoto { Source = fotoPad });
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }

            return recenteFotos;
        }

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
                    Width = 550,
                    Height = 308,
                    Margin = new System.Windows.Thickness(5)
                };

                Instance.spPictures.Children.Add(image);
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
                img.Freeze();

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
