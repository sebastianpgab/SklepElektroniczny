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
        private readonly SklepDbContext dbContext;
        public ZamowieniePozycjeEdycja()
        {
            dbContext = new SklepDbContext();

            InitializeComponent();
            buttonPokazDodaj.Click += new EventHandler(dodajButton_Click);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(edytujButton_Click);
            buttonZapisz.Click += new EventHandler(buttonZapisz_Click);
            buttonUsun.Click += new EventHandler(buttonUsun_Click);
            buttonSzukaj.Click += new EventHandler(textBoxSzukaj_Click);


            DataGridViewTextBoxColumn produktColumn = new DataGridViewTextBoxColumn();
            produktColumn.HeaderText = "Produkt";
            produktColumn.Name = "nazwaProduktu";
            DataGridViewTextBoxColumn iloscZakupionaColumn = new DataGridViewTextBoxColumn();
            iloscZakupionaColumn.Name = "iloscZakupiona";
            iloscZakupionaColumn.HeaderText = "Ilość zakupiona";
            DataGridViewTextBoxColumn cenaColumn = new DataGridViewTextBoxColumn();
            cenaColumn.Name = "cena";
            cenaColumn.HeaderText = "Cena";

            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(produktColumn);
            dataGridView1.Columns.Add(iloscZakupionaColumn);
            dataGridView1.Columns.Add(cenaColumn);

            // Dodaj kolumnę z przyciskiem "Edytuj"
            DataGridViewButtonColumn edytujColumn = new DataGridViewButtonColumn();
            edytujColumn.HeaderText = "";
            edytujColumn.Name = "EdytujButton";
            edytujColumn.Text = "Edytuj";
            edytujColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(edytujColumn);

            var zamowienieProduktow = from zp in dbContext.ZamowienieProdukt
                                        join p in dbContext.Produkty on zp.IdProdukt equals p.Id
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

        private void edytujButton_Click(object sender, DataGridViewCellEventArgs e)
        {
            PokazKontrolkiZamowieniePozycjaEdycja();
            // Pobierz indeks wybranej pozycji zamówienia
            int index = dataGridView1.CurrentRow.Index;

            // Odczytaj wartości komórek z wybranej pozycji zamówienia
            string nazwaProduktu = dataGridView1.Rows[index].Cells["nazwaProduktu"].Value.ToString();
            string iloscZakupiona = dataGridView1.Rows[index].Cells["iloscZakupiona"].Value.ToString();
            string cenaProduktu = dataGridView1.Rows[index].Cells["cena"].Value.ToString();

            // Wczytaj wartości do pól tekstowych
            textBoxNowaNazwa.Text = nazwaProduktu;
            textBoxNowaIlosc.Text = iloscZakupiona;
            textBoxNowaCena.Text = cenaProduktu;
            buttonPokazDodaj.Enabled = false;

        }

        private void dodajButton_Click(object sender, EventArgs e)
        {
            PokazKontrolkiZamowieniePozycjaEdycja();
            buttonPokazDodaj.Enabled = true;
        }

        private void PokazKontrolkiZamowieniePozycjaEdycja()
        {
            textBoxNowaNazwa.Visible = true;
            textBoxNowaCena.Visible = true;
            textBoxNowaIlosc.Visible = true;

            labelNazwa.Visible = true;
            labelCena.Visible = true;
            labelIlosc.Visible = true;
        }

        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            if (buttonPokazDodaj.Enabled == true)
            {
                string nazwa = textBoxNowaNazwa.Text;
                int ilosc = 0;
                decimal cena = 0;

                var produkt = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == nazwa);
                if(produkt == null)
                {
                    MessageBox.Show("Chcesz utworzyć zamowienia z produktem, który nie istnieje!");
                    return;
                }

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

                var productId = produkt.Id;

                var zamowienieProduktObject = new ZamowienieProdukt
                {
                    IdProdukt = productId,
                    Cena = cena,
                    Ilosc = ilosc
                };
                dbContext.Add(zamowienieProduktObject);
                dbContext.SaveChanges();

                dbContext.SaveChanges();
                MessageBox.Show("Produkt został dodany do bazy danych.");
            }
            else
            {

                if (string.IsNullOrEmpty(textBoxNowaNazwa.Text))
                {
                    MessageBox.Show("Wpisz nazwę produktu.");
                    return;
                }

                int index = dataGridView1.CurrentRow.Index;
                string nazwaProduktu = dataGridView1.Rows[index].Cells["nazwaProduktu"].Value.ToString();

                var produkt = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == textBoxNowaNazwa.Text);
                var zamowienieProdukt = dbContext.ZamowienieProdukt.FirstOrDefault(p => p.IdProdukt == produkt.Id);

                if (produkt == null)
                {
                    MessageBox.Show("Produkt o podanej nazwie nie istnieje w bazie danych.");
                    return;
                }

                int ilosc = string.IsNullOrEmpty(textBoxNowaIlosc.Text) ? zamowienieProdukt.Ilosc : int.Parse(textBoxNowaIlosc.Text);
                decimal cena = string.IsNullOrEmpty(textBoxNowaCena.Text) ? zamowienieProdukt.Cena : decimal.Parse(textBoxNowaCena.Text);

                //if (!int.TryParse(textBoxNowaIlosc.Text, out ilosc))
                //{
                //    MessageBox.Show("Wpisz poprawną ilość.");
                //    return;
                //}

                //if (!decimal.TryParse(textBoxNowaCena.Text, out cena))
                //{
                //    MessageBox.Show("Wpisz poprawną cenę.");
                //    return;
                //}

                // produkt.Id = zamowienieProdukt.Id;
                zamowienieProdukt.Cena = cena;
                zamowienieProdukt.Ilosc = ilosc;
                buttonPokazDodaj.Enabled = true;

                dbContext.SaveChanges();
                MessageBox.Show("Produkt został edytowany w bazie.");
            }
        }
        private void buttonUsun_Click(object sender, EventArgs e)
        {
            var product = dbContext.Produkty.FirstOrDefault(p => p.Nazwa == textBoxUsun.Text);
            if(product == null)
            {
                MessageBox.Show("Nie ma takiego produktu.");
                return;
            }
            var zamowienieProduktu = dbContext.ZamowienieProdukt.FirstOrDefault(p => p.IdProdukt == product.Id);
            dbContext.Remove(zamowienieProduktu);
            dbContext.SaveChanges();
            MessageBox.Show("Produkt został poprawnie usunięty.");
        }
        private void textBoxSzukaj_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSzukaj.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                MessageBox.Show("Nie ma takiego produktu.");
            }
            else
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    List<DataGridViewRow> matchingRows = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var cellValue = row.Cells["nazwaProduktu"].Value;
                        if (cellValue != null && cellValue.ToString().Contains(searchValue))
                        {
                            matchingRows.Add(row);
                        }
                    }

                    dataGridView1.Rows.Clear();

                    foreach (DataGridViewRow row in matchingRows)
                    {
                        dataGridView1.Rows.Insert(0, row);
                    }

                    if (dataGridView1.Rows.Count > 0)
                    {
                        dataGridView1.Rows[0].Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
