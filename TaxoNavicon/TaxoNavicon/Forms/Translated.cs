using Guna.UI2.WinForms;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Windows.Forms;
using TaxoNavicon.Model;

namespace TaxoNavicon.Forms
{
    public partial class Translated : Form
    {
        Translate translate = new Translate();
        public Translated()
        {
            InitializeComponent();

            foreach (KeyValuePair<string, string> pair in translate.translitDict)
            {
                string key = pair.Key;
                string value = pair.Value;

                Guna2Panel panel = new Guna2Panel
                {
                    BorderColor = Color.FromArgb(24, 175, 240),
                    BorderRadius = 4,
                    BorderThickness = 1,
                    FillColor = Color.White,
                    Size = new System.Drawing.Size(368, 35)
                };

                Label newLabel = new Label
                {
                    Font = new Font("Arial", 12f),
                    TextAlign = ContentAlignment.MiddleCenter, // Выравнивание текста по центру
                    BackColor = Color.Transparent,
                };

                newLabel.Text = $"{key} - {value}"; // Например, "ул. - st."
                panel.Controls.Add(newLabel);
                newLabel.Dock = DockStyle.Fill;
                panelTextTranslated.Controls.Add(panel);                         // Добавьте newLabel на форму или в нужный контейнер.
            }
        }
    }
}
