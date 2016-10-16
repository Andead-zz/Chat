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

        public ChatForm(IServiceClient client)
        {
            Client = client;

            client.MessageReceived += ClientOnMessageReceived;

            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Client.MessageReceived -= ClientOnMessageReceived;

            base.OnClosing(e);
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