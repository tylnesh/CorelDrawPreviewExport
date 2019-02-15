using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using VGCore;

namespace CorelDrawPreviewExport
{

    /// <summary>
    /// Interaction logic for Button MSMX C
    /// </summary>
    public partial class ButtonMSMXC : UserControl
    {
        ExportForm ex;

        public ButtonMSMXC()
        {
            InitializeComponent();
        }

        public ButtonMSMXC(object app)
        {
            InitializeComponent();
            ex = new ExportForm((VGCore.Application)app);
            
        }

        public void ButtonMSMXC_Click(object sender, RoutedEventArgs e)
        {
            ex.ShowDialog();
        }


    }
}


