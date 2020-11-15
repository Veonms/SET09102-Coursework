using System.Collections.Generic;
using System.Windows;
using BusinessLayer;

namespace SET09102_Coursework
{
    /// <summary>
    /// Interaction logic for DisplayLists.xaml
    /// </summary>
    public partial class DisplayLists : Window
    {
        private MessageFacade messageService = new MessageFacade();
        public DisplayLists()
        {
            InitializeComponent();

            Dictionary<string, int> hashtags = messageService.GetHashtagList();
            List<string> mentions = messageService.GetMentionsList();
            List<string> SIR = messageService.GetSIRList();
            List<string> URLs = messageService.GetURLList();

            foreach (KeyValuePair<string,int> pair in hashtags)
            {
                txtbox_Hashtags.Text += pair.Key + "(" + pair.Value + ")\n";
            }
            foreach (string s in mentions)
            {
                txtbox_Mentions.Text += s + "\n";
            }
            foreach (string s in SIR)
            {
                txtbox_SIR.Text += s + "\n";
            }
            foreach (string s in URLs)
            {
                txtbox_URL.Text += s + "\n";
            }
        }
    }
}
