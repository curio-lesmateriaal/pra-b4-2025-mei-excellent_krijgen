using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        public static Home Window { get; set; } 

        public void Start()
        {
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2,55    Beschrijving: Glanzende fotoprint 10x15 cm\nFoto 20x30: €4,99   Beschrijving: Grote fotoprint 20x30 cm\nPasfoto: €5,00   Beschrijving: Set van 4 officiële pasfoto's");
            ShopManager.SetShopReceipt("Eindbedrag\n€");
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55m, Description = "Glanzende fotoprint 10x15 cm" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 20x30", Price = 4.99m, Description = "Grote fotoprint 20x30 cm" });
            ShopManager.Products.Add(new KioskProduct() { Name = "Pasfoto", Price = 5.00m, Description = "Set van 4 officiële pasfoto's" });
            ShopManager.UpdateDropDownProducts();
        }

        public void AddButtonClick()
        {
            var fotoId = ShopManager.GetFotoId();
            var amount = ShopManager.GetAmount();
            var product = ShopManager.GetSelectedProduct();

            if (fotoId == null || amount == null || product == null) return;

            decimal prijsPerStuk = product.Price;
            decimal totaal = prijsPerStuk * amount.Value;

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
