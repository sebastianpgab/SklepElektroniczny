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
    public partial class ZamowieniaEdycja : Form
    {
        private readonly SklepDbContext dbContext;
        public ZamowieniaEdycja()
        {
            dbContext = new SklepDbContext();
            InitializeComponent();
            this.buttonDodajNoweZamowienie.Click += new System.EventHandler(this.buttonDodaj_Click);
            dataGridView1.CellContentClick += new DataGridViewCellEventHandler(edytujButton_Click);
            this.buttonZapisz.Click += new System.EventHandler(this.buttonZapisz_Click);
            this.buttonZapisz.Click += new System.EventHandler(this.edytujButton_Click);
            this.buttonSzukaj.Click += new System.EventHandler(this.textBoxSzukaj_Click);
            this.buttonUsun.Click += new System.EventHandler(this.textBoxUsun_Click); 

            DataGridViewTextBoxColumn numerZamowieniaColumn = new DataGridViewTextBoxColumn();
            numerZamowieniaColumn.HeaderText = "Numer zamówienia";
            numerZamowieniaColumn.Name = "numerZamowienia";
            DataGridViewTextBoxColumn dataZamowieniaColumn = new DataGridViewTextBoxColumn();
            dataZamowieniaColumn.HeaderText = "Data zamówienia";
            dataZamowieniaColumn.Name = "dataZamowienia";
            DataGridViewTextBoxColumn pozycjaZamowieniaColumn = new DataGridViewTextBoxColumn();
            pozycjaZamowieniaColumn.Name = "pozycjaZamowienia";
            pozycjaZamowieniaColumn.HeaderText = "Pozycja zamówienia";

            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(numerZamowieniaColumn);
            dataGridView1.Columns.Add(dataZamowieniaColumn);
            dataGridView1.Columns.Add(pozycjaZamowieniaColumn);

            // Dodaj kolumnę z przyciskiem "Edytuj"
            DataGridViewButtonColumn edytujColumn = new DataGridViewButtonColumn();
            edytujColumn.HeaderText = "";
            edytujColumn.Name = "EdytujButton";
            edytujColumn.Text = "Edytuj";
            edytujColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(edytujColumn);
            ZaladujZamowienia();

        }

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            string pozycjaZamowienia = textBoxSzukajFaktury.Text;

            // Znajdź ostatni numer zamówienia w bazie danych
            var ostatniNumerZamowienia = dbContext.Zamowienia
                .OrderByDescending(p => p.Id)
                .Select(p => p.NumerZamowienia)
                .FirstOrDefault();

             // Wygeneruj nowy unikalny numer zamówienia
             string nowyNumerZamowienia = GenerujNumerZamowienia(ostatniNumerZamowienia);

            // Wygeneruj datę dzisiejszą
             var data = DateTime.Now;

            // Utwórz nowe zamówienie z wygenerowanym numerem i datą
            var zamowienie = new Zamowienie
            {
                NumerZamowienia = nowyNumerZamowienia,
                DataZamowienia = data
            };

            // Dodaj zamówienie do bazy danych i zapisz zmiany
            dbContext.Zamowienia.Add(zamowienie);
            dbContext.SaveChanges();
            MessageBox.Show("Zamowienie zostało dodane poprawnie.");
        }

        private void ZaladujZamowienia()
        {
            dataGridView1.Rows.Clear();

            var zamowienia = from z in dbContext.Zamowienia
                             select new
                             {
                                 NumerZamowienia = z.NumerZamowienia,
                                 DataZamowienia = z.DataZamowienia,
                                 IdZamowienia = z.Id
                             };

            foreach (var zamowienie in zamowienia)
            {
                dataGridView1.Rows.Add(zamowienie.NumerZamowienia, zamowienie.DataZamowienia, zamowienie.IdZamowienia);
            }
        }

        private void edytujButton_Click(object sender, EventArgs e)
        {
            // Pobierz indeks wybranej pozycji zamówienia
            int index = dataGridView1.CurrentRow.Index;

            // Odczytaj wartości komórek z wybranej pozycji zamówienia
            string numerZamowienia = dataGridView1.Rows[index].Cells["numerZamowienia"].Value.ToString();
            string dataZamowienia = dataGridView1.Rows[index].Cells["dataZamowienia"].Value.ToString();

            // Wczytaj wartości do pól tekstowych
            textBoxNowaFakturaEdycja.Text = numerZamowienia;
            textBoxNowaDataEdycja.Text = dataZamowienia;
        }

        public string GenerujNumerZamowienia(string ostatniNumerZamowienia)
        {
            int ostatniNumer = 0;
            int aktualnyRok = DateTime.Now.Year;

            if (!string.IsNullOrEmpty(ostatniNumerZamowienia))
            {
                string[] podzielonyNumer = ostatniNumerZamowienia.Split('-');
                if (podzielonyNumer.Length == 3 && podzielonyNumer[0] == "ZAM" && int.TryParse(podzielonyNumer[1], out aktualnyRok) && int.TryParse(podzielonyNumer[2], out ostatniNumer))
                {
                    // numer zamówienia jest w poprawnym formacie
                }
                else
                {
                    throw new ArgumentException("Nieprawidłowy format ostatniego numeru zamówienia");
                }
            }

            string nowyNumerZamowienia = (ostatniNumer + 1).ToString("D3");
            string numerZamowienia = $"ZAM-{aktualnyRok}-{nowyNumerZamowienia}";
            return numerZamowienia;
        }

        private void buttonZapisz_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            string numerFakturyStary = dataGridView1.Rows[index].Cells["numerZamowienia"].Value.ToString();

            string numerFaktury = textBoxNowaFakturaEdycja.Text;
            string data = textBoxNowaDataEdycja.Text;
            var zamowienie = dbContext.Zamowienia.FirstOrDefault(p => p.NumerZamowienia == numerFakturyStary);

            if (zamowienie != null)
            {
                zamowienie.NumerZamowienia = string.IsNullOrEmpty(textBoxNowaFakturaEdycja.Text) ? zamowienie.NumerZamowienia : numerFaktury;
                zamowienie.DataZamowienia = string.IsNullOrEmpty(textBoxNowaDataEdycja.Text) ? zamowienie.DataZamowienia : DateTime.Parse(data);
                dbContext.SaveChanges();
                MessageBox.Show("Produkt został zaktualizowany w bazie danych.");
            }
            else
            {
                MessageBox.Show("Nie znaleziono numeru faktury.");
            }
        }

        private void textBoxSzukaj_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSzukajFaktury.Text.Trim();

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
                        var cellValue = row.Cells["numerZamowienia"].Value;
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
        private void textBoxUsun_Click(object sender, EventArgs e)
        {
            var faktura = dbContext.Zamowienia.FirstOrDefault(p => p.NumerZamowienia == textBoxUsun.Text);
            if(faktura == null)
            {
                MessageBox.Show("Nie znaleziono takiej faktury");
                return;
            }
            dbContext.Remove(faktura);
            dbContext.SaveChanges();
            MessageBox.Show("Poprawienie usunięto fakturę");
        }

    }
}
