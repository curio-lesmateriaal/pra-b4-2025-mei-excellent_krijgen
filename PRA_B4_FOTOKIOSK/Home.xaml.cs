using PRA_B4_FOTOKIOSK.controller;
using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PRA_B4_FOTOKIOSK
{
    public partial class Home : Window
    {
        public ShopController ShopController { get; set; }
        public PictureController PictureController { get; set; }
        public SearchController SearchController { get; set; }

        public Home()
        {
            InitializeComponent();

            // Maak eerst de controllers aan
            ShopController = new ShopController();
            PictureController = new PictureController();
            SearchController = new SearchController();

            // Zet dit window in de managers
            PictureManager.Instance = this;
            ShopManager.Instance = this;

            // Koppel window aan controllers
            ShopController.Window = this;
            PictureController.Window = this;
            SearchController.Window = this;

            // Start controllers
            PictureController.Start();
            ShopController.Start();
            SearchController.Start();
        }

        private void btnShopAdd_Click(object sender, RoutedEventArgs e)
        {
            ShopController.AddButtonClick();
        }

        private void btnShopReset_Click(object sender, RoutedEventArgs e)
        {
            ShopController.ResetButtonClick();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            PictureController.RefreshButtonClick();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ShopController.SaveButtonClick();
        }

        private void btnZoeken_Click(object sender, RoutedEventArgs e)
        {
            SearchController.SearchButtonClick();
        }
    }
}
