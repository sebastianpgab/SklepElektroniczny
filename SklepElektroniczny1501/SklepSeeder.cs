using Microsoft.EntityFrameworkCore;
using SklepElektroniczny1501.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep
{
    public class SklepSeeder
    {
        private readonly SklepDbContext _context;

        public SklepSeeder(SklepDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if (!_context.Kategorie.Any())
            {
                var kategorie = new List<Kategoria>
                {
                    new Kategoria { Nazwa = "Dyski SSD" },
                    new Kategoria { Nazwa = "Laptopy" },
                    new Kategoria { Nazwa = "Monitory" }
                };

                _context.Kategorie.AddRange(kategorie);
                _context.SaveChanges();
            }

            if (!_context.Produkty.Any())
            {
                var produkty = new List<Produkt>
                {
                    new Produkt { Nazwa = "Samsung Portable SSD T7", Model = "MU-PC1T0H/WW", Opis = "Samsung Portable SSD T7 500GB USB 3.2 Gen. 2 Szary", IloscDostepna = 50, Cena = 299.00M },
                    new Produkt { Nazwa = "ASUS TUF Gaming F15 FX506", Model = "FX506LH-HN261", Opis = "ASUS TUF Gaming F15 FX506LH-HN261 i5-10300H/15,6\"FHD/8GB/SSD512GB/NVIDIA GTX 1650/DOS/Czarny", IloscDostepna = 30, Cena = 3199.00M },
                    new Produkt { Nazwa = "LG 24MK430H-B", Model = "24MK430H-B", Opis = "LG 24MK430H-B 24\" IPS FreeSync 75Hz Czarny", IloscDostepna = 20, Cena = 569.00M }
                };

                _context.Produkty.AddRange(produkty);
                _context.SaveChanges();
            }

            if (!_context.ProduktKatergorie.Any())
            {
                var produktKategorie = new List<ProduktKategoria>
                {
                    new ProduktKategoria { IdProduktu = 1, IdKategoria = 1 },
                    new ProduktKategoria { IdProduktu = 2, IdKategoria = 2 },
                    new ProduktKategoria { IdProduktu = 3, IdKategoria = 3 }
                };

                _context.ProduktKatergorie.AddRange(produktKategorie);
                _context.SaveChanges();
            }

            if (!_context.Zmowienia.Any())
            {
                var zamowienia = new List<Zamowienie>
                {
                    new Zamowienie { NumerZamowienia = "ZAM-2022-001", DataZamowienia = DateTime.Parse("2022-01-01") },
                    new Zamowienie { NumerZamowienia = "ZAM-2022-002", DataZamowienia = DateTime.Parse("2022-02-01") }
                };

                _context.Zmowienia.AddRange(zamowienia);
                _context.SaveChanges();
            }
            if (!_context.ZamowienieProdukty.Any())
            {
                var zamowienieProdukty = new List<ZamowienieProdukt>
                {
                    new ZamowienieProdukt { IdZamowienie = 1, IdProdukt = 1, Ilosc = 2, Cena = 299.00m},
                    new ZamowienieProdukt { IdZamowienie = 2, IdProdukt = 2, Ilosc = 1, Cena = 3199.00m }
                };
                _context.ZamowienieProdukty.AddRange(zamowienieProdukty);
                _context.SaveChanges();
            }
        }
    }
}