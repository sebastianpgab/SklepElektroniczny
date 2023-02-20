using System.Collections.Generic;

namespace SklepElektroniczny1501.Entities
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Model { get; set; }
        public string Opis { get; set; }
        public int? IloscDostepna { get; set; }
        public decimal? Cena { get; set; }

        public virtual List<ZamowienieProdukt> ZamowienieProdukty { get; set; }
        public virtual List<ProduktKategoria> ProduktKategorie { get; set; }
    }
}