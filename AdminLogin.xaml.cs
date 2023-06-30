using Prism.Events;
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
using static CoffeMachine.PlaceOrder;

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for AdminLogin.xaml
    /// </summary>
    public partial class AdminLogin : Window
    {
        private Button izmeni;
        private Button plati;
        private StackPanel bottomStackPanel;
        private PlaceOrder placeOrderForm;
        private ComboBox comboBox;
        Button btnIzbrisi,btnDodaj,btnLogOut,btnSezona,btnLeto,btnZima;
        private int izbrisaniId;
        private Label lbZaMeni;
        private int buttonPressCount = 0;


        public AdminLogin(Button btnIzmeni, Button btnPlati, StackPanel bottomPartStackPanel, PlaceOrder placeOrder,ComboBox cbIzbrisi, int StackPanelId, Label meniLabel)
        {
            InitializeComponent();
            this.izmeni = btnIzmeni;
            this.plati = btnPlati;
            this.bottomStackPanel = bottomPartStackPanel;
            this.placeOrderForm = placeOrder;
            this.comboBox = cbIzbrisi;
            this.izbrisaniId = StackPanelId;
            this.lbZaMeni = meniLabel;
        }
        public Point Location { get; private set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int screenWidth = (int)SystemParameters.FullPrimaryScreenWidth;
            int screenHeight = (int)SystemParameters.FullPrimaryScreenHeight;
            int formWidth = (int)this.Width;
            int formHeight = (int)this.Height;

            int x = (screenWidth - formWidth) / 2;
            int y = (screenHeight - formHeight) / 2;
            Location = new Point(x, y);

        }
        private void AddNewButtons()
        {
            //Izbrisi button
            btnIzbrisi = new Button();
            btnIzbrisi.Click += BtnIzbrisi_Click;
            btnIzbrisi.Content = "IZBRISI";
            btnIzbrisi.Margin = new Thickness(10, 110, 0, 0);
            btnIzbrisi.Height = 30;
            btnIzbrisi.Width = 100;
            btnIzbrisi.FontSize = 20;
            btnIzbrisi.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnIzbrisi.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnIzbrisi.FontFamily = new FontFamily("Segoe UI Black");
            btnIzbrisi.FontWeight = FontWeights.Bold;
            btnIzbrisi.BorderThickness = new Thickness(0, 0, 0, 0);
            //Dodaj button
            btnDodaj = new Button();
            btnDodaj.Click += BtnDodaj_Click;
            btnDodaj.Content = "DODAJ";
            btnDodaj.Margin = new Thickness(10, 110, 0, 0);
            btnDodaj.Height = 30;
            btnDodaj.Width = 100;
            btnDodaj.FontSize = 20;
            btnDodaj.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnDodaj.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnDodaj.FontFamily = new FontFamily("Segoe UI Black");
            btnDodaj.FontWeight = FontWeights.Bold;
            btnDodaj.BorderThickness = new Thickness(0, 0, 0, 0);

            btnLogOut = new Button();
            btnLogOut.Content = "IZLOGUJ SE";
            btnLogOut.Click += BtnLogOut_Click;
            btnLogOut.Margin = new Thickness(10, 110, 0, 0);
            btnLogOut.Height = 30;
            btnLogOut.Width = 120;
            btnLogOut.FontSize = 18;
            btnLogOut.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnLogOut.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnLogOut.FontFamily = new FontFamily("Segoe UI Black");
            btnLogOut.FontWeight = FontWeights.Bold;
            btnLogOut.BorderThickness = new Thickness(0, 0, 0, 0);

            //Dodaj sezonu
            btnSezona = new Button();
            btnSezona.Content = "SEZONA";
            btnSezona.Click += BtnSezona_Click;
            btnSezona.Margin = new Thickness(10, 110, 0, 0);
            btnSezona.Height = 30;
            btnSezona.Width = 120;
            btnSezona.FontSize = 18;
            btnSezona.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnSezona.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnSezona.FontFamily = new FontFamily("Segoe UI Black");
            btnSezona.FontWeight = FontWeights.Bold;
            btnSezona.BorderThickness = new Thickness(0, 0, 0, 0);
            //Dodaj leto
            btnLeto = new Button();
            btnLeto.Content = "LETO";
            btnLeto.Click += BtnLeto_Click;
            btnLeto.Margin = new Thickness(10, 110, 0, 0);
            btnLeto.Height = 30;
            btnLeto.Width = 120;
            btnLeto.FontSize = 18;
            btnLeto.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnLeto.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnLeto.FontFamily = new FontFamily("Segoe UI Black");
            btnLeto.FontWeight = FontWeights.Bold;
            btnLeto.BorderThickness = new Thickness(0, 0, 0, 0);
            //Dodaj zimu
            btnZima = new Button();
            btnZima.Content = "ZIMA";
            btnZima.Click += BtnZima_Click;
            btnZima.Margin = new Thickness(10, 110, 0, 0);
            btnZima.Height = 30;
            btnZima.Width = 120;
            btnZima.FontSize = 18;
            btnZima.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnZima.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnZima.FontFamily = new FontFamily("Segoe UI Black");
            btnZima.FontWeight = FontWeights.Bold;
            btnZima.BorderThickness = new Thickness(0, 0, 0, 0);

            // Add the new buttons to the stack panel
            bottomStackPanel.Children.Add(btnIzbrisi);
            bottomStackPanel.Children.Add(btnDodaj);
            bottomStackPanel.Children.Add(btnLogOut);
            bottomStackPanel.Children.Add(btnSezona);
        }

        private void BtnZima_Click(object sender, RoutedEventArgs e)
        {
            lbZaMeni.Content = "ZIMSKI MENI";
        }

        private void BtnLeto_Click(object sender, RoutedEventArgs e)
        {
            lbZaMeni.Content = "LETNJI MENI";
        }

        private void BtnSezona_Click(object sender, RoutedEventArgs e)
        {
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnSezona);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnIzbrisi);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnDodaj);
            placeOrderForm.cbPart.Children.Clear();
            placeOrderForm.bottomPartStackPanel.Children.Add(btnLeto);
            placeOrderForm.bottomPartStackPanel.Children.Add(btnZima);
        }

        private void BtnLogOut_Click(object sender, RoutedEventArgs e)
        {
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnIzbrisi);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnDodaj);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnLogOut);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnSezona);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnLeto);
            placeOrderForm.bottomPartStackPanel.Children.Remove(btnZima);
            placeOrderForm.Width = 1020;
            placeOrderForm.Height = 600;
            placeOrderForm.konacnaCena.Visibility = Visibility.Visible;
            placeOrderForm.ukupanIznos.Visibility = Visibility.Visible;
            placeOrderForm.dinLabel.Visibility = Visibility.Visible;
            bottomStackPanel.Children.Add(izmeni);
            bottomStackPanel.Children.Add(plati);
            placeOrderForm.cbPart.Children.Clear();

         

            
        }
       

        private void BtnDodaj_Click(object sender, RoutedEventArgs e)
        {
            izbrisaniId = 0;
            placeOrderForm.cbZaDodavanje();
            placeOrderForm.AddNewCoffe();
        
            
        }

        private void BtnIzbrisi_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the reference to the PlaceOrder window

            buttonPressCount++;
            if (placeOrderForm != null)
            {
                izbrisaniId = 0;
                placeOrderForm.cbZaBrisanje();
                placeOrderForm.RemoveCoffeeCard();
                if (buttonPressCount > 2)
                {
                    placeOrderForm.cbPart.Children.Clear();
                    buttonPressCount = 0;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbUsername.Text == "admin" && pbPassword.Password == "1234")
            {
                bottomStackPanel.Children.Remove(izmeni);
                bottomStackPanel.Children.Remove(plati);
                placeOrderForm.Width = 1220;
                placeOrderForm.Height = 650;
                placeOrderForm.konacnaCena.Visibility = Visibility.Hidden;
                placeOrderForm.ukupanIznos.Visibility = Visibility.Hidden;
                placeOrderForm.dinLabel.Visibility = Visibility.Hidden;
                AddNewButtons();

               placeOrderForm.Show();
                this.Hide();

            }
            else if (!string.IsNullOrWhiteSpace(tbUsername.Text) && string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                MessageBox.Show("Unesite šifru");
            }
            else if (string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                MessageBox.Show("Uneiste korisničko ime");
            }
            else if (tbUsername.Text != "admin" && pbPassword.Password != "1234")
            {
                MessageBox.Show("Vi niste vlasnik kafemata!");
            }
            else
            {
                MessageBox.Show("Unesite korisničko ime i šifru");
            }
        }
    }
}
