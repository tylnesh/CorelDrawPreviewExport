using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VGCore;

namespace CorelDrawPreviewExport
{
    public partial class ExportForm : Form
    {
        VGCore.Application appDraw;
        public ExportForm(VGCore.Application app)
        {
            this.appDraw = app;
            InitializeComponent();
        }

        private void exportButton_Click_1(object sender, EventArgs e)
        {
            CorelDrawPreviewExport.CorelDrawExporter exp = new CorelDrawPreviewExport.CorelDrawExporter(this.appDraw);
            exp.ExportMSMXBackground(this.variantBox.Text, this.numberBox.Text, this.nameBox.Text);
            Close();
        }

        private void ExportForm_Load_1(object sender, EventArgs e)
        {

            this.variantBox.Text = this.appDraw.ActiveDocument.FileName.ToString().Substring(0, 6) + " -";

            System.Drawing.Image i = System.Drawing.Image.FromFile("C:\\CorelDrawPreviewExport\\CorelDrawPreviewExport\\assets\\logo.jpg"); // read in image
            logo.Size = new Size(i.Width, i.Height); //set label to correct size
            logo.Image = i; // put image on label



        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

    
    }
}
