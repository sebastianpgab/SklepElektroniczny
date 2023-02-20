using SklepElektroniczny1501;
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

namespace SklepElektroniczny
{
    public partial class Zamowienia : Form
    {
        public Zamowienia()
        {
            InitializeComponent();

            // Utwórz kolumny dla kontrolki DataGridView
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.HeaderText = "Id";
            DataGridViewTextBoxColumn numerZamowieniaColumn = new DataGridViewTextBoxColumn();
            numerZamowieniaColumn.HeaderText = "Numer zamówienia";
            DataGridViewTextBoxColumn sumaSprzedazyColumn = new DataGridViewTextBoxColumn();
            sumaSprzedazyColumn.HeaderText = "Suma Sprzedaży";
            DataGridViewTextBoxColumn dataZamowieniaColumn = new DataGridViewTextBoxColumn();
            dataZamowieniaColumn.HeaderText = "Data zamówienia)";

            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(idColumn);
            dataGridView1.Columns.Add(numerZamowieniaColumn);
            dataGridView1.Columns.Add(sumaSprzedazyColumn);
            dataGridView1.Columns.Add(dataZamowieniaColumn);

            using (var db = new SklepDbContext())
            {
                var query = db.Zmowienia
                    .Include(p => p.ZamowienieProdukty)
                    .Select(zamowienie => new
                    {
                        Id = zamowienie.Id,
                        NumerZamowienia = zamowienie.NumerZamowienia,
                        SumaSprzedazy = zamowienie.ZamowienieProdukty.Sum(zamowienieProdukt => zamowienieProdukt.Cena * zamowienieProdukt.Ilosc),
                        DataZamowienia = zamowienie.DataZamowienia
                    });

                foreach (var row in query)
                {
                    dataGridView1.Rows.Add(row.Id, row.NumerZamowienia, row.SumaSprzedazy, row.DataZamowienia);
                }
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ZamowieniaEdycja form = new ZamowieniaEdycja();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZamowieniePozycjeEdycja form = new ZamowieniePozycjeEdycja();
            form.Show();
        }
    }
}
