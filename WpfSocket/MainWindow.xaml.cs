using Microsoft.AspNet.SignalR.Client;
using System.Windows;
using System.Windows.Controls;

namespace WpfSocket
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection connection;
        private IHubProxy hubProxy;
        public MainWindow()
        {
            InitializeComponent();

            connection = new HubConnection("http://localhost:60690/");
            hubProxy = connection.CreateHubProxy("WebHub");

            // Define the method to update the output text box
            hubProxy.On<string>("UpdateOutput", UpdateOutputTextBox);

            // Start the connection
            connection.Start().Wait();
        }
        private void UpdateOutputTextBox(string message)
        {
            // Update the output text box with the received message on the UI thread
            Dispatcher.Invoke(() =>
            {
                outputText.Text = message;
            });
        }
        private void InputText_TextChanged(object sender, TextChangedEventArgs e)
        {
           hubProxy.Invoke("SendMessage", inputText.Text);
        }
    }
}
