namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
namespace PRA_B4_FOTOKIOSK.models
{
    public class OrderedProduct
    {
        public int FotoNummer { get; set; }
        public string ProductNaam { get; set; }
        public int Aantal { get; set; }
        public decimal TotaalPrijs { get; set; }
    }
}

