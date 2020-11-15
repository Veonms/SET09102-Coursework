using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer;

namespace SET09102_Coursework
{
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
                    txtblk_SIGMen.Text = null;
                    txtblk_URLHash.Text = null;
                    lbl_messageType.Content = "SMS Text";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messageService.abbreviations(messages[index + 1]);
                    txtblk_SIGMen.Visibility = Visibility.Hidden;
                    txtblk_URLHash.Visibility = Visibility.Hidden;
                    lbl_URLHash.Visibility = Visibility.Hidden;
                    lbl_SIRMen.Visibility = Visibility.Hidden;
                }
                if (messages[index].StartsWith("E"))
                {
                    txtblk_SIGMen.Text = null;
                    txtblk_URLHash.Text = null;
                    txtblk_SIGMen.Visibility = Visibility.Visible;
                    txtblk_URLHash.Visibility = Visibility.Visible;
                    lbl_URLHash.Visibility = Visibility.Visible;
                    lbl_SIRMen.Visibility = Visibility.Visible;
                    lbl_messageType.Content = "Email";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messageService.checkURL(messages[index + 1]);
                    lbl_URLHash.Content = "URLs Quarantined";
                    lbl_SIRMen.Content = "Significant Incedent Report";
                    txtblk_SIGMen.Text = messageService.GetSIR(messages[index + 1]);
                    foreach (var v in messageService.GetUrl(messages[index + 1]))
                    {
                        txtblk_URLHash.Text += (v + " ");
                    }
                }
                if (messages[index].StartsWith("T"))
                {
                    txtblk_SIGMen.Text = null;
                    txtblk_URLHash.Text = null;
                    txtblk_SIGMen.Visibility = Visibility.Visible;
                    txtblk_URLHash.Visibility = Visibility.Visible;
                    lbl_URLHash.Visibility = Visibility.Visible;
                    lbl_SIRMen.Visibility = Visibility.Visible;
                    lbl_messageType.Content = "Tweet";
                    lbl_messageHeader.Content = messages[index];
                    txtblk_body.Text = messageService.abbreviations(messages[index + 1]);
                    lbl_URLHash.Content = "Hashtags";
                    lbl_SIRMen.Content = "Mentions";
                    foreach (var v in messageService.GetHashtag(messages[index + 1]))
                    {
                        txtblk_URLHash.Text += (v + " ");
                    }
                    foreach (var v in messageService.GetMentions(messages[index + 1]))
                    {
                        txtblk_SIGMen.Text += (v + " ");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception){

            }
        }

        private void btn_Main_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            if (!messageService.SaveMessages())
            {
                MessageBox.Show("Error");
            }
            else
            {
                DisplayLists displayLists = new DisplayLists();
                displayLists.Show();
                this.Close();
            }
        }

        private void btn_NextMssage_Click(object sender, RoutedEventArgs e)
        {
            if (index < messageService.DisplayData().Count-2)
            {
                index += 2;
            }
            else
                index = 0;
            changes();
        }
    }
}
