using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace AVATAR
{
    /// <summary>
    /// Interaction logic for Avatar_Login_Register.xaml
    /// </summary>
    public partial class Avatar_Login_Register : Window
    {
        public bool account_1_exists = false;
        public bool account_2_exists = false;
        public bool account_3_exists = false;
        public string account_logging = "";
        public Security security = new Security();

        public Avatar_Login_Register()
        {
            InitializeComponent();
            Starting();
        }

        public void Starting()
        {
            choose_account_canvas.Visibility = Visibility.Visible;
            create_account_canvas.Visibility = Visibility.Hidden;
            login_to_account_canvas.Visibility = Visibility.Hidden;

            name_textBox_new.Text = "";
            password_textBox_new.Text = "";
            password_textBox.Text = "";
            account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
            password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

            if (File.Exists("data/user1.txt"))
            {
                security.Decrypt_Data("data/user1.txt");
                string[] data1 = File.ReadAllLines("data/user1.txt");
                account_name_1.Content = data1[0].ToString();
                add_new_account_1.Source = new BitmapImage(new Uri(data1[3].ToString(), UriKind.RelativeOrAbsolute));
                add_new_account_1.ToolTip = "Go to account.";
                account_1_exists = true;
            }
            if (File.Exists("data/user2.txt"))
            {
                security.Decrypt_Data("data/user2.txt");
                string[] data2 = File.ReadAllLines("data/user2.txt");
                account_name_2.Content = data2[0].ToString();
                add_new_account_2.Source = new BitmapImage(new Uri(data2[3].ToString(), UriKind.RelativeOrAbsolute));
                add_new_account_2.ToolTip = "Go to account.";
                account_2_exists = true;
            }
            if (File.Exists("data/user3.txt"))
            {
                security.Decrypt_Data("data/user3.txt");
                string[] data3 = File.ReadAllLines("data/user3.txt");
                account_name_3.Content = data3[0].ToString();
                add_new_account_3.Source = new BitmapImage(new Uri(data3[3].ToString(), UriKind.RelativeOrAbsolute));
                add_new_account_3.ToolTip = "Go to account.";
                account_3_exists = true;
            }
        }

        private void main_bar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void create_account_button_Click(object sender, RoutedEventArgs e)
        {
            if(File.Exists("data/user1.txt"))
            {
                if (File.Exists("data/user2.txt"))
                {
                    if (File.Exists("data/user3.txt"))
                    {
                        MessageBox.Show("You have reached your free account limit!");
                    }
                    else
                    {
                        string[] account3 = { name_textBox_new.Text.ToString(), password_textBox_new.Text.ToString(), key_textBox_new.Text.ToString(), account_avatar_new.Source.ToString(), "", "", "", "", "", "", "", "" };
                        File.WriteAllLines("data/user3.txt", account3);

                        MainWindow mw = new MainWindow();
                        mw.Show();
                        this.Hide();
                        mw.Load_User_Data("data/user3.txt");
                    }
                }
                else
                {
                    string[] account2 = { name_textBox_new.Text.ToString(), password_textBox_new.Text.ToString(), key_textBox_new.Text.ToString(), account_avatar_new.Source.ToString(), "", "", "", "", "", "", "", "" };
                    File.WriteAllLines("data/user2.txt", account2);

                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Hide();
                    mw.Load_User_Data("data/user2.txt");
                }
            }
            else
            {
                string[] account = { name_textBox_new.Text.ToString(), password_textBox_new.Text.ToString(), key_textBox_new.Text.ToString(), account_avatar_new.Source.ToString(), "", "", "", "", "", "", "", "" };
                File.WriteAllLines("data/user1.txt", account);

                //MainWindow mw = Application.Current.MainWindow as MainWindow;
                //mw.GetAccounts();
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Hide();
                mw.Load_User_Data("data/user1.txt");
            }

            name_textBox_new.Text = "";
            password_textBox_new.Text = "";
            password_textBox.Text = "";
            account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
            password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void add_new_account_1_MouseEnter(object sender, MouseEventArgs e)
        {
            add_new_account_1.Opacity = 0.7;
        }

        private void add_new_account_1_MouseLeave(object sender, MouseEventArgs e)
        {
            add_new_account_1.Opacity = 1.0;
        }

        private void add_new_account_2_MouseEnter(object sender, MouseEventArgs e)
        {
            add_new_account_2.Opacity = 0.7;
        }

        private void add_new_account_2_MouseLeave(object sender, MouseEventArgs e)
        {
            add_new_account_2.Opacity = 1.0;
        }

        private void add_new_account_3_MouseEnter(object sender, MouseEventArgs e)
        {
            add_new_account_3.Opacity = 0.7;
        }

        private void add_new_account_3_MouseLeave(object sender, MouseEventArgs e)
        {
            add_new_account_3.Opacity = 1.0;
        }

        private void add_new_account_1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(account_1_exists)
            {
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Hidden;
                string[] data1 = File.ReadAllLines("data/user1.txt");
                account_name.Content = data1[0].ToString();
                account_avatar.Source= new BitmapImage(new Uri(data1[3].ToString(), UriKind.RelativeOrAbsolute));
                login_to_account_canvas.Visibility = Visibility.Visible;
                account_logging = "data/user1.txt";
            }
            else
            {
                name_textBox_new.Text = "";
                password_textBox_new.Text = "";
                password_textBox.Text = "";
                account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
                password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Visible;
                key_textBox_new.Text = security.Random_Security_Key();
            }
        }

        private void add_new_account_2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (account_2_exists)
            {
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Hidden;
                string[] data2 = File.ReadAllLines("data/user2.txt");
                account_name.Content = data2[0].ToString();
                account_avatar.Source = new BitmapImage(new Uri(data2[3].ToString(), UriKind.RelativeOrAbsolute));
                login_to_account_canvas.Visibility = Visibility.Visible;
                account_logging = "data/user2.txt";
            }
            else
            {
                name_textBox_new.Text = "";
                password_textBox_new.Text = "";
                password_textBox.Text = "";
                account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
                password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Visible;
                key_textBox_new.Text = security.Random_Security_Key();
            }
        }

        private void add_new_account_3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (account_3_exists)
            {
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Hidden;
                string[] data3 = File.ReadAllLines("data/user3.txt");
                account_name.Content = data3[0].ToString();
                account_avatar.Source = new BitmapImage(new Uri(data3[3].ToString(), UriKind.RelativeOrAbsolute));
                login_to_account_canvas.Visibility = Visibility.Visible;
                account_logging = "data/user3.txt";
            }
            else
            {
                name_textBox_new.Text = "";
                password_textBox_new.Text = "";
                password_textBox.Text = "";
                account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
                password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                choose_account_canvas.Visibility = Visibility.Hidden;
                create_account_canvas.Visibility = Visibility.Visible;
                key_textBox_new.Text = security.Random_Security_Key();
            }
        }

        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("data/user1.txt")) security.Encrypt_Data("data/user1.txt");
            if (File.Exists("data/user2.txt")) security.Encrypt_Data("data/user2.txt");
            if (File.Exists("data/user3.txt")) security.Encrypt_Data("data/user3.txt");
            Environment.Exit(0);
        }

        private void back_arrow_button_new_Click(object sender, RoutedEventArgs e)
        {
            name_textBox_new.Text = "";
            password_textBox_new.Text = "";
            password_textBox.Text = "";
            account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
            password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            choose_account_canvas.Visibility = Visibility.Visible;
            create_account_canvas.Visibility = Visibility.Hidden;
            login_to_account_canvas.Visibility = Visibility.Hidden;
        }

        private void back_arrow_button_Click(object sender, RoutedEventArgs e)
        {
            name_textBox_new.Text = "";
            password_textBox_new.Text = "";
            password_textBox.Text = "";
            account_avatar_new.Source = new BitmapImage(new Uri("/AVATAR;component/resources/avatar3.png", UriKind.RelativeOrAbsolute));
            password_textBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            choose_account_canvas.Visibility = Visibility.Visible;
            create_account_canvas.Visibility = Visibility.Hidden;
            login_to_account_canvas.Visibility = Visibility.Hidden;
        }

        private void account_avatar_new_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            Nullable<bool> result = ofd.ShowDialog();

            if(result==true)
            {
                account_avatar_new.Source = new BitmapImage(new Uri(ofd.FileName.ToString(), UriKind.RelativeOrAbsolute));
            }
        }

        private void login_to_account_button_Click(object sender, RoutedEventArgs e)
        {
            string[] account_data = File.ReadAllLines(account_logging);
            if(password_textBox.Text==account_data[1].ToString())
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Hide();
                mw.Load_User_Data(account_logging);
            }
            else
            {
                password_textBox.BorderBrush= new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }
        }

        private void minimalize_button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
