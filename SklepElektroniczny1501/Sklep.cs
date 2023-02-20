using SklepElektroniczny;
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
    public partial class Sklep : Form
    {
        public Sklep()
        {
            InitializeComponent();
            button1.Click += new EventHandler(button1_Click);
            button2.Click += new EventHandler(button2_Click);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produkty form2 = new Produkty();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Zamowienia form3 = new Zamowienia();
            form3.Show();
        }

    }
}
