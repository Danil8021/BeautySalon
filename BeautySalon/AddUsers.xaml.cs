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
                tBlockId.Visibility = Visibility.Hidden;
                tBoxId.Visibility = Visibility.Hidden;
            }
            else
            {
                this.Title = "Редактирование";
            }
            cBoxGender.ItemsSource = context.Gender.ToList();
        }

        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "*.png, *.jpeg|*.png, *.jpeg";
            file.ShowDialog();
            if (file.FileName.Length != 0)
            {
                string fileName = file.FileName;
                FileInfo i = new FileInfo(fileName);
                string path = $@"C:\Users\Дмитрий\source\repos\GNEBeautySalon-master\GNEBeautySalon\Клиенты\{i.Name}";
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

        }
    }
}
