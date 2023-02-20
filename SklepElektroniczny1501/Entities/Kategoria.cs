using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Entities
{
    public class Kategoria
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public virtual List<ProduktKategoria> KategorieProduktu { get; set; }
    }
}
