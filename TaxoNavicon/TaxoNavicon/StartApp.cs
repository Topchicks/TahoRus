using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxoNavicon
{
    public partial class StartApp : Form
    {
        public StartApp()
        {
            InitializeComponent();
        }
        private void buttonOpenEuropeanTypeForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            EuropeanTypeForm europeanTypeForm = new EuropeanTypeForm();

            europeanTypeForm.ShowDialog();
            
        }

        private void buttonOpenRussianPanel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RussianTypeForm russianTypeForm = new RussianTypeForm();

            russianTypeForm.ShowDialog();
        }
    }
}
