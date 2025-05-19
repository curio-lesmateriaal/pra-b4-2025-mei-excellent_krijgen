using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        private int currentDayNumber = 0; // 0 = Zondag

        public Home()
        {
            InitializeComponent();

            LoadPictures(currentDayNumber);

            // Voorbeeld producten vullen (pas aan naar eigen data)
            cbProducts.Items.Add("Product A");
            cbProducts.Items.Add("Product B");
            cbProducts.Items.Add("Product C");
        }

        private void LoadPictures(int dayNumber)
        {
            spPictures.Children.Clear();

            string basePath = @"C:\aaa_blok_B\blok_b\pra\pra-b4-2025-mei-excellent_krijgen\PRA_B4_FOTOKIOSK\fotos\";
            string[] days = { "Zondag", "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag" };

            if (dayNumber < 0 || dayNumber > 6)
            {
                MessageBox.Show("Ongeldig dagnummer!");
                return;
            }

            string folderName = $"{dayNumber}_{days[dayNumber]}";
            string photosFolder = Path.Combine(basePath, folderName);

            if (!Directory.Exists(photosFolder))
            {
                MessageBox.Show("Foto map bestaat niet: " + photosFolder);
                return;
            }

            string[] files = Directory.GetFiles(photosFolder, "*.jpg");

            foreach (string file in files)
            {
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(file);
                    bitmap.DecodePixelWidth = 550;
                    bitmap.DecodePixelHeight = 308;
                    bitmap.EndInit();

                    Image image = new Image
                    {
                        Source = bitmap,
                        Width = 550,
                        Height = 308,
                        Margin = new Thickness(5)
                    };

                    spPictures.Children.Add(image);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fout bij laden foto: " + ex.Message);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadPictures(currentDayNumber);
        }

        private void btnNextDay_Click(object sender, RoutedEventArgs e)
        {
            currentDayNumber = (currentDayNumber + 1) % 7;
            LoadPictures(currentDayNumber);
        }

        private void btnPreviousDay_Click(object sender, RoutedEventArgs e)
        {
            currentDayNumber = (currentDayNumber - 1 + 7) % 7;
            LoadPictures(currentDayNumber);
        }

        // Hier kan je ook je andere event handlers voor kassa en zoeken zetten
        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            // Voeg toe logica hier
        }

        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            // Reset logica hier
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Bon opslaan logica hier
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            // Zoek logica hier
        }
    }
}
