using System;
using System.Windows.Forms;
using Andead.Chat.Client.Entities;
using Andead.Chat.Client.WinForms.Interfaces;
using Andead.Chat.Client.WinForms.Services;

namespace Andead.Chat.Client.WinForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(Object sender, EventArgs e)
        {
            signInButton.Enabled = ((TextBox) sender).Text.Length > 0;
        }

        private async void signInButton_Click(Object sender, EventArgs e)
        {
            var client = new ServiceClient(new ApplicationSettingsConnectionConfigurationProvider(),
                new WcfChatServiceFactory());

            string name = nameTextBox.Text;
            SignInResult result = await client.SignInAsync(name);

            if (!result.Success)
            {
                MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var chatForm = new ChatForm(client);
            chatForm.Closed += ChatFormOnClosed;
            chatForm.Show();

            Hide();
        }

        private async void ChatFormOnClosed(object sender, EventArgs eventArgs)
        {
            var chatForm = (ChatForm) sender;

            if (chatForm.Client.SignedIn)
            {
                await chatForm.Client.SignOutAsync();
                chatForm.Client.Dispose();
            }

            Show();
        }
    }
}