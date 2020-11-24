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
        private MessageFacade messageService = new MessageFacade(); // Creates instance of MessageFacade

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Confirm_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)chkBox_Import.IsChecked && // Checks if the import checkbox is checked and if the method LoadMessages returns true
                messageService.LoadMessages(txtbox_File_Name.Text))
            {
                Display display = new Display(); // Creates new window
                display.Show(); // Displays new window
                this.Close(); // Closes current window
            }
            else if (messageService.AddMessage(txtbox_Message_Header.Text, txtbox_Message_Body.Text))
                MessageBox.Show("Message Added"); // Confirms message added
            else
                MessageBox.Show("Error: Try again"); // Returns error messagee

        }

        private void chkBox_Import_Checked(object sender, RoutedEventArgs e) // If import checkbox is cheecked
        {
            txtbox_File_Name.Visibility = Visibility.Visible; // Makes the filename textbox visable
            lbl_File_Name.Visibility = Visibility.Visible; // Makes the filename label visable
            btn_Confirm.Content = "Import Data"; //changes confirm button to display import data
        }
        private void chkBox_Import_Unchecked(object sender, RoutedEventArgs e)
        {
            txtbox_File_Name.Visibility = Visibility.Collapsed; // Makes the filename textbox invisable
            lbl_File_Name.Visibility = Visibility.Collapsed; // Makes the filename label invisable
            txtbox_File_Name.Clear(); // Removes data from the textbox
            btn_Confirm.Content = "Add Message"; //changes confirm button to display add message
        }

        private void btn_MessagView_Click(object sender, RoutedEventArgs e)
        {

            Display display = new Display(); // Creates new window
            display.Show(); //Displays new window
            this.Close(); // Closes current window

        }
    }
}
