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
            DataGridViewTextBoxColumn numerZamowieniaColumn = new DataGridViewTextBoxColumn();
            numerZamowieniaColumn.HeaderText = "Numer zamówienia";
            DataGridViewTextBoxColumn sumaSprzedazyColumn = new DataGridViewTextBoxColumn();
            sumaSprzedazyColumn.HeaderText = "Suma Sprzedaży";
            DataGridViewTextBoxColumn dataZamowieniaColumn = new DataGridViewTextBoxColumn();
            dataZamowieniaColumn.HeaderText = "Data zamówienia";

            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(numerZamowieniaColumn);
            dataGridView1.Columns.Add(sumaSprzedazyColumn);
            dataGridView1.Columns.Add(dataZamowieniaColumn);

            using (var db = new SklepDbContext())
            {
                var zamowioneProdukty = from zp in db.ZamowienieProdukt
                                      join z in db.Zamowienia on zp.IdZamowienie equals z.Id
                                      select new
                                      {
                                          NumerZamowienia = zp.IdZamowienie,
                                          SumaSprzedazy = zp.Ilosc * zp.Cena,
                                          DataZamowienia = z.DataZamowienia
                                      };

                foreach (var row in zamowioneProdukty)
                {
                    dataGridView1.Rows.Add(row.NumerZamowienia, row.SumaSprzedazy, row.DataZamowienia);
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
