using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AVATAR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int spacer = 10;
        public string account_file = null;
        public Security security = new Security();
        //public bool is_new_user = false;

        public MainWindow()
        {
            InitializeComponent();
            //RESET
            add_acc.Visibility = Visibility.Hidden;
            scroll_social_media.Visibility = Visibility.Visible;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
            //RESET-END
        }

        public void Load_User_Data(string file)
        {
            account_file = file;
            //TODO
            /*
            string[] temp = File.ReadAllLines(account_file);
            if (temp[2].Contains("newuser")) is_new_user = true;
            if (is_new_user)
            {
                var blur = new BlurEffect();
                blur.Radius = 15;
                main.Effect = blur;
                intro_manual.Visibility = Visibility.Visible;
                is_new_user = false;
            }
            else
            {
                main.Effect = null;
                intro_manual.Visibility = Visibility.Hidden;
                is_new_user = false;
            }*/

            GetAccounts(account_file);
        }

        private void main_bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //DRAGGING
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            //ENCRYPT_DATA
            if (File.Exists("data/user1.txt")) security.Encrypt_Data("data/user1.txt");
            if (File.Exists("data/user2.txt")) security.Encrypt_Data("data/user2.txt");
            if (File.Exists("data/user3.txt")) security.Encrypt_Data("data/user3.txt");
            //ENCRYPT_DATA-END
            var avatar_main = Application.Current.MainWindow as Avatar_Login_Register;
            this.Close();
            avatar_main.Close();
        }

        private void minimalize_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public void GetAccounts(string file)
        {
            //RESET
            social_media.Children.Clear();
            email_accounts.Children.Clear();
            games_platforms.Children.Clear();
            communication.Children.Clear();
            applications.Children.Clear();
            websites.Children.Clear();
            payment.Children.Clear();
            other.Children.Clear();

            social_media.Height = 505;
            email_accounts.Height = 505;
            games_platforms.Height = 505;
            communication.Height = 505;
            applications.Height = 505;
            websites.Height = 505;
            payment.Height = 505;
            other.Height = 505;

            spacer = 10;
            //RESET-END
            try
            {
                //ONLY GOD KNOWS WHAT THIS ONE DOES REF
                string[] raw_data = File.ReadAllLines(account_file);

                user_name_label.Content = raw_data[0].ToString();
                user_name_avatar.Source = new BitmapImage(new Uri(raw_data[3].ToString(), UriKind.RelativeOrAbsolute));
                string[] data_social_media = raw_data[4].Split('-');
                string[] data_email_accounts = raw_data[5].Split('-');
                string[] data_games_platforms = raw_data[6].Split('-');
                string[] data_communication = raw_data[7].Split('-');
                string[] data_applications = raw_data[8].Split('-');
                string[] data_websites = raw_data[9].Split('-');
                string[] data_payment = raw_data[10].Split('-');
                string[] data_other = raw_data[11].Split('-');
            

                int pointer_name = 0;
                int pointer_login = 1;
                int pointer_password = 2;
                int pointer_icon = 3;

                int number_of_accounts_social_media = data_social_media.Length / 4;
                int number_of_accounts_email_accounts = data_email_accounts.Length / 4;
                int number_of_accounts_games_platforms = data_games_platforms.Length / 4;
                int number_of_accounts_communication = data_communication.Length / 4;
                int number_of_accounts_applications = data_applications.Length / 4;
                int number_of_accounts_websites = data_websites.Length / 4;
                int number_of_accounts_payment = data_payment.Length / 4;
                int number_of_accounts_other = data_other.Length / 4;

                int number_of_accounts_total = data_social_media.Length / 4 + data_email_accounts.Length / 4 + data_games_platforms.Length / 4 + data_communication.Length / 4 + data_applications.Length / 4 + data_websites.Length / 4 + data_payment.Length / 4 + data_other.Length / 4;

                //creating lists with names of controls
                List<string> accounts_panels = new List<string>();
                List<string> icons = new List<string>();
                List<string> names = new List<string>();
                List<string> labels_login = new List<string>();
                List<string> labels_password = new List<string>();
                List<string> logins = new List<string>();
                List<string> passwords = new List<string>();
                List<string> labels_descriptions = new List<string>();
                List<string> descriptions = new List<string>();
                List<string> edit_panels = new List<string>();
                List<string> delete_panels = new List<string>();

                for (int i = 0; i < number_of_accounts_total; i++)
                {
                    accounts_panels.Add("account_" + i);
                    icons.Add("icon_" + i);
                    names.Add("name_" + i);
                    labels_login.Add("label_login_" + i);
                    labels_password.Add("label_password_" + i);
                    logins.Add("login_" + i);
                    passwords.Add("password_" + i);
                    labels_descriptions.Add("label_description_" + i);
                    descriptions.Add("description_" + i);
                    edit_panels.Add("edit_panel_" + i);
                    delete_panels.Add("delete_panel_" + i);
                }

                for (int i = 0; i < number_of_accounts_social_media; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    social_media.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_social_media[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_social_media[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_social_media[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_social_media[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {
                        
                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if(i>=5)
                    {
                        social_media.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_email_accounts; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    email_accounts.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_email_accounts[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_email_accounts[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_email_accounts[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_email_accounts[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        email_accounts.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_games_platforms; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    games_platforms.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_games_platforms[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_games_platforms[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_games_platforms[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_games_platforms[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        games_platforms.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_communication; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    communication.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_communication[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_communication[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_communication[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_communication[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        communication.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_applications; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    applications.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_applications[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_applications[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_applications[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_applications[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        applications.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_websites; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    websites.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_websites[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_websites[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_websites[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_websites[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        communication.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_payment; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    payment.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_payment[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_payment[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_payment[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_payment[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {

                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {

                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        payment.Height += 100;
                    }
                }

                spacer = 10;
                pointer_name = 0;
                pointer_login = 1;
                pointer_password = 2;
                pointer_icon = 3;

                for (int i = 0; i < number_of_accounts_other; i++)
                {
                    Canvas account = new Canvas();
                    account.Height = 100;
                    account.Width = 815;
                    account.Name = accounts_panels[i];
                    Thickness account_margin = account.Margin;
                    account_margin.Left = 10;
                    account_margin.Top = spacer;
                    account.Margin = account_margin;
                    other.Children.Add(account);

                    Image icon = new Image();
                    icon.Height = 80;
                    icon.Width = 80;
                    icon.Name = icons[i];
                    Thickness icon_margin = account.Margin;
                    icon_margin.Left = 5;
                    icon_margin.Top = 4;
                    icon.Margin = icon_margin;
                    BitmapImage image = new BitmapImage(new Uri(data_other[pointer_icon], UriKind.RelativeOrAbsolute));
                    icon.Source = image;
                    account.Children.Add(icon);

                    Label service_name = new Label();
                    Thickness service_name_margin = account.Margin;
                    service_name_margin.Left = 95;
                    service_name_margin.Top = 5;
                    service_name.Margin = service_name_margin;
                    service_name.FontSize = 20;
                    service_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    service_name.FontWeight = FontWeights.Light;
                    service_name.Content = data_other[pointer_name].ToString();
                    service_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 231, 30, 98));
                    service_name.Name = names[i];
                    account.Children.Add(service_name);

                    Label login_name = new Label();
                    Thickness login_name_margin = account.Margin;
                    login_name_margin.Left = 95;
                    login_name_margin.Top = 35;
                    login_name.Margin = login_name_margin;
                    login_name.FontSize = 16;
                    login_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login_name.FontWeight = FontWeights.Light;
                    login_name.Content = "USERNAME:";
                    login_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login_name.Name = labels_login[i];
                    account.Children.Add(login_name);

                    Label password_name = new Label();
                    Thickness password_name_margin = account.Margin;
                    password_name_margin.Left = 95;
                    password_name_margin.Top = 60;
                    password_name.Margin = password_name_margin;
                    password_name.FontSize = 16;
                    password_name.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password_name.FontWeight = FontWeights.Light;
                    password_name.Content = "PASSWORD:";
                    password_name.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password_name.Name = labels_password[i];
                    account.Children.Add(password_name);

                    Label login = new Label();
                    Thickness login_margin = account.Margin;
                    login_margin.Left = 220;
                    login_margin.Top = 35;
                    login.Margin = login_margin;
                    login.FontSize = 16;
                    login.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    login.FontWeight = FontWeights.Light;
                    login.Content = data_other[pointer_login].ToString();
                    login.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    login.Name = logins[i];
                    account.Children.Add(login);

                    Label password = new Label();
                    Thickness password_margin = account.Margin;
                    password_margin.Left = 220;
                    password_margin.Top = 60;
                    password.Margin = password_margin;
                    password.FontSize = 16;
                    password.FontFamily = new FontFamily("/AVATAR;component/resources/#Moon");
                    password.FontWeight = FontWeights.Light;
                    password.Content = data_other[pointer_password].ToString();
                    password.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 119, 238));
                    password.Name = passwords[i];
                    account.Children.Add(password);

                    Image edit_icon = new Image();
                    edit_icon.Height = 30;
                    edit_icon.Width = 30;
                    edit_icon.Name = edit_panels[i];
                    Thickness edit_margin = account.Margin;
                    edit_margin.Left = 720;
                    edit_margin.Top = 35;
                    edit_icon.Margin = edit_margin;
                    BitmapImage edit_image = new BitmapImage(new Uri("/Avatar;component/resources/edit_account.png", UriKind.Relative));
                    edit_icon.Source = edit_image;
                    edit_icon.MouseDown += (source, e) =>
                    {
                    
                    };
                    edit_icon.MouseEnter += (source, e) =>
                    {
                        edit_icon.Opacity = 0.5;
                    };
                    edit_icon.MouseLeave += (source, e) =>
                    {
                        edit_icon.Opacity = 1.0;
                    };
                    account.Children.Add(edit_icon);

                    Image delete_icon = new Image();
                    delete_icon.Height = 30;
                    delete_icon.Width = 30;
                    delete_icon.Name = delete_panels[i];
                    Thickness delete_margin = account.Margin;
                    delete_margin.Left = 760;
                    delete_margin.Top = 35;
                    delete_icon.Margin = delete_margin;
                    BitmapImage delete_image = new BitmapImage(new Uri("/Avatar;component/resources/delete_account.png", UriKind.Relative));
                    delete_icon.Source = delete_image;
                    delete_icon.MouseDown += (source, e) =>
                    {
                    
                    };
                    delete_icon.MouseEnter += (source, e) =>
                    {
                        delete_icon.Opacity = 0.5;
                    };
                    delete_icon.MouseLeave += (source, e) =>
                    {
                        delete_icon.Opacity = 1.0;
                    };
                    account.Children.Add(delete_icon);

                    spacer = spacer + 100;
                    pointer_name += 4;
                    pointer_login += 4;
                    pointer_password += 4;
                    pointer_icon += 4;

                    if (i >= 5)
                    {
                        other.Height += 100;
                    }
            }
            }
            catch (Exception ex)
            {

            }

            //Canvas todelete = (Canvas)FindName("");
            //todelete.Children.Clear();
        }

        private void add_button_Click(object sender, RoutedEventArgs e)
        {
            var blur = new BlurEffect();
            blur.Radius = 15;
            main.Effect = blur;
            add_acc.Visibility = Visibility.Visible;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Visible;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Visible;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Visible;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Visible;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Visible;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Visible;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Visible;
            scroll_other.Visibility = Visibility.Hidden;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            scroll_social_media.Visibility = Visibility.Hidden;
            scroll_email_accounts.Visibility = Visibility.Hidden;
            scroll_games_platforms.Visibility = Visibility.Hidden;
            scroll_communication.Visibility = Visibility.Hidden;
            scroll_applications.Visibility = Visibility.Hidden;
            scroll_websites.Visibility = Visibility.Hidden;
            scroll_payment.Visibility = Visibility.Hidden;
            scroll_other.Visibility = Visibility.Visible;
        }

        private void add_account_button_Click(object sender, RoutedEventArgs e)
        {
            if (category_comboBox.SelectedIndex == 0)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[4]!=null)
                    raw_data[4] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[4];
                else
                    raw_data[4] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 1)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[5] != null)
                    raw_data[5] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[5];
                else
                    raw_data[5] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 2)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[6] != null)
                    raw_data[6] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[6];
                else
                    raw_data[6] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);


                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 3)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[7] != null)
                    raw_data[7] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[7];
                else
                    raw_data[7] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 4)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[8] != null)
                    raw_data[8] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[8];
                else
                    raw_data[8] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 5)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[9] != null)
                    raw_data[9] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[9];
                else
                    raw_data[9] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 6)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[10] != null)
                    raw_data[10] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[10];
                else
                    raw_data[10] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            if (category_comboBox.SelectedIndex == 7)
            {
                string[] raw_data = File.ReadAllLines(account_file);
                if (raw_data[11] != null)
                    raw_data[11] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-" + raw_data[11];
                else
                    raw_data[11] = name_textBox.Text + "-" + login_textBox.Text + "-" + password_textBox.Text + "-" + new_account_image.Source.ToString() + "-";

                File.WriteAllLines(account_file, raw_data);

                GetAccounts(account_file);

                main.Effect = null;
                add_acc.Visibility = Visibility.Hidden;
            }
            name_textBox.Text = "Name of account";
            login_textBox.Text = "Login";
            password_textBox.Text = "Password";
            new_account_image.Source = new BitmapImage(new Uri("/AVATAR;component/resources/add_account.png", UriKind.RelativeOrAbsolute));
        }

        private void close_adding_button_Click(object sender, RoutedEventArgs e)
        {
            main.Effect = null;
            add_acc.Visibility = Visibility.Hidden;
            name_textBox.Text = "Name of account";
            login_textBox.Text = "Login";
            password_textBox.Text = "Password";
            new_account_image.Source = new BitmapImage(new Uri("/AVATAR;component/resources/add_account.png", UriKind.RelativeOrAbsolute));
        }

        private void new_account_image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Nullable<bool> result = ofd.ShowDialog();

            if (result == true)
            {
                new_account_image.Source = new BitmapImage(new Uri(ofd.FileName.ToString(), UriKind.RelativeOrAbsolute));
            }
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            GetAccounts(account_file);
        }

        private void log_out_image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var avatar_main=Application.Current.MainWindow as Avatar_Login_Register;
            avatar_main.Starting();
            avatar_main.Show();
            this.Close();
        }

        private void log_out_image_MouseEnter(object sender, MouseEventArgs e)
        {
            log_out_image.Opacity = 0.7;
        }

        private void log_out_image_MouseLeave(object sender, MouseEventArgs e)
        {
            log_out_image.Opacity = 1.0;
        }

        private void exit_manual_button_Click(object sender, RoutedEventArgs e)
        {
            intro_manual.Visibility = Visibility.Hidden;
            main.Effect = null;
        }
    }
}
