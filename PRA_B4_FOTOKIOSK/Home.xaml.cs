using PRA_B4_FOTOKIOSK.controller;
using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Windows;

namespace PRA_B4_FOTOKIOSK
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        // Declareer de controllers
        public ShopController ShopController { get; set; }
        public PictureController PictureController { get; set; }
        public SearchController SearchController { get; set; }

        public Home()
        {
            // Bouw de UI
            InitializeComponent();

            // Controleer of de Manager instantie correct is (zorg ervoor dat deze statisch is)
            PictureManager.Instance = this;
            ShopManager.Instance = this;
            SearchController.Window = this;

            // Maak de controllers aan
            ShopController = new ShopController();
            PictureController = new PictureController();
            SearchController = new SearchController();

            // Koppel de controllers aan het huidige Window
            ShopController.Window = this;
            PictureController.Window = this;
            SearchController.Window = this;

            // Start de pagina's via de controllers
            PictureController.Start();
            ShopController.Start();
            SearchController.Start();
        }

        // Event handler voor het klikken op de knop "Toevoegen"
        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShopController.AddButtonClick();  // Zorg ervoor dat je logica voor toevoegen is geïmplementeerd in de controller
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het toevoegen: {ex.Message}");
            }
        }

        // Event handler voor het klikken op de knop "Resetten"
        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShopController.ResetButtonClick();  // Zorg ervoor dat je logica voor resetten is geïmplementeerd in de controller
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het resetten: {ex.Message}");
            }
        }

        // Event handler voor het klikken op de knop "Vernieuwen"
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PictureController.RefreshButtonClick();  // Zorg ervoor dat je logica voor vernieuwen is geïmplementeerd in de controller
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het vernieuwen: {ex.Message}");
            }
        }

        // Event handler voor het klikken op de knop "Bon Opslaan"
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShopController.SaveButtonClick();  // Zorg ervoor dat je logica voor opslaan is geïmplementeerd in de controller
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het opslaan: {ex.Message}");
            }
        }

        // Event handler voor het klikken op de knop "Zoeken"
        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchController.SearchButtonClick();  // Zorg ervoor dat je logica voor zoeken is geïmplementeerd in de controller
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Er is een fout opgetreden bij het zoeken: {ex.Message}");
            }
        }
    }
}
