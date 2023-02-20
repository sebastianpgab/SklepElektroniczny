using Sklep;
using SklepElektroniczny1501.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var db = new SklepDbContext())
            {
                var seeder = new SklepSeeder(db);
                seeder.Seed();
            }
            Application.Run(new Sklep());


        }
    }
}
