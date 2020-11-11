using System;
using System.Collections;
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
using System.Windows.Shapes;
using BusinessLayer;

namespace SET09102_Coursework
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class Display : Window
    {
        private MessageFacade messageService = new MessageFacade();
        int index = 0;
        public Display()
        {
            InitializeComponent();
            changes();
        }
        
        public void changes()
        {
            try
            {
                List<string> messages = messageService.DisplayData();
                if (messages[index].StartsWith("S"))
                {
                    lbl_messageType.Content = "SMS Text";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messages[index+1];
                    txtblk_SIGMen.Visibility = Visibility.Hidden;
                    txtblk_URLHash.Visibility = Visibility.Hidden;
                    lbl_URLHash.Visibility = Visibility.Hidden;
                    lbl_SIRMen.Visibility = Visibility.Hidden;
                }
                if (messages[index].StartsWith("E"))
                {
                    lbl_messageType.Content = "Email";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messages[index + 1];
                    lbl_URLHash.Content = "URLs Quarantined";
                    lbl_SIRMen.Content = "Significant Incedent Report";
                }
                if (messages[index].StartsWith("T"))
                {
                    lbl_messageType.Content = "Tweet";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messages[index + 1];
                    lbl_URLHash.Content = "Hashtags";
                    lbl_SIRMen.Content = "Mentions";
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception){

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_NextMssage_Click(object sender, RoutedEventArgs e)
        {
            index += 2;
            changes();
        }
    }
}
