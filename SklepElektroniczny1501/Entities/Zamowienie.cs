using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Entities
{
    public class Zamowienie
    {
        public int Id { get; set; }
        public string NumerZamowienia { get; set; }
        public DateTime DataZamowienia { get; set; }
        public virtual List<ZamowienieProdukt> ZamowienieProdukty { get; set; }
    }
}
