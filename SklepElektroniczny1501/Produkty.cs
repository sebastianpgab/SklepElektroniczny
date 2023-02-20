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
    public partial class Produkty : Form
    {
        public Produkty()
        {
            InitializeComponent();


            // Utwórz kolumny dla kontrolki DataGridView
            DataGridViewTextBoxColumn nazwaColumn = new DataGridViewTextBoxColumn();
            nazwaColumn.Name = "nazwaColumn";
            nazwaColumn.HeaderText = "Nazwa";
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            modelColumn.HeaderText = "Model";
            DataGridViewTextBoxColumn opisColumn = new DataGridViewTextBoxColumn();
            opisColumn.HeaderText = "Opis";
            DataGridViewTextBoxColumn iloscColumn = new DataGridViewTextBoxColumn();
            iloscColumn.HeaderText = "Ilość dostępna";
            DataGridViewTextBoxColumn cenaColumn = new DataGridViewTextBoxColumn();
            cenaColumn.HeaderText = "Cena";

            // Dodaj kolumny do kontrolki DataGridView
            dataGridView1.Columns.Add(nazwaColumn);
            dataGridView1.Columns.Add(modelColumn);
            dataGridView1.Columns.Add(opisColumn);
            dataGridView1.Columns.Add(iloscColumn);
            dataGridView1.Columns.Add(cenaColumn);

            using (var db = new SklepDbContext())
            {
                var produkty = db.Produkty.ToList();
                foreach (var produkt in produkty)
                {
                    dataGridView1.Rows.Add(produkt.Nazwa, produkt.Model, produkt.Opis, produkt.IloscDostepna, produkt.Cena);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProduktyEdycja form2 = new ProduktyEdycja();
            form2.Show();
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchValue))
            {
                using (var db = new SklepDbContext())
                {
                    var produkty = db.Produkty.ToList();
                    dataGridView1.Rows.Clear();
                    foreach (var produkt in produkty)
                    {
                        dataGridView1.Rows.Add(produkt.Id, produkt.Nazwa, produkt.Model, produkt.Opis, produkt.IloscDostepna, produkt.Cena);
                    }
                }
            }
            else
            {
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                try
                {
                    List<DataGridViewRow> matchingRows = new List<DataGridViewRow>();
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        var cellValue = row.Cells["nazwaColumn"].Value;
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
