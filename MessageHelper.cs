using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProgPart2
{
    public class MessageHelper
    {
        private readonly StackPanel chatPanel;
        private readonly ScrollViewer scrollViewer;

        public MessageHelper(StackPanel chatPanel, ScrollViewer scrollViewer)
        {
            this.chatPanel = chatPanel ?? throw new ArgumentNullException(nameof(chatPanel));
            this.scrollViewer = scrollViewer ?? throw new ArgumentNullException(nameof(scrollViewer));
        }

        public void AddUserMessage(string message)
        {
            if (chatPanel == null || scrollViewer == null) return;

            var border = CreateMessageBubble(message,
                new SolidColorBrush(Color.FromRgb(0, 122, 255)), // Blue
                HorizontalAlignment.Right);
            chatPanel.Children.Add(border);
            scrollViewer.ScrollToEnd();
        }

        public void AddBotMessage(string message, Color color = default)
        {
            if (chatPanel == null || scrollViewer == null) return;

            if (color == default) color = Colors.LightBlue;   // ← Changed from White

            var border = CreateMessageBubble(message,
                new SolidColorBrush(color),
                HorizontalAlignment.Left);
            chatPanel.Children.Add(border);
            scrollViewer.ScrollToEnd();
        }

        private Border CreateMessageBubble(string message, SolidColorBrush brush, HorizontalAlignment alignment)
        {
            var textBlock = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 15,
                Foreground = Brushes.White,        // Always white text
                TextAlignment = TextAlignment.Left
            };

            return new Border
            {
                Background = brush,
                CornerRadius = new CornerRadius(18),
                Margin = new Thickness(10, 5, 10, 5),
                Padding = new Thickness(15),
                HorizontalAlignment = alignment,
                MaxWidth = 550,
                Child = textBlock
            };
        }
    }
}