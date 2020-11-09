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
using BusinessLayer;

namespace SET09102_Coursework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MessageFacade messageService = new MessageFacade();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (!(String.IsNullOrEmpty(txtbox_File_Name.Text)))
            {
                Boolean success = messageService.Load(txtbox_File_Name.Text);
                if (success)
                {
                    Display display = new Display();
                    display.Show();
                    this.Close();
                }
            }
            else
            {
                if (messageService.AddMessage(txtbox_Message_Header.Text, txtbox_Message_Body.Text))
                {
                    MessageBox.Show("Works");
                }
                else
                {

                }
            }

        }

        private void chkBox_Import_Checked(object sender, RoutedEventArgs e)
        {
            txtbox_File_Name.Visibility = Visibility.Visible;
            lbl_File_Name.Visibility = Visibility.Visible;
            btn_Confirm.Content = "Import Data";
        }
        private void chkBox_Import_Unchecked(object sender, RoutedEventArgs e)
        {
            txtbox_File_Name.Visibility = Visibility.Collapsed;
            lbl_File_Name.Visibility = Visibility.Collapsed;
            txtbox_File_Name.Clear();
            btn_Confirm.Content = "Add Message";
        }

        private void btn_MessagView_Click(object sender, RoutedEventArgs e)
        {

            DisplayLists displayLists = new DisplayLists();
            displayLists.Show();
            this.Close();

        }
    }
}
