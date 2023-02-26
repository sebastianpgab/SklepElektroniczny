using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Entities
{
    public class SklepDbContext : DbContext
    {
        private string _connectionString = "Server=SEBASTIANPGAB\\SQLEXPRESS; Database=SklepDb; Trusted_Connection=True";
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<ZamowienieProdukt> ZamowienieProdukt { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<ProduktKategoria> ProduktKatergoria { get; set; }
        public DbSet<Kategoria> Kategorie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
