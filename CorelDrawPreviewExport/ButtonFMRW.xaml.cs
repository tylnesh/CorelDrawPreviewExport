﻿using System;
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
    /// Interaction logic for Button FMR All
    /// </summary>
    public partial class ButtonFMRW : UserControl
    {
        CorelDrawExporter cde;



        public ButtonFMRW()
        {
            InitializeComponent();
        }

        public ButtonFMRW(object app)
        {
            InitializeComponent();
            cde = new CorelDrawExporter(app);

        }

        public void ButtonFMRW_Click(object sender, RoutedEventArgs e)
        {
            cde.ExportFmrWBackground();
        }


    }
}


