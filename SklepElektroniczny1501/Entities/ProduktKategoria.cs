using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Entities
{
    public class ProduktKategoria
    {
        public int Id { get; set; }
        public int IdProduktu { get; set; }
        public int IdKategoria { get; set; }
        public virtual Produkt Produkt { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}
