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

namespace SET09102_Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbox_File_Name.Text))
            {
                Display display = new Display();
                display.Show();
                this.Close();
            }
            else
            {
                ImportedData impData = new ImportedData();
                impData.Show();
                this.Close();
            }

        }
    }
}
