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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BeautySalonEntities context;

        public MainWindow()
        {
            InitializeComponent();
            context = new BeautySalonEntities();
            ShowTClient();
        }
        
        public void ShowTClient()
        {
            dgClient.ItemsSource = context.Client.ToList();
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
