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
        int page = 0;
        int countList = 0;


        public MainWindow()
        {
            InitializeComponent();
            context = new BeautySalonEntities();
            cbGender.SelectedIndex = 0;
            cbPage.SelectedIndex = 0;
        }
        
        public void ShowTClient()
        {
            int genderIndex = 0;
            int pageItem = 0;
            if (cbGender.SelectedIndex == 0 && cbPage.SelectedIndex == 0)
            {
                var sel0 = (from c in context.Client where c.Email.StartsWith(tbEmail.Text) && c.FirstName.StartsWith(tbI.Text) && c.LastName.StartsWith(tbF.Text) && c.Patronymic.StartsWith(tbO.Text) && c.Phone.StartsWith(tbPhone.Text) orderby c.ID select c).ToList();
                dgClient.ItemsSource = sel0;
                countList = sel0.Count;
                tbCountPage.Text = $"{sel0.Count} из {countList}";
                return;
            }
            switch(cbGender.SelectedIndex)
            {
                case 0:
                    genderIndex = 0;
                    break;
                case 1:
                    genderIndex = 2;
                    break;
                case 2:
                    genderIndex = 1;
                    break;
                    
            }
            switch(cbPage.SelectedIndex)
            {
                case 0:
                    pageItem = 0;
                    break;
                case 1:
                    pageItem = 10;
                    break;
                case 2:
                    pageItem = 50;
                    break;
                case 3:
                    pageItem = 200;
                    break;
            }
            if (cbGender.SelectedIndex != 0 && cbPage.SelectedIndex == 0)
            {
                var selG = (from c in context.Client where c.GenderCode.StartsWith(genderIndex.ToString()) && c.Email.StartsWith(tbEmail.Text) && c.FirstName.StartsWith(tbI.Text) && c.LastName.StartsWith(tbF.Text) && c.Patronymic.StartsWith(tbO.Text) && c.Phone.StartsWith(tbPhone.Text) orderby c.ID select c).ToList();
                dgClient.ItemsSource = selG;
                tbCountPage.Text = $"{selG.Count} из {countList}";
                return;
            }
            if (cbGender.SelectedIndex == 0 && cbPage.SelectedIndex != 0)
            {
                var selP = (from c in context.Client where c.Email.StartsWith(tbEmail.Text) && c.FirstName.StartsWith(tbI.Text) && c.LastName.StartsWith(tbF.Text) && c.Patronymic.StartsWith(tbO.Text) && c.Phone.StartsWith(tbPhone.Text) orderby c.ID select c).Skip(page).Take(pageItem).ToList();
                dgClient.ItemsSource = selP;
                tbCountPage.Text = $"{selP.Count} из {countList}";
                return;
            }
            var sel = (from c in context.Client where c.GenderCode.StartsWith(genderIndex.ToString()) && c.Email.StartsWith(tbEmail.Text) && c.FirstName.StartsWith(tbI.Text) && c.LastName.StartsWith(tbF.Text) && c.Patronymic.StartsWith(tbO.Text) && c.Phone.StartsWith(tbPhone.Text) orderby c.ID select c).Skip(page).Take(pageItem).ToList();
            dgClient.ItemsSource = sel;
            tbCountPage.Text = $"{sel.Count} из {countList}";
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEdit.IsEnabled = true;
            btnDel.IsEnabled = true;
            btnListOfVisit.IsEnabled = true;
        }

        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTClient();
        }

        private void cbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTClient();
        }

        private void tbI_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTClient();
        }

        private void tbF_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTClient();
        }

        private void tbO_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTClient();
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTClient();
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowTClient();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (page == 0)
            {
                return;
            }
            switch (cbPage.SelectedIndex)
            {
                case 0:
                    page -= 0;
                    break;
                case 1:
                    page -= 10;
                    break;
                case 2:
                    page -= 50;
                    break;
                case 3:
                    page -= 200;
                    break;
            }
            ShowTClient();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (page == countList - dgClient.Items.Count)
            {
                return;
            }
            switch (cbPage.SelectedIndex)
            {
                case 0:
                    page += 0;
                    break;
                case 1:
                    page += 10;
                    break;
                case 2:
                    page += 50;
                    break;
                case 3:
                    page += 200;
                    break;
            }
            ShowTClient();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new Client();
            context.Client.Add(newClient);
            var window = new AddUsers(context, newClient, true);
            window.ShowDialog();
            ShowTClient();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            var d = dgClient.SelectedItem as Client;
            if (d == null)
            {
                MessageBox.Show("Выберите строку в таблице!");
                return;
            }

            context.Client.Remove(d);
            context.SaveChanges();
            ShowTClient();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var Client = dgClient.SelectedItem as Client;
            var add = new AddUsers(context, Client, false);
            add.ShowDialog();
            ShowTClient();
        }

        private void btnListOfVisit_Click(object sender, RoutedEventArgs e)
        {
            var Client = dgClient.SelectedItem as Client;
            var list = new ListOfVisit(context, Client);
            list.ShowDialog();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbI.Text = "";
            tbF.Text = "";
            tbO.Text = "";
            cbGender.SelectedIndex = 0;
            tbPhone.Text = "";
            tbEmail.Text = "";
        }
    }
}
