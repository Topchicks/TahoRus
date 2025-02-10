using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxoNavicon
{
    public partial class Sticker : Form
    {
        private PrintDocument printDocument;
        public Sticker()
        {
            InitializeComponent();
        }

        //Наклейка
        private void toolStripButtonPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        // Сертификат
        private void toolStripButtonCertificate_Click(object sender, EventArgs e)
        {

        }
    }
}
