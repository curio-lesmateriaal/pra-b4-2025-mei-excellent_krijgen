using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class ShopManager
    {
        // Zorg ervoor dat deze lijst met producten daadwerkelijk gevuld wordt
        public static List<KioskProduct> Products = new List<KioskProduct>();

        // Singleton-instance van de Home window
        public static Home Instance { get; set; }

        // Zet de prijs lijst in de Label van de Home window
        public static void SetShopPriceList(string text)
        {
            Instance.lbPrices.Content = text;
        }

        public static void AddShopPriceList(string Name, decimal Price, string Description)
        {
            Instance.lbPrices.Content += $"{Name} - €{Price:F2} - {Description}\n";
        }

        public static void SetShopReceipt(string text)
        {
            Instance.lbReceipt.Content = text;
        }

        public static string GetShopReceipt()
        {
            return (string)Instance.lbReceipt.Content;
        }

        public static void AddShopReceipt(string text)
        {
            SetShopReceipt(GetShopReceipt() + text);
        }

        public static void UpdateDropDownProducts()
        {
            Instance.cbProducts.Items.Clear();
            foreach (KioskProduct item in Products)
            {
                Instance.lbPrices.Content = text;
            }
        }

        // Voeg iets toe aan de prijs lijst in de Label
        public static void AddShopPriceList(string text)
        {
            if (Instance != null)
            {
                Instance.lbPrices.Content = Instance.lbPrices.Content + text;
            }
        }

        // Zet de bon tekst in de Label van de Home window
        public static void SetShopReceipt(string text)
        {
            if (Instance != null)
            {
                Instance.lbReceipt.Content = text;
            }
        }

        // Haal de bon tekst op uit de Label van de Home window
        public static string GetShopReceipt()
        {
            if (Instance != null)
            {
                return (string)Instance.lbReceipt.Content;
            }
            return string.Empty;
        }

        // Voeg iets toe aan de bon
        public static void AddShopReceipt(string text)
        {
            if (Instance != null)
            {
                SetShopReceipt(GetShopReceipt() + text);
            }
        }

        // Update de dropdown met producten
        public static void UpdateDropDownProducts()
        {
            if (Instance != null)
            {
                Instance.cbProducts.Items.Clear();
                foreach (KioskProduct item in Products)
                {
                    Instance.cbProducts.Items.Add(item.Name);
                }
            }
        }

        // Haal het geselecteerde product op uit de dropdown
        public static KioskProduct GetSelectedProduct()
        {
            if (Instance?.cbProducts.SelectedItem == null) return null;
            string selected = Instance.cbProducts.SelectedItem.ToString();
            foreach (KioskProduct product in Products)
            {
                if (product.Name == selected) return product;
            }
            return null;
        }

        // Haal het foto ID op uit de tekstbox
        public static int? GetFotoId()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance?.tbFotoId.Text, out amount))
            {
                id = amount;
            }
            return id;
        }

        // Haal het aantal op uit de tekstbox
        public static int? GetAmount()
        {
            int? id = null;
            int amount = -1;
            if (int.TryParse(Instance?.tbAmount.Text, out amount))
            {
                id = amount;
            }
            return id;
        }
    }
}
