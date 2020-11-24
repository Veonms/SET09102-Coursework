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
            changes(); // Calls method changes
        }
        
        public void changes()
        {
            try
            {
                List<string> messages = messageService.DisplayData();

                if (messages[index].StartsWith("S")) // Checks if message is an SMS
                {
                    txtblk_SIGMen.Text = null; // Makes sure the textblock is empty
                    txtblk_URLHash.Text = null;// Makes sure the textblock is empty
                    lbl_messageType.Content = "SMS Text"; // Displays SMS Text
                    lbl_messageHeader.Content = messages[index]; // Displays the Heeader of the message
                    txtblk_body.Text = messageService.abbreviations(messages[index + 1]); // Displays the body of the message with abbriviations
                    txtblk_SIGMen.Visibility = Visibility.Hidden; // Hides Textblock
                    txtblk_URLHash.Visibility = Visibility.Hidden; // Hides Textblock
                    lbl_URLHash.Visibility = Visibility.Hidden; // Hides label 
                    lbl_SIRMen.Visibility = Visibility.Hidden; // Hides label
                }
                if (messages[index].StartsWith("E"))
                {
                    txtblk_SIGMen.Text = null; // Makes sure the textblock is empty
                    txtblk_URLHash.Text = null; // Makes sure the textblock is empty
                    txtblk_SIGMen.Visibility = Visibility.Visible; // Makes textblock visable
                    txtblk_URLHash.Visibility = Visibility.Visible;// Makes textblock visable
                    lbl_URLHash.Visibility = Visibility.Visible;// Makes label visable
                    lbl_SIRMen.Visibility = Visibility.Visible;// Makes label visable
                    lbl_messageType.Content = "Email"; // Displays Email
                    lbl_messageHeader.Content = messages[index]; // Displays the Heeader of the message
                    txtblk_body.Text = messageService.checkURL(messages[index + 1]);// Displays the body of the message with URLs removed
                    lbl_URLHash.Content = "URLs Quarantined"; // Displays URLs Quarantines
                    lbl_SIRMen.Content = "Significant Incedent Report"; // Displays Significant Incedent Report
                    txtblk_SIGMen.Text = messageService.GetSIR(messages[index + 1]); // Displays the SIR in the textblock
                    foreach (var v in messageService.GetUrl(messages[index + 1]))
                    {
                        txtblk_URLHash.Text += (v + " "); // 
                    }
                }
                if (messages[index].StartsWith("T")) // Checks if the messagee is a Tweet
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
            MainWindow mainWindow = new MainWindow(); // Creates new window
            mainWindow.Show(); // Displays new window
            this.Close(); // Closes current window
        }

        private void btn_Export_Click(object sender, RoutedEventArgs e)
        {
            if (!messageService.SaveMessages())
            {
                MessageBox.Show("Error"); // Shows error box
            }
            else
            {
                DisplayLists displayLists = new DisplayLists(); // Creates new window
                displayLists.Show(); // Displays new window
                this.Close(); // Closees current window
            }
        }

        private void btn_NextMssage_Click(object sender, RoutedEventArgs e)
        {
            if (index < messageService.DisplayData().Count-2) 
            {
                index += 2; // Cycles through messages
            }
            else
                index = 0; // Goes back to 0 to allow thee user to cycle through messages
            changes(); // Changees the display
        }
    }
}
