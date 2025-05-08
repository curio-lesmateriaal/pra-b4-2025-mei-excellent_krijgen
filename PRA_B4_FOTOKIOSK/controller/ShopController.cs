using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.IO;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        public static Home Window { get; set; }

        public void Start()
        {
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2.55");
            ShopManager.SetShopReceipt("Eindbedrag\n€");
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15" });
            ShopManager.UpdateDropDownProducts();
        }

        public void AddButtonClick()
        {
            var fotoId = ShopManager.GetFotoId();
            var amount = ShopManager.GetAmount();
            var product = ShopManager.GetSelectedProduct();

            if (fotoId == null || amount == null || product == null) return;

            double prijsPerStuk = 2.55;
            double totaal = prijsPerStuk * amount.Value;

            ShopManager.AddShopReceipt($"\n{product.Name} x{amount} = €{totaal:F2}");

            string fotoPath = $"foto_{fotoId}.jpg";
            if (File.Exists(fotoPath))
            {
                File.Copy(fotoPath, $"bestelling_{fotoId}.jpg", true);
            }
        }

        public void ResetButtonClick()
        {
            ShopManager.SetShopReceipt("Eindbedrag\n€");
        }

        public void SaveButtonClick()
        {
            string inhoud = ShopManager.GetShopReceipt();
            File.WriteAllText("bon.txt", inhoud);
        }
    }
}
