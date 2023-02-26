using SklepElektroniczny1501.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    public partial class ProduktyEdycja : Form
    {
        private readonly SklepDbContext dbContext;
        public ProduktyEdycja()
        {
            dbContext = new SklepDbContext();
            InitializeComponent();
            button1.Click += new EventHandler(dodajButton_Click);
            button1.Click += new EventHandler(edytujButton_Click);
        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                string nazwatextBox = textBoxNazwa.Text;
                var nazwaProduktu = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == nazwatextBox);
                if (nazwaProduktu != null)
                {
                    MessageBox.Show("Taki produkt już istnieje, dodaj produkt o innej nazwie.");
                    return;
                }
                string nazwa = textBoxNazwa.Text;
                string model = textBoxModel.Text;
                string opis = textBoxOpis.Text;
                int ilosc = string.IsNullOrEmpty(textBoxIlosc.Text) ? 0 : int.Parse(textBoxIlosc.Text);
                decimal cena = string.IsNullOrEmpty(textBoxCena.Text) ? 0 : decimal.Parse(textBoxCena.Text);
                var produkt = new Produkt { Nazwa = nazwa, Model = model, Opis = opis, IloscDostepna = ilosc, Cena = cena };
                dbContext.Add(produkt);
                dbContext.SaveChanges();
                MessageBox.Show("Produkt został dodany do bazy danych. Identyfikator produktu: " + produkt.Id);
            }
        }

        private void edytujButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !string.IsNullOrEmpty(textBoxNazwaEdycja.Text))
            {
                string nazwa = textBoxNazwa.Text;
                var product = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == nazwa);
                if (product != null)
                {
                    MessageBox.Show("Taki produkt już istnieje, dodaj produkt o innej nazwie.");
                    return;
                }
                else
                {
                    product.Nazwa = string.IsNullOrEmpty(textBoxNazwa.Text) ? product.Nazwa : textBoxNazwa.Text;
                    product.Model = string.IsNullOrEmpty(textBoxModel.Text) ? product.Model : textBoxModel.Text;
                    product.Opis = string.IsNullOrEmpty(textBoxOpis.Text) ? product.Opis : textBoxOpis.Text;
                    product.IloscDostepna = string.IsNullOrEmpty(textBoxIlosc.Text) ? product.IloscDostepna : int.Parse(textBoxIlosc.Text);
                    product.Cena = string.IsNullOrEmpty(textBoxCena.Text) ? product.Cena : decimal.Parse(textBoxCena.Text);
                    dbContext.SaveChanges();
                    MessageBox.Show("Produkt został zaktualizowany w bazie danych.");
                }
            }
            else
            {
                MessageBox.Show("Podaj nazwę produktu, którą chcesz edytować.");
            }
        }
    }
}
