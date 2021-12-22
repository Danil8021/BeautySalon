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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Reflection;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        BeautySalonEntities context;
        bool a;
        public AddUsers(BeautySalonEntities context, Client Client, bool a)
        {
            InitializeComponent();
            this.context = context;
            this.DataContext = Client;
            this.a = a;

            if (a)
            {
                dpBirthday.SelectedDate = DateTime.Now;
                tBlockId.Visibility = Visibility.Hidden;
                tBoxId.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Title = "Редактирование";
            }
            cBoxGender.ItemsSource = context.Gender.ToList();
        }

        private void bAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            file.ShowDialog();
            if (file.CheckFileExists)
            {
                string fileName = file.FileName;
                FileInfo i = new FileInfo(fileName); //C:\Users\Дмитрий\Source\Repos\Danil8021\BeautySalon\BeautySalon\Клиенты\
                string path = $@"d:\users\is12329\Desktop\BeautySalon\BeautySalon\Клиенты\{i.Name}";
                if (!File.Exists(path))
                {
                    i.CopyTo(path);
                }
                var cl = (Client)this.DataContext;
                cl.PhotoPath = $@"Клиенты\{i.Name}";
                ImgMin.Source = new BitmapImage(new Uri(path));
            }
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            if (cBoxGender.SelectedIndex == -1)
            {
                MessageBox.Show("Пол не указан.");
            }
            else
            {
                if (tBoxFirstName.Text == "")
                {
                    MessageBox.Show("Имя не указано.");
                }
                else
                {
                    if (tBoxLastName.Text == "")
                    {
                        MessageBox.Show("Фамилия не указана.");
                    }
                    else
                    {
                        if (tBoxPhone.Text == "")
                        {
                            MessageBox.Show("Телефон не указан.");
                        }
                        else
                        {
                            string pattern = @"\w+([-+.]*\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                            var email = tBoxEmail.Text.Trim().ToLowerInvariant();
                            if (Regex.Match(email,pattern).Success || tBoxEmail.Text == "")
                            {
                                var client = (Client)this.DataContext;
                                client.RegistrationDate = DateTime.Now;
                                client.Birthday = (DateTime)dpBirthday.SelectedDate;
                                context.SaveChanges();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Неправельно введён адрес электронной почты");
                                tBoxEmail.Text = "";
                            }
                        }
                    }
                }
            }
        }

        private void tBoxFirstName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text,0))
            {
                e.Handled = true;
            }
        }

        private void tBoxLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void tBoxPatronymic_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void tBoxPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsNumber(e.Text,0) && e.Text != "+" && e.Text != "(" && e.Text != ")")
            {
                e.Handled = true;
            }
        }
    }
}
