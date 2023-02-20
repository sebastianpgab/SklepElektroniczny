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
        public ZamowieniaEdycja()
        {
            InitializeComponent();
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Click += new System.EventHandler(this.edytujButton_Click);
            this.button2.Click += new System.EventHandler(this.buttonSearch_Click);
            WyswietlElementyWBazie();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && textBox1.Text != "") { 

            string pozycjaZamowienia = textBox1.Text;

            using (var dbContext = new SklepDbContext())
            {
                // Znajdź ostatni numer zamówienia w bazie danych
                var ostatniNumerZamowienia = dbContext.Zmowienia
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
                    DataZamowienia = data,
                   // Id = int.Parse(pozycjaZamowienia)
                };

                // Dodaj zamówienie do bazy danych i zapisz zmiany
                dbContext.Zmowienia.Add(zamowienie);
                dbContext.SaveChanges();

                // Ustaw tekst w groupBox1 na nowy numer zamówienia
                listBox1.Items.Add(nowyNumerZamowienia);

                // Ustaw tekst w groupBox2 na dzisiejszą datę
                listBox2.Items.Add(data.ToShortDateString());

                // Ustaw tekst w groupBox3 na wartość pobraną z textBox1
                listBox3.Items.Add(pozycjaZamowienia);

                MessageBox.Show("Zamówienie zostało dodane do bazy danych. Identyfikator zamówienia: " + zamowienie.Id);
                }
            }
        }

        private void WyswietlElementyWBazie()
        {
            using (var dbContext = new SklepDbContext())
            {
                // Pobierz listę elementów z bazy danych
                var listaElementow = dbContext.Zmowienia.ToList();

                // Wyczyść kontrolkę ListBox
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                listBox3.Items.Clear();

                // Dodaj każdy element z listy do kontrolki ListBox
                foreach (var element in listaElementow)
                {
                    listBox1.Items.Add(element.NumerZamowienia);
                    listBox2.Items.Add(element.DataZamowienia);
                    listBox3.Items.Add(element.Id);
                }
            }
        }

        private void edytujButton_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                using (var dbContext = new SklepDbContext())
                {
                    string numer_faktury = textBoxNrFaktury.Text.ToString();
                    var zamowienie = dbContext.Zmowienia.FirstOrDefault(p => p.NumerZamowienia == numer_faktury);

                    if (zamowienie != null)
                    {
                        zamowienie.NumerZamowienia = string.IsNullOrEmpty(textBoxNowyNumerFaktury.Text) ? zamowienie.NumerZamowienia : textBoxNowyNumerFaktury.Text;
                        zamowienie.DataZamowienia = string.IsNullOrEmpty(textBoxData.Text) ? zamowienie.DataZamowienia : DateTime.Parse(textBoxData.Text);
                        dbContext.SaveChanges();
                        MessageBox.Show("Produkt został zaktualizowany w bazie danych.");

                    }
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(textBoxSearch.Text != "")
            {


            listBox1.BeginUpdate(); // Zablokuj odświeżanie ListBox, aby uniknąć migotania
            listBox2.BeginUpdate();
            listBox3.BeginUpdate();

            string searchQuery = textBoxSearch.Text.ToLower(); // Pobierz tekst z pola wyszukiwania
            List<object> searchResults = new List<object>();
            foreach (var item in listBox1.Items)
            {
                string itemText = item.ToString().ToLower();
                if (itemText.Contains(searchQuery))
                {
                    searchResults.Insert(0, item); // Dodaj element na początek listy wyników
                }
                else
                {
                    searchResults.Add(item); // Dodaj element na koniec listy wyników
                }
            }

            listBox1.DataSource = null; // Wyłącz źródło danych, aby zmienić elementy
            listBox1.DataSource = searchResults; // Ustaw nową listę jako źródło danych
            listBox1.EndUpdate(); // Odblokuj odświeżanie ListBox
            }
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

    }
}
