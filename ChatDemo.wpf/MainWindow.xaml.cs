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

namespace ChatDemo.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChatClient _chatClient;

        public MainWindow()
        {
            InitializeComponent();
            _chatClient = new ChatClient();
            InitializeConnection();
        }

        private async void InitializeConnection()
        {
            await _chatClient.StartConnectionAsync();
        }

        private async void JoinGroupButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text;
            await _chatClient.JoinGroupAsync(groupName);
        }

        private async void LeaveGroupButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text;
            await _chatClient.LeaveGroupAsync(groupName);
        }

        private async void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string groupName = GroupNameTextBox.Text;
            string message = MessageTextBox.Text;
            await _chatClient.SendMessageToGroupAsync(groupName, message);
        }
    }

}
