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
    public partial class Form1 : Form
    {
        PoleData poleData;
        public Form1()
        {
            InitializeComponent();
            poleData = new PoleData();
        }

        private void ToolStripMenuItemCertificate_Click(object sender, EventArgs e)
        {
            SetData();
            Certificate Certificate = new Certificate(poleData);
            Certificate.Show();
        }

        private void ToolStripMenuItemSticker_Click(object sender, EventArgs e)
        {
            Sticker sticker = new Sticker();

            sticker.Show();
        }
        
        private void SetData()
        {
            poleData.orderNumber = (int)numericUpDowntextBoxOrderNumber.Value;
            poleData.adresCustomer = textBoxAdresCustomer.Text;
        }
    }
}
