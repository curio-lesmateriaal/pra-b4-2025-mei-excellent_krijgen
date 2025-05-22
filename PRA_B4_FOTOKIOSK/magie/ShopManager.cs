using PRA_B4_FOTOKIOSK.models;
using System.Collections.Generic;

namespace PRA_B4_FOTOKIOSK.magie
{
    public class ShopManager
    {
        public static List<KioskProduct> Products = new List<KioskProduct>();
        public static Home Instance { get; set; }

        public static void SetShopPriceList(string text)
        {
            if (Instance != null)
                Instance.lbPrices.Content = text;
        }

        public static void AddShopPriceList(string Name, decimal Price, string Description)
        {
            if (Instance != null)
                Instance.lbPrices.Content += $"{Name} - €{Price:F2} - {Description}\n";
        }

        public static void AddShopPriceList(string text)
        {
            if (Instance != null)
                Instance.lbPrices.Content += text;
        }

        public static void SetShopReceipt(string text)
        {
            if (Instance != null)
                Instance.lbReceipt.Content = text;
        }

        public static string GetShopReceipt()
        {
            return Instance != null ? (string)Instance.lbReceipt.Content : string.Empty;
        }

        public static void AddShopReceipt(string text)
        {
            if (Instance != null)
                SetShopReceipt(GetShopReceipt() + text);
        }

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

        public static int? GetFotoId()
        {
            if (int.TryParse(Instance?.tbFotoId.Text, out int id))
                return id;
            return null;
        }

        public static int? GetAmount()
        {
            if (int.TryParse(Instance?.tbAmount.Text, out int amount))
                return amount;
            return null;
        }
    }
}
