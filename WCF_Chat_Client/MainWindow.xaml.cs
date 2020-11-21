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
using WCF_Chat_Client.ServiceChat;

namespace WCF_Chat_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , IServiceChatCallback
    {
        bool isConnected = false;

        ServiceChatClient client;

        int ID;


        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                tbMessage.IsEnabled = true;
                bConDiscon.Content = "Disconnect";
                isConnected = true;
            }
        }
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                tbMessage.IsEnabled = false;
                bConDiscon.Content = "Connect";
                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else 
            {
                ConnectUser();
            }
        }

        public void MessageCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }         
            }
        }

        private void tbUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text == "User Name")     
                tbUserName.Clear();
            tbUserName.Foreground = Brushes.Black;
        }

        private void tbMessage_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tbUserName.Text == "Write a message...")
                tbUserName.Clear();
            tbUserName.Foreground = Brushes.Black;
        }
    }
}
