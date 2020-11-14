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

            foreach (KeyValuePair<string,int> pair in hashtags)
            {
                txtbox_Hashtags.Text += pair.Key + "(" + pair.Value + ")\n";
            }

            /*
            txtbox_Hashtags.Text = messageService.;
            txtbox_Mentions.Text;
            txtbox_SIR.Text;
            txtbox_URL.Text;*/
        }
    }
}
