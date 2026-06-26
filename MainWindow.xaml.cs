using CybersecurityChatBot;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ProgPart2
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotEngine chatbotEngine;
        private readonly AudioPlayer audioPlayer;
        private readonly AsciiArtGenerator asciiArt;
        private MessageHelper messageHelper;

        private readonly TaskManager taskManager;
        private readonly MiniGameManager miniGameManager;   
        private readonly ActivityLogger activityLogger; 

        public MainWindow()
        {
            InitializeComponent();

            audioPlayer = new AudioPlayer();
            asciiArt = new AsciiArtGenerator();
            taskManager = new TaskManager();
            miniGameManager = new MiniGameManager();
            activityLogger = new ActivityLogger();             

            chatbotEngine = new ChatbotEngine(taskManager, miniGameManager, activityLogger);

            Loaded += MainWindow_Loaded;
        }

        private void ActivityLogButton_Click(object sender, RoutedEventArgs e)
        {

            var logs = activityLogger.GetRecentLogs(15);


            Window logWindow = new Window();

            logWindow.Title = "Activity Log";

            logWindow.Width = 500;

            logWindow.Height = 400;

            logWindow.Background =
                new SolidColorBrush(Color.FromRgb(15, 22, 41));




            ListBox list = new ListBox();

            list.Foreground = Brushes.White;

            list.Background =
                new SolidColorBrush(Color.FromRgb(30, 42, 68));



            foreach (string log in logs)
            {
                list.Items.Add(log);
            }



            logWindow.Content = list;


            logWindow.Show();

        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            messageHelper = new MessageHelper(ChatPanel, ChatScrollViewer);

            ShowAsciiLogo();
            await Task.Delay(600);

            string username = GetUserName();
            chatbotEngine.SetUserName(username);

            ShowWelcomeAscii(username);
            await Task.Delay(500);

            messageHelper.AddBotMessage($"Hello {username}! I'm your Cybersecurity Awareness Bot 🛡️", Colors.Cyan);
            messageHelper.AddBotMessage("Ask me about passwords, phishing, tasks, start quiz, or 'show activity log'.", Colors.White);

            // Log startup
            activityLogger.LogAction("System", "Chatbot started successfully");

            InputTextBox.Focus();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SendMessage();
        }

        private void SendMessage()
        {
            string input = InputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            try
            {
                messageHelper.AddUserMessage(input);

                string response = chatbotEngine.GetResponse(input);

                // Debug output
                Console.WriteLine($"User: {input}");
                Console.WriteLine($"Bot: {response}");

                messageHelper.AddBotMessage(response);
                InputTextBox.Clear();
                ChatScrollViewer.ScrollToEnd();
            }
            catch (Exception ex)
            {
                messageHelper?.AddBotMessage($"[ERROR] {ex.Message}", Colors.Red);
                MessageBox.Show($"Error: {ex.Message}\n\n{ex.StackTrace}", "Debug Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Keep your existing helper methods
        private void ShowAsciiLogo()
        {
            string logo = @"CYBER SECURITY BOT LOADING...";
            ChatPanel.Children.Add(new TextBlock
            {
                Text = logo,
                FontFamily = new FontFamily("Consolas"),
                Foreground = Brushes.Cyan,
                FontSize = 14,
                HorizontalAlignment = HorizontalAlignment.Center
            });
        }

        private void ShowWelcomeAscii(string username)
        {
            string ascii = asciiArt.GetWelcomeAsciiText("WELCOME", username);
            ChatPanel.Children.Add(new TextBlock
            {
                Text = ascii,
                FontFamily = new FontFamily("Consolas"),
                FontSize = 11,
                Foreground = Brushes.Cyan,
                TextAlignment = TextAlignment.Center
            });
        }

        private string GetUserName()
        {
            var input = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter your name:", "Welcome", "Friend");
            return string.IsNullOrWhiteSpace(input) ? "Friend" : input.Trim();
        }
    }
}