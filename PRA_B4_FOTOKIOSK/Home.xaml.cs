using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using PRA_B4_FOTOKIOSK.controller;
using PRA_B4_FOTOKIOSK.magie;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        private int currentDayNumber;

        public ShopController ShopController { get; set; }
        public PictureController PictureController { get; set; }
        public SearchController SearchController { get; set; }

        public Home()
        {
            InitializeComponent();

            currentDayNumber = (int)DateTime.Now.DayOfWeek;

            ShopController = new ShopController();
            PictureController = new PictureController();
            SearchController = new SearchController();

            ShopManager.Instance = this;

            PictureController.Start();
            ShopController.Start();
            SearchController.Start();

            LoadPictures(currentDayNumber);
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

        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            ShopController?.AddButtonClick();
        }

        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            ShopController?.ResetButtonClick();
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string bonInhoud = ShopController.MaakBonTekst();

                string documentenPad = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string bestandspad = Path.Combine(documentenPad, "kassabon.txt");

                File.WriteAllText(bestandspad, bonInhoud);

                MessageBox.Show("Bon opgeslagen in Documenten!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout bij opslaan bon: " + ex.Message);
            }
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            // Search logic here if needed
        }
    }
}