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
        public ProduktyEdycja()
        {
            InitializeComponent();
            button1.Click += new EventHandler(dodajButton_Click);
            button1.Click += new EventHandler(edytujButton_Click);


        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                string nazwa = textBoxNazwa.Text;
                string model = textBoxModel.Text;
                string opis = textBoxOpis.Text;
                int ilosc = string.IsNullOrEmpty(textBoxIlosc.Text) ? 0 : int.Parse(textBoxIlosc.Text);
                decimal cena = string.IsNullOrEmpty(textBoxCena.Text) ? 0 : decimal.Parse(textBoxCena.Text);
                using (var dbContext = new SklepDbContext())
                {
                    var product = new Produkt { Nazwa = nazwa, Model = model, Opis = opis, IloscDostepna = ilosc, Cena = cena };
                    dbContext.Add(product);
                    dbContext.SaveChanges();
                    MessageBox.Show("Produkt został dodany do bazy danych. Identyfikator produktu: " + product.Id);
                }
            }
        }

        private void edytujButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && !string.IsNullOrEmpty(textBoxId.Text))
            {
                using (var dbContext = new SklepDbContext())
                {
                    int id = int.Parse(textBoxId.Text);
                    var product = dbContext.Produkty.FirstOrDefault(p => p.Id == id);

                    if (product != null)
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
            }
        }

    }
}
