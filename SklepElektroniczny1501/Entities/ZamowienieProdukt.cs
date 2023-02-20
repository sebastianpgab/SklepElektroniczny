using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SklepElektroniczny1501.Entities
{
public class ZamowienieProdukt
{
    public int Id { get; set; }
    public int IdProdukt { get; set; }
    public int IdZamowienie { get; set; }
    public int Ilosc { get; set; }
    public decimal Cena { get; set; }
    public virtual Zamowienie Zamowienie { get; set; }
    public virtual Produkt Produkt { get; set; }
}
}
