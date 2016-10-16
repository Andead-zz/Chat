using System;
using System.ComponentModel;
using System.Windows.Forms;
using Andead.Chat.Client.Entities;
using Andead.Chat.Client.Interfaces;

namespace Andead.Chat.Client.WinForms
{
    public partial class ChatForm : Form
    {
        internal readonly IServiceClient Client;
        private Timer _onlineCountTimer;

        public ChatForm(IServiceClient client)
        {
            Client = client;

            client.MessageReceived += ClientOnMessageReceived;

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _onlineCountTimer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            _onlineCountTimer.Tick += async (sender, args) => UpdateOnlineCount(await Client.GetOnlineCount());
            _onlineCountTimer.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Client.MessageReceived -= ClientOnMessageReceived;

            _onlineCountTimer?.Stop();

            base.OnClosing(e);
        }

        private void UpdateOnlineCount(int? onlineCount)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int?>(UpdateOnlineCount), onlineCount);
                return;
            }

            onlineCountLabel.Text = onlineCount.HasValue ? $"{onlineCount} users online." : null;
        }

        private void ClientOnMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ClientOnMessageReceived(sender, args)));
                return;
            }

            string message = args.Message;
            chatTextBox.AppendText(message + Environment.NewLine);
        }

        private async void button1_Click(Object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                return;
            }

            string message = textBox1.Text;
            textBox1.Clear();

            await Client.SendAsync(message);
        }
    }
}