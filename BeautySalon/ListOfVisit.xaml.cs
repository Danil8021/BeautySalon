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

namespace BeautySalon
{
    /// <summary>
    /// Логика взаимодействия для ListOfVisit.xaml
    /// </summary> 

    public partial class ListOfVisit : Window
    {
        BeautySalonEntities context;
        Client client;
        public ListOfVisit(BeautySalonEntities context, Client client)
        {
            InitializeComponent();
            this.context = context;
            this.client = client;
            var list = from cs in context.ClientService where cs.ClientID == client.ID select cs;
            LVVisits.ItemsSource = list.ToList();
        }
    }
}
