using SklepElektroniczny1501.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    public partial class ZamowieniePozycjeEdycja : Form
    {
        public ZamowieniePozycjeEdycja()
        {
            InitializeComponent();
            button1.Click += new EventHandler(dodajButton_Click);
            button1.Click += new EventHandler(edytujButton_Click);

            // Utwórz kolumny dla kontrolki DataGridView
            DataGridViewTextBoxColumn produktColumn = new DataGridViewTextBoxColumn();
            produktColumn.HeaderText = "Produkt";
            produktColumn.Name = "nazwaProduktu";
            DataGridViewTextBoxColumn iloscZakupionaColumn = new DataGridViewTextBoxColumn();
            iloscZakupionaColumn.HeaderText = "Ilość zakupiona";
            DataGridViewTextBoxColumn cenaColumn = new DataGridViewTextBoxColumn();
            cenaColumn.HeaderText = "Cena";


            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(produktColumn);
            dataGridView1.Columns.Add(iloscZakupionaColumn);
            dataGridView1.Columns.Add(cenaColumn);

            using (var db = new SklepDbContext())
            {
                var zamowienieProduktow = from zp in db.ZamowienieProdukty
                                          join p in db.Produkty on zp.IdProdukt equals p.Id
                                          select new
                                          {
                                              Produkt = p.Nazwa,
                                              Ilosc = zp.Ilosc,
                                              Cena = zp.Cena
                                          };

                foreach (var row in zamowienieProduktow)
                {
                    dataGridView1.Rows.Add(row.Produkt, row.Ilosc, row.Cena);
                }
                decimal suma = zamowienieProduktow.Sum(zp => zp.Cena);
                labelSuma.Text = suma.ToString();
            }

        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            if (!checkBoxEdit.Checked)
            {
                string nazwa = textBoxNowaNazwa.Text;
                int ilosc = 0;
                decimal cena = 0;


                if (!int.TryParse(textBoxNowaIlosc.Text, out ilosc))
                {
                    MessageBox.Show("Wpisz poprawną ilość.");
                    return;
                }

                if (!decimal.TryParse(textBoxNowaCena.Text, out cena))
                {
                    MessageBox.Show("Wpisz poprawną cenę.");
                    return;
                }

                using (var dbContext = new SklepDbContext())
                {
                    var product = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == nazwa);
                    if(product == null)
                    {
                        MessageBox.Show("Chcesz utworzyć zamowienia z produktem, który nie istnieje!");
                        return;
                    }
                     var productId = product.Id;

                    var zamowienieProdukt = new ZamowienieProdukt
                    {
                       
                        IdProdukt = productId,
                        Cena = cena,
                        Ilosc = ilosc
                    };
                    dbContext.ZamowienieProdukty.Add(zamowienieProdukt);
                    dbContext.SaveChanges();
                    MessageBox.Show("Produkt został dodany do bazy danych. Identyfikator produktu: " + product.Id);
                }
            }
        }

        private void edytujButton_Click(object sender, EventArgs e)
        {
            if (checkBoxEdit.Checked)
            {
                if (string.IsNullOrEmpty(textBoxNowaNazwa.Text))
                {
                    MessageBox.Show("Wpisz nazwę produktu.");
                    return;
                }

                using (var dbContext = new SklepDbContext())
                {
                    var product = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == textBoxEdit.Text);

                    if (product == null)
                    {
                        MessageBox.Show("Produkt o podanej nazwie nie istnieje w bazie danych.");
                        return;
                    }

                    int ilosc = 0;
                    decimal cena = 0;

                    if (!int.TryParse(textBoxNowaIlosc.Text, out ilosc))
                    {
                        MessageBox.Show("Wpisz poprawną ilość.");
                        return;
                    }

                    if (!decimal.TryParse(textBoxNowaCena.Text, out cena))
                    {
                        MessageBox.Show("Wpisz poprawną cenę.");
                        return;
                    }
                    product.Nazwa = textBoxNowaNazwa.Text;
                    product.Cena = cena;
                    product.IloscDostepna = ilosc;

                    dbContext.SaveChanges();

                    MessageBox.Show("Produkt został zaktualizowany w bazie danych. Identyfikator produktu: " + product.Id);
                }
            }
        }

    }
}
