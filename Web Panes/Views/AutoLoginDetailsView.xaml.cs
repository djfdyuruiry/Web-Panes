using System.Windows;
using System.Windows.Controls;

namespace WebPanes.Views
{
    /// <summary>
    /// Interaction logic for AutoLoginDetailsView.xaml
    /// </summary>
    public partial class AutoLoginDetailsView
    {
        public AutoLoginDetailsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Workaround for being unable to bind to PasswordBox control
        /// Password property out of the box with Caliburn Micro.
        /// 
        /// Assigns the Password property value for the "PasswordBox" control 
        /// to the "Password" control's Text property.
        /// </summary>
        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs args)
        {
            var passwordBox = FindName("PasswordBox") as PasswordBox;
            var hiddenPasswordField = FindName("Password") as TextBox;

            var newPassword = passwordBox?.Password ?? string.Empty;

            if (hiddenPasswordField == null)
            {
                return;
            }

            hiddenPasswordField.Text = newPassword;
        }
    }
}
