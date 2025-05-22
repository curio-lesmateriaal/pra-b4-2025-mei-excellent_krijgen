using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System.Collections.Generic;
using System.IO;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class ShopController
    {
        private List<OrderedProduct> bonList = new List<OrderedProduct>();
        internal IReadOnlyList<OrderedProduct> BonList => bonList.AsReadOnly();

        private decimal totaal = 0;
        public decimal Totaal => totaal;

        public void Start()
        {
            ShopManager.SetShopPriceList("Prijzen:\nFoto 10x15: €2,55\nFoto 20x30: €4,99\nPasfoto: €5,00");
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

            decimal totaalPrijs = product.Price * amount.Value;

            bonList.Add(new OrderedProduct
            {
                FotoNummer = fotoId.Value,
                ProductNaam = product.Name,
                Aantal = amount.Value,
            });

            totaal += totaalPrijs;

            ShopManager.AddShopReceipt($"\n{product.Name} x{amount} = €{totaalPrijs:F2}");

            string fotoPath = $"foto_{fotoId}.jpg";
            if (File.Exists(fotoPath))
            {
                File.Copy(fotoPath, $"bestelling_{fotoId}.jpg", true);
            }
        }

        public void ResetButtonClick()
        {
            ShopManager.SetShopReceipt("Eindbedrag\n€");
            bonList.Clear();
            totaal = 0;
        }

        public void SaveButtonClick()
        {
            string inhoud = MaakBonTekst();
            File.WriteAllText("bon.txt", inhoud);
        }

        public string MaakBonTekst()
        {
            string bonInhoud = "";
            foreach (var op in bonList)
            {
                bonInhoud += $"{op.FotoNummer} - {op.ProductNaam} x{op.Aantal} = €{op.TotaalPrijs:F2}\n";
            }
            bonInhoud += $"Totaal: €{totaal:F2}";
            return bonInhoud;
        }
    }
}
