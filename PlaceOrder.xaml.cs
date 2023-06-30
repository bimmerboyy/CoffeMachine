using Dapper;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using Microsoft.VisualStudio.Services.Common;
using Prism.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for PlaceOrder.xaml
    /// </summary>
    public partial class PlaceOrder : Window
    {
        private int brojMleka = 0;
        private int brojSecera = 0;
        private int brMalih = 0;
        private int brSrednjih = 0;
        private int brVelikih = 0;
        public static PlaceOrder? instance;
        public TextBox cena;
        private Label valueLabelMakijato { get; set; }
        private Button naruciMakijato { get; set;}
        private Label valueLabelEspreso { get; set; }
        private Button naruciEspreso { get; set; }
        private Label valueLabelEspresoSaMlekom { get; set; }
        private Button naruciEspresoSaMlekom { get; set; }
        private Label valueLabelKapucino { get; set; }
        private Button naruciKapucino { get; set; }
        private Label valueLabelToplaCokolada { get; set; }
        private Button naruciToplaCokolada { get; set; }
        private Label valueLabelIrskiKapucino { get; set; }
        private Button naruciIrskiKapucino { get; set; }
        private Label valueLabelTurskaKafa { get; set; }
        private Button naruciTurskaKafa { get; set; }
        private Label valueLabelNescafe { get; set; }
        private Button naruciNescafe { get; set; }
        private Label valueLabelMalinada { get; set; }
        private Button naruciMalinada { get; set; }
        private Label valueLabelCaj { get; set; }
        private Button naruciCaj { get; set; }
        private Button btnPlati { get; set; }
        private Button btnIzmeni {get ; set; }
        private int StackPanelId { get; set; }
        private int izbrisanoMesto { get; set; }
        ComboBox cbIzbrisi { get; set; }
        ComboBox cbDodaj { get; set; }
        Label meniLabel { get; set; }
        private int cenaPica = 0;
        private int cenaMakijata = 0;
        private int cenaEspresa = 0;
        private int cenaEspresaSaMlekom = 0;
        private int cenaKapucina = 0;
        private int cenaTopleCokolade = 0;
        private int cenaIrskogKapucina = 0;
        private int cenaTurskeKafe = 0;
        private int cenaNescafe = 0;
        private int cenaMalinada = 0;
        private int cenaCaj = 0;
        private int selektovanaMalinada = 0;
        private int selektovaniCaj = 0;
        private bool dodajNaMakijatoCaj = false;
        private bool dodajNaEspresoCaj = false;
        private bool dodajNaEspresoSaMlekomCaj = false;
        private bool dodajNaKapucinoCaj = false;
        private bool dodajNaIrskiKapucinoCaj = false;
        private bool dodajNaTopluCokoladuCaj = false;
        private bool dodajNaTurskuKafuCaj = false;
        private bool dodajNaNescafeCaj = false;

        private bool dodajNaMakijatoMalinadu = false;
        private bool dodajNaEspresoMalinadu = false;
        private bool dodajNaEspresoSaMlekomMalinadu = false;
        private bool dodajNaKapucinoMalinadu = false;
        private bool dodajNaIrskiKapucinoMalinadu = false;
        private bool dodajNaTopluCokoladuMalinadu = false;
        private bool dodajNaTurskuKafuMalinadu = false;
        private bool dodajNaNescafeMalinadu = false;


        public PlaceOrder()
        {
            InitializeComponent();
            DodajUCombobox();
            AddCoffe();
            UpdateUI();
            BottomPart();
            brMleka.Content = 0;
            brSecera.Content = 0;
            instance = this;
            cena = konacnaCena;
            
        }
        List<CoffeItem> coffeMenu = new List<CoffeItem>();
        public void AddCoffe()
        {
            CoffeItem item1 = new CoffeItem { Name = "Makijato",Id = 1};
            coffeMenu.Add(item1);

            CoffeItem item2 = new CoffeItem { Name = "Espreso", Id = 2};
            coffeMenu.Add(item2);

            CoffeItem item3 = new CoffeItem {Name = "Espreso sa mlekom", Id = 3};
            coffeMenu.Add(item3);

            CoffeItem item4 = new CoffeItem { Name = "Kapucino",Id = 4};
            coffeMenu.Add(item4);

            CoffeItem item5 = new CoffeItem { Name = "Topla čokolada",Id = 5};
            coffeMenu.Add(item5);

            CoffeItem item6 = new CoffeItem { Name = "Irski kapućino",Id = 6};
            coffeMenu.Add(item6);

            CoffeItem item7 = new CoffeItem { Name = "Turska kafa",Id = 7};
            coffeMenu.Add(item7);

            CoffeItem item8 = new CoffeItem { Name = "Nescafe",Id = 8};
            coffeMenu.Add(item8);

            CoffeItem item9 = new CoffeItem { Name = "Malinada", Id = 9};
            coffeMenu.Add(item9);

            CoffeItem item10 = new CoffeItem { Name = "Čaj", Id = 10 };
            coffeMenu.Add(item10);
        }
        public void RemoveUI()
        {
            makijatoStackPanel.Children.Clear();
            espresoStackPanel.Children.Clear();
            espresoSaMlekomStackPanel.Children.Clear();
            kapucinoStackPanel.Children.Clear();
            toplaCokoladaStackPanel.Children.Clear();
            irskiKapucinoStackPanel.Children.Clear();
            turksaKafaStackPanel.Children.Clear();
            nescafeStackPanel.Children.Clear();
        }
        public void cbZaBrisanje()
        {
            cbIzbrisi = new ComboBox();
            cbIzbrisi.Width = 200;
            cbIzbrisi.Margin = new Thickness(350,10,0,0);
            ComboBoxItemModel item1 = new ComboBoxItemModel { ID = 1, Name = "Makijato" };
            ComboBoxItemModel item2 = new ComboBoxItemModel { ID = 2, Name = "Espreso" };
            ComboBoxItemModel item3 = new ComboBoxItemModel { ID = 3, Name = "Espreso sa mlekom" };
            ComboBoxItemModel item4 = new ComboBoxItemModel { ID = 4, Name = "Kapucino" };
            ComboBoxItemModel item5 = new ComboBoxItemModel { ID = 5, Name = "Topla cokolada" };
            ComboBoxItemModel item6 = new ComboBoxItemModel { ID = 6, Name = "Irski kapucino" };
            ComboBoxItemModel item7 = new ComboBoxItemModel { ID = 7, Name = "Turska kafa" };
            ComboBoxItemModel item8 = new ComboBoxItemModel { ID = 8, Name = "Nescafe" };
            ComboBoxItemModel item9 = new ComboBoxItemModel { ID = 9, Name = "Malinada" };
            ComboBoxItemModel item10 = new ComboBoxItemModel { ID = 10, Name = "Caj" };

            cbIzbrisi.Items.Add(item1);
            cbIzbrisi.Items.Add(item2);
            cbIzbrisi.Items.Add(item3);
            cbIzbrisi.Items.Add(item4);
            cbIzbrisi.Items.Add(item5);
            cbIzbrisi.Items.Add(item6);
            cbIzbrisi.Items.Add(item7);
            cbIzbrisi.Items.Add(item8);
            cbIzbrisi.Items.Add(item9);
            cbIzbrisi.Items.Add(item10);
           


            cbIzbrisi.SelectionChanged += CbIzbrisi_SelectionChanged;

            cbPart.Children.Add(cbIzbrisi);
        }


        private void CbIzbrisi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbIzbrisi.SelectedItem is ComboBoxItemModel selectedItem)
            {
               
                StackPanelId = selectedItem.ID;
                izbrisanoMesto = selectedItem.ID;
                


            }
        }


        public bool RemoveCoffeeCard()
        {
            // Find the coffee card with the provided cardId
            CoffeItem cardToRemove = coffeMenu.FirstOrDefault(card => card.Id == StackPanelId);

            // Remove the coffee card if found
            if (cardToRemove != null)
            {
                RemoveUI();
                coffeMenu.Remove(cardToRemove);
                UpdateUI();
                
            }
            return true;
           
        }

        public void cbZaDodavanje()
        {
            cbDodaj = new ComboBox();
            cbDodaj.Width = 200;
            cbDodaj.Margin = new Thickness(250, 10, 0, 0);
            ComboBoxItemModel item1 = new ComboBoxItemModel { ID = 9, Name = "Malinada" };
            ComboBoxItemModel item2 = new ComboBoxItemModel { ID = 10, Name = "Čaj" };

            cbDodaj.Items.Add(item1);
            cbDodaj.Items.Add(item2);


            cbDodaj.SelectionChanged += CbDodaj_SelectionChanged;

            cbPart.Children.Add(cbDodaj);
        }

        private void CbDodaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbDodaj.SelectedItem is ComboBoxItemModel selectedItem)
            {
               
                StackPanelId = selectedItem.ID;
                
            }
           
        }

        public void AddNewCoffe()
        {
          
            if (StackPanelId == 9)
            {
                selektovanaMalinada++;
                AddMalinada();
                
                StackPanelId = 0;
            }
            if (StackPanelId == 10)
            {
                selektovaniCaj++;
                StackPanelId = 0;
                AddCaj();
            }
            

        }
        
        public void AddMalinada()
        {
            //Dodavanje malinade
            foreach (CoffeItem coffe in coffeMenu)
            {
                if (coffe.Name == "Malinada")
                {
                    string query = "select Cena from Malinada";
                   
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 20;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgMalinada";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\rasberry.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelMalinada = new Label();

                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }

                    
                    cenaMalinada = cenaPica;
                  
                    valueLabelMalinada.Content = cenaPica;
                    valueLabelMalinada.Name = "cenaMalinada";
                    valueLabelMalinada.FontSize = 18;
                    valueLabelMalinada.FontWeight = FontWeights.Bold;
                    valueLabelMalinada.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelMalinada.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelMalinada);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciMalinada = new Button();
                    naruciMalinada.Name = "btnMalinada";
                    naruciMalinada.Content = "Naruči";
                    try
                    {
                        naruciMalinada.Click += Button_Click_8;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciMalinada.Margin = new Thickness(0, 7, 0, 0);
                    naruciMalinada.Height = 30;
                    naruciMalinada.Width = 100;
                    naruciMalinada.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciMalinada.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciMalinada.FontSize = 18;
                    naruciMalinada.FontWeight = FontWeights.Bold;
                    
                    //dodavanje u glavni Stack panel
                    switch (izbrisanoMesto)
                    {
                        case 1:
                            dodajNaMakijatoCaj = true;
                            makijatoStackPanel.Children.Add(coffeName);
                            makijatoStackPanel.Children.Add(coffeImage);
                            makijatoStackPanel.Children.Add(priceStackPanel);
                            makijatoStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 2:
                            dodajNaEspresoCaj = true;
                            espresoStackPanel.Children.Add(coffeName);
                            espresoStackPanel.Children.Add(coffeImage);
                            espresoStackPanel.Children.Add(priceStackPanel);
                            espresoStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 3:
                            dodajNaEspresoSaMlekomCaj = true;
                            espresoSaMlekomStackPanel.Children.Add(coffeName);
                            espresoSaMlekomStackPanel.Children.Add(coffeImage);
                            espresoSaMlekomStackPanel.Children.Add(priceStackPanel);
                            espresoSaMlekomStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 4:
                            dodajNaKapucinoCaj = true;
                            kapucinoStackPanel.Children.Add(coffeName);
                            kapucinoStackPanel.Children.Add(coffeImage);
                            kapucinoStackPanel.Children.Add(priceStackPanel);
                            kapucinoStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 5:
                            dodajNaTopluCokoladuCaj = true;
                            toplaCokoladaStackPanel.Children.Add(coffeName);
                            toplaCokoladaStackPanel.Children.Add(coffeImage);
                            toplaCokoladaStackPanel.Children.Add(priceStackPanel);
                            toplaCokoladaStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 6:
                            dodajNaIrskiKapucinoCaj = true;
                            irskiKapucinoStackPanel.Children.Add(coffeName);
                            irskiKapucinoStackPanel.Children.Add(coffeImage);
                            irskiKapucinoStackPanel.Children.Add(priceStackPanel);
                            irskiKapucinoStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 7:
                            dodajNaTurskuKafuCaj = true;
                            turksaKafaStackPanel.Children.Add(coffeName);
                            turksaKafaStackPanel.Children.Add(coffeImage);
                            turksaKafaStackPanel.Children.Add(priceStackPanel);
                            turksaKafaStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 8:
                            dodajNaNescafeCaj = true;
                            nescafeStackPanel.Children.Add(coffeName);
                            nescafeStackPanel.Children.Add(coffeImage);
                            nescafeStackPanel.Children.Add(priceStackPanel);
                            nescafeStackPanel.Children.Add(naruciMalinada);
                            break;
                        case 10:
                            if (dodajNaMakijatoMalinadu == true)
                            {
                                dodajNaMakijatoCaj = true;
                                makijatoStackPanel.Children.Add(coffeName);
                                makijatoStackPanel.Children.Add(coffeImage);
                                makijatoStackPanel.Children.Add(priceStackPanel);
                                makijatoStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaEspresoMalinadu == true)
                            {
                                espresoStackPanel.Children.Add(coffeName);
                                espresoStackPanel.Children.Add(coffeImage);
                                espresoStackPanel.Children.Add(priceStackPanel);
                                espresoStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaEspresoSaMlekomMalinadu == true)
                            {
                                espresoSaMlekomStackPanel.Children.Add(coffeName);
                                espresoSaMlekomStackPanel.Children.Add(coffeImage);
                                espresoSaMlekomStackPanel.Children.Add(priceStackPanel);
                                espresoSaMlekomStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaKapucinoMalinadu == true)
                            {
                                kapucinoStackPanel.Children.Add(coffeName);
                                kapucinoStackPanel.Children.Add(coffeImage);
                                kapucinoStackPanel.Children.Add(priceStackPanel);
                                kapucinoStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaTopluCokoladuMalinadu == true)
                            {
                                toplaCokoladaStackPanel.Children.Add(coffeName);
                                toplaCokoladaStackPanel.Children.Add(coffeImage);
                                toplaCokoladaStackPanel.Children.Add(priceStackPanel);
                                toplaCokoladaStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaIrskiKapucinoMalinadu == true)
                            {
                                irskiKapucinoStackPanel.Children.Add(coffeName);
                                irskiKapucinoStackPanel.Children.Add(coffeImage);
                                irskiKapucinoStackPanel.Children.Add(priceStackPanel);
                                irskiKapucinoStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaTurskuKafuMalinadu == true)
                            {
                                turksaKafaStackPanel.Children.Add(coffeName);
                                turksaKafaStackPanel.Children.Add(coffeImage);
                                turksaKafaStackPanel.Children.Add(priceStackPanel);
                                turksaKafaStackPanel.Children.Add(naruciMalinada);
                            }
                            else if (dodajNaNescafeMalinadu == true)
                            {
                                nescafeStackPanel.Children.Add(coffeName);
                                nescafeStackPanel.Children.Add(coffeImage);
                                nescafeStackPanel.Children.Add(priceStackPanel);
                                nescafeStackPanel.Children.Add(naruciMalinada);
                            }

                            break;
                        default:
                            MessageBox.Show("Ne mozete dodati malinadu");
                            break;
                    }
                }
                
            }
        }

       

        public void AddCaj()
        {
            foreach(CoffeItem coffe in coffeMenu)
            {
                if (coffe.Name == "Čaj")
                {
                    string query = "select Cena from Makijato";
                   
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 20;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgCaj";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\tea in cup.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelCaj = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaCaj = cenaPica;
                    valueLabelCaj.Content = cenaPica;
                    valueLabelCaj.Name = "cenaCaj";
                    valueLabelCaj.FontSize = 18;
                    valueLabelCaj.FontWeight = FontWeights.Bold;
                    valueLabelCaj.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelCaj.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelCaj);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciCaj = new Button();
                    naruciCaj.Name = "btnCaj";
                    naruciCaj.Content = "Naruči";
                    try
                    {
                        naruciCaj.Click += Button_Click_9;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciCaj.Margin = new Thickness(0, 7, 0, 0);
                    naruciCaj.Height = 30;
                    naruciCaj.Width = 100;
                    naruciCaj.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciCaj.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciCaj.FontSize = 18;
                    naruciCaj.FontWeight = FontWeights.Bold;

                    switch (izbrisanoMesto)
                    {
                        case 1:
                            dodajNaMakijatoMalinadu = true;
                            makijatoStackPanel.Children.Add(coffeName);
                            makijatoStackPanel.Children.Add(coffeImage);
                            makijatoStackPanel.Children.Add(priceStackPanel);
                            makijatoStackPanel.Children.Add(naruciCaj);
                            break;
                        case 2:
                            dodajNaEspresoMalinadu = true;
                            espresoStackPanel.Children.Add(coffeName);
                            espresoStackPanel.Children.Add(coffeImage);
                            espresoStackPanel.Children.Add(priceStackPanel);
                            espresoStackPanel.Children.Add(naruciCaj);
                            break;
                        case 3:
                            dodajNaEspresoSaMlekomMalinadu = true;
                            espresoSaMlekomStackPanel.Children.Add(coffeName);
                            espresoSaMlekomStackPanel.Children.Add(coffeImage);
                            espresoSaMlekomStackPanel.Children.Add(priceStackPanel);
                            espresoSaMlekomStackPanel.Children.Add(naruciCaj);
                            break;
                        case 4:
                            dodajNaKapucinoMalinadu = true;
                            kapucinoStackPanel.Children.Add(coffeName);
                            kapucinoStackPanel.Children.Add(coffeImage);
                            kapucinoStackPanel.Children.Add(priceStackPanel);
                            kapucinoStackPanel.Children.Add(naruciCaj);
                            break;
                        case 5:
                            dodajNaTopluCokoladuMalinadu = true;
                            toplaCokoladaStackPanel.Children.Add(coffeName);
                            toplaCokoladaStackPanel.Children.Add(coffeImage);
                            toplaCokoladaStackPanel.Children.Add(priceStackPanel);
                            toplaCokoladaStackPanel.Children.Add(naruciCaj);
                            break;
                        case 6:
                            dodajNaIrskiKapucinoMalinadu = true;
                            irskiKapucinoStackPanel.Children.Add(coffeName);
                            irskiKapucinoStackPanel.Children.Add(coffeImage);
                            irskiKapucinoStackPanel.Children.Add(priceStackPanel);
                            irskiKapucinoStackPanel.Children.Add(naruciCaj);
                            break;
                        case 7:
                            dodajNaTurskuKafuMalinadu = true;
                            turksaKafaStackPanel.Children.Add(coffeName);
                            turksaKafaStackPanel.Children.Add(coffeImage);
                            turksaKafaStackPanel.Children.Add(priceStackPanel);
                            turksaKafaStackPanel.Children.Add(naruciCaj);
                            break;
                        case 8:
                            dodajNaNescafeMalinadu = true;
                            nescafeStackPanel.Children.Add(coffeName);
                            nescafeStackPanel.Children.Add(coffeImage);
                            nescafeStackPanel.Children.Add(priceStackPanel);
                            nescafeStackPanel.Children.Add(naruciCaj);
                            break;
                        case 9:
                            if(dodajNaMakijatoCaj == true)
                            {
                                dodajNaMakijatoCaj = true;
                                makijatoStackPanel.Children.Add(coffeName);
                                makijatoStackPanel.Children.Add(coffeImage);
                                makijatoStackPanel.Children.Add(priceStackPanel);
                                makijatoStackPanel.Children.Add(naruciCaj);
                            }
                           else if(dodajNaEspresoCaj == true)
                            {
                                espresoStackPanel.Children.Add(coffeName);
                                espresoStackPanel.Children.Add(coffeImage);
                                espresoStackPanel.Children.Add(priceStackPanel);
                                espresoStackPanel.Children.Add(naruciCaj);
                            }
                           else if(dodajNaEspresoSaMlekomCaj == true)
                            {
                                espresoSaMlekomStackPanel.Children.Add(coffeName);
                                espresoSaMlekomStackPanel.Children.Add(coffeImage);
                                espresoSaMlekomStackPanel.Children.Add(priceStackPanel);
                                espresoSaMlekomStackPanel.Children.Add(naruciCaj);
                            }
                            else if(dodajNaKapucinoCaj == true)
                            {
                                kapucinoStackPanel.Children.Add(coffeName);
                                kapucinoStackPanel.Children.Add(coffeImage);
                                kapucinoStackPanel.Children.Add(priceStackPanel);
                                kapucinoStackPanel.Children.Add(naruciCaj);
                            }
                            else if(dodajNaTopluCokoladuCaj == true)
                            {
                                toplaCokoladaStackPanel.Children.Add(coffeName);
                                toplaCokoladaStackPanel.Children.Add(coffeImage);
                                toplaCokoladaStackPanel.Children.Add(priceStackPanel);
                                toplaCokoladaStackPanel.Children.Add(naruciCaj);
                            }
                            else if(dodajNaIrskiKapucinoCaj == true)
                            {
                                irskiKapucinoStackPanel.Children.Add(coffeName);
                                irskiKapucinoStackPanel.Children.Add(coffeImage);
                                irskiKapucinoStackPanel.Children.Add(priceStackPanel);
                                irskiKapucinoStackPanel.Children.Add(naruciCaj);
                            }
                            else if(dodajNaTurskuKafuCaj == true)
                            {
                                turksaKafaStackPanel.Children.Add(coffeName);
                                turksaKafaStackPanel.Children.Add(coffeImage);
                                turksaKafaStackPanel.Children.Add(priceStackPanel);
                                turksaKafaStackPanel.Children.Add(naruciCaj);
                            }
                            else if (dodajNaNescafeCaj == true)
                            {
                                nescafeStackPanel.Children.Add(coffeName);
                                nescafeStackPanel.Children.Add(coffeImage);
                                nescafeStackPanel.Children.Add(priceStackPanel);
                                nescafeStackPanel.Children.Add(naruciCaj);
                            }
                            
                            break;
                        default:
                            MessageBox.Show("Ne mozete dodati caj");
                            break;
                    }
                }
            }
        }



        public void UpdateUI()
        {
            meniLabel = new Label();
            meniLabel.Content = "STANDARDNI MENI";
            meniLabel.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            meniLabel.FontSize = 30;
            meniLabel.FontWeight = FontWeights.Bold;
            meniStackPanel.Children.Add(meniLabel);

            foreach (CoffeItem coffe in coffeMenu)
            {
                //Dodavanje Makijata u UI
              if(coffe.Name == "Makijato")
                {
                    string query = "select Cena from Makijato";
                    
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 20;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgMakijato";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\machiato in cup.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30,0,0,0);
                    //vrednost
                    valueLabelMakijato = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaMakijata = cenaPica;
                    valueLabelMakijato.Content = cenaPica;
                    valueLabelMakijato.Name = "cenaMakijato";
                    valueLabelMakijato.FontSize = 18;
                    valueLabelMakijato.FontWeight= FontWeights.Bold;
                    valueLabelMakijato.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelMakijato.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelMakijato);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciMakijato = new Button();
                    naruciMakijato.Name = "btnMakijato";
                    naruciMakijato.Content = "Naruči";
                    try
                    {
                        naruciMakijato.Click += Button_Click;
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciMakijato.Margin = new Thickness(0,7,0,0);
                    naruciMakijato.Height = 30;
                    naruciMakijato.Width = 100;
                    naruciMakijato.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciMakijato.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciMakijato.FontSize = 18;
                    naruciMakijato.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                    
                    makijatoStackPanel.Children.Add(coffeName);
                    makijatoStackPanel.Children.Add(coffeImage);
                    makijatoStackPanel.Children.Add(priceStackPanel);
                    makijatoStackPanel.Children.Add(naruciMakijato);

                }
                
              //Dodavanje espresa u UI
                if (coffe.Name == "Espreso")
                {

                    string query = "select Cena from Espreso";
                  
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 20;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgEspreso";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\espresso in cup.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelEspreso = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaEspresa = cenaPica;
                    valueLabelEspreso.Content = cenaPica;
                    valueLabelEspreso.Name = "cenaEspreso";
                    valueLabelEspreso.FontSize = 18;
                    valueLabelEspreso.FontWeight = FontWeights.Bold;
                    valueLabelEspreso.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelEspreso.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelEspreso);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciEspreso = new Button();
                    naruciEspreso.Name = "btnEspreso";
                    naruciEspreso.Content = "Naruči";
                    try
                    {
                        naruciEspreso.Click += Button_Click_1;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciEspreso.Margin = new Thickness(0, 7, 0, 0);
                    naruciEspreso.Height = 30;
                    naruciEspreso.Width = 100;
                    naruciEspreso.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciEspreso.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciEspreso.FontSize = 18;
                    naruciEspreso.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel
                    
                    espresoStackPanel.Children.Add(coffeName);
                    espresoStackPanel.Children.Add(coffeImage);
                    espresoStackPanel.Children.Add(priceStackPanel);
                    espresoStackPanel.Children.Add(naruciEspreso);

                }

                //Dodavanje espersa sa mlekom
                if (coffe.Name == "Espreso sa mlekom")
                {

                    string query = "select Cena from [Espreso sa mlekom]";
                    
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgEspresoSaMlekom";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\espresso with milk.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelEspresoSaMlekom = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaEspresaSaMlekom = cenaPica;
                    valueLabelEspresoSaMlekom.Content = cenaPica;
                    valueLabelEspresoSaMlekom.Name = "cenaEspresoSaMlekom";
                    valueLabelEspresoSaMlekom.FontSize = 18;
                    valueLabelEspresoSaMlekom.FontWeight = FontWeights.Bold;
                    valueLabelEspresoSaMlekom.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelEspresoSaMlekom.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelEspresoSaMlekom);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciEspresoSaMlekom = new Button();
                    naruciEspresoSaMlekom.Name = "btnEspresoSaMlekom";
                    naruciEspresoSaMlekom.Content = "Naruči";
                    try
                    {
                        naruciEspresoSaMlekom.Click += Button_Click_2;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciEspresoSaMlekom.Margin = new Thickness(0, 7, 0, 0);
                    naruciEspresoSaMlekom.Height = 30;
                    naruciEspresoSaMlekom.Width = 100;
                    naruciEspresoSaMlekom.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciEspresoSaMlekom.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciEspresoSaMlekom.FontSize = 18;
                    naruciEspresoSaMlekom.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                   
                    espresoSaMlekomStackPanel.Children.Add(coffeName);
                    espresoSaMlekomStackPanel.Children.Add(coffeImage);
                    espresoSaMlekomStackPanel.Children.Add(priceStackPanel);
                    espresoSaMlekomStackPanel.Children.Add(naruciEspresoSaMlekom);

                }

                //Dodavanje kapucina
                if (coffe.Name == "Kapucino")
                {
                    string query = "select Cena from Kapucino";
                   

                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgKapucino";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\cappuccino.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelKapucino = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaIrskogKapucina = cenaPica;
                    valueLabelKapucino.Content = cenaPica;
                    valueLabelKapucino.Name = "cenaKapucino";
                    valueLabelKapucino.FontSize = 18;
                    valueLabelKapucino.FontWeight = FontWeights.Bold;
                    valueLabelKapucino.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelKapucino.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelKapucino);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciKapucino = new Button();
                    naruciKapucino.Name = "btnKapucino";
                    naruciKapucino.Content = "Naruči";
                    try
                    {
                        naruciKapucino.Click += Button_Click_3;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciKapucino.Margin = new Thickness(0, 7, 0, 0);
                    naruciKapucino.Height = 30;
                    naruciKapucino.Width = 100;
                    naruciKapucino.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciKapucino.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciKapucino.FontSize = 18;
                    naruciKapucino.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                   
                    kapucinoStackPanel.Children.Add(coffeName);
                    kapucinoStackPanel.Children.Add(coffeImage);
                    kapucinoStackPanel.Children.Add(priceStackPanel);
                    kapucinoStackPanel.Children.Add(naruciKapucino);

                }
                //Dodavanje Tople cokolade
                if (coffe.Name == "Topla čokolada")
                {
                    string query = "select Cena from [Topla cokolada]";
                    
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgToplaCokolada";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\hot chocolate.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelToplaCokolada = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaTopleCokolade = cenaPica;
                    valueLabelToplaCokolada.Content = cenaPica;
                    valueLabelToplaCokolada.Name = "cenaKapucino";
                    valueLabelToplaCokolada.FontSize = 18;
                    valueLabelToplaCokolada.FontWeight = FontWeights.Bold;
                    valueLabelToplaCokolada.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelToplaCokolada.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelToplaCokolada);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciToplaCokolada = new Button();
                    naruciToplaCokolada.Name = "btnToplaCokolada";
                    naruciToplaCokolada.Content = "Naruči";
                    try
                    {
                        naruciToplaCokolada.Click += Button_Click_4;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciToplaCokolada.Margin = new Thickness(0, 7, 0, 0);
                    naruciToplaCokolada.Height = 30;
                    naruciToplaCokolada.Width = 100;
                    naruciToplaCokolada.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciToplaCokolada.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciToplaCokolada.FontSize = 18;
                    naruciToplaCokolada.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                    
                    toplaCokoladaStackPanel.Children.Add(coffeName);
                    toplaCokoladaStackPanel.Children.Add(coffeImage);
                    toplaCokoladaStackPanel.Children.Add(priceStackPanel);
                    toplaCokoladaStackPanel.Children.Add(naruciToplaCokolada);

                }
                //Dodavanje irskog kapucina
                if (coffe.Name == "Irski kapućino")
                {

                    string query = "select Cena from [Irski kapucino]";
                 
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgIrskiKapucino";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\irish coffe.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelIrskiKapucino = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaIrskogKapucina = cenaPica;
                    valueLabelIrskiKapucino.Content = cenaPica;
                    valueLabelIrskiKapucino.Name = "cenaIrskiKapucino";
                    valueLabelIrskiKapucino.FontSize = 18;
                    valueLabelIrskiKapucino.FontWeight = FontWeights.Bold;
                    valueLabelIrskiKapucino.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelIrskiKapucino.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelIrskiKapucino);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciIrskiKapucino = new Button();
                    naruciIrskiKapucino.Name = "btnIrskiKapucino";
                    naruciIrskiKapucino.Content = "Naruči";
                    try
                    {
                        naruciIrskiKapucino.Click += Button_Click_5;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciIrskiKapucino.Margin = new Thickness(0, 7, 0, 0);
                    naruciIrskiKapucino.Height = 30;
                    naruciIrskiKapucino.Width = 100;
                    naruciIrskiKapucino.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciIrskiKapucino.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciIrskiKapucino.FontSize = 18;
                    naruciIrskiKapucino.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                    
                    irskiKapucinoStackPanel.Children.Add(coffeName);
                    irskiKapucinoStackPanel.Children.Add(coffeImage);
                    irskiKapucinoStackPanel.Children.Add(priceStackPanel);
                    irskiKapucinoStackPanel.Children.Add(naruciIrskiKapucino);

                }
                //Dodavanje turske kafe 
                if (coffe.Name == "Turska kafa")
                {
                    string query = "select Cena from [Turska Kafa]";
                 

                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgTurskaKafa";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\turkish coffe.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelTurskaKafa = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaTurskeKafe = cenaPica;
                    valueLabelTurskaKafa.Content = cenaPica;
                    valueLabelTurskaKafa.Name = "cenaTurskaKafa";
                    valueLabelTurskaKafa.FontSize = 18;
                    valueLabelTurskaKafa.FontWeight = FontWeights.Bold;
                    valueLabelTurskaKafa.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelTurskaKafa.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelTurskaKafa);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciTurskaKafa = new Button();
                    naruciTurskaKafa.Name = "btnTurskaKafa";
                    naruciTurskaKafa.Content = "Naruči";
                    try
                    {
                        naruciTurskaKafa.Click += Button_Click_6;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciTurskaKafa.Margin = new Thickness(0, 7, 0, 0);
                    naruciTurskaKafa.Height = 30;
                    naruciTurskaKafa.Width = 100;
                    naruciTurskaKafa.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciTurskaKafa.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciTurskaKafa.FontSize = 18;
                    naruciTurskaKafa.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                   
                    turksaKafaStackPanel.Children.Add(coffeName);
                    turksaKafaStackPanel.Children.Add(coffeImage);
                    turksaKafaStackPanel.Children.Add(priceStackPanel);
                    turksaKafaStackPanel.Children.Add(naruciTurskaKafa);

                }
                //Dodavanje nescafea
                if (coffe.Name == "Nescafe")
                {
                    string query = "select Cena from [Nescafe]";
                    
                    //Label za ime
                    Label coffeName = new Label();
                    coffeName.Content = coffe.Name;
                    coffeName.FontSize = 18;
                    coffeName.HorizontalAlignment = HorizontalAlignment.Center;
                    coffeName.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19)); //braon
                    coffeName.FontWeight = FontWeights.Bold;
                    //Slika
                    Image coffeImage = new Image();
                    coffeImage.Name = "imgNescafe";
                    coffeImage.Height = 100;
                    coffeImage.Width = 100;
                    coffeImage.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\nescafe.png", UriKind.Absolute));
                    //Stack panel za cenu
                    StackPanel priceStackPanel = new StackPanel();
                    priceStackPanel.Height = 30;
                    priceStackPanel.Orientation = Orientation.Horizontal;
                    //cena
                    Label cenaLabel = new Label();
                    cenaLabel.Content = "Cena:";
                    cenaLabel.Width = 56;
                    cenaLabel.FontSize = 18;
                    cenaLabel.FontWeight = FontWeights.Bold;
                    cenaLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    cenaLabel.Margin = new Thickness(30, 0, 0, 0);
                    //vrednost
                    valueLabelNescafe = new Label();
                    using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                    {
                        cnn.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                        {
                            SQLiteDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                cenaPica = reader.GetInt32(0);
                            }
                            reader.Close();
                        }
                    }
                    cenaNescafe = cenaPica;
                    valueLabelNescafe.Content = cenaPica;
                    valueLabelNescafe.Name = "cenaNescafe";
                    valueLabelNescafe.FontSize = 18;
                    valueLabelNescafe.FontWeight = FontWeights.Bold;
                    valueLabelNescafe.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    valueLabelNescafe.Width = 30;
                    //din
                    Label dinLabel = new Label();
                    dinLabel.Content = "din";
                    dinLabel.FontSize = 18;
                    dinLabel.FontWeight = FontWeights.Bold;
                    dinLabel.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    dinLabel.Width = 40;
                    //dodavanje u Stack panel za cenu
                    priceStackPanel.Children.Add(cenaLabel);
                    priceStackPanel.Children.Add(valueLabelNescafe);
                    priceStackPanel.Children.Add(dinLabel);

                    //Button Naruci
                    naruciNescafe = new Button();
                    naruciNescafe.Name = "btnNescafe";
                    naruciNescafe.Content = "Naruči";
                    try
                    {
                        naruciNescafe.Click += Button_Click_7;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }

                    naruciNescafe.Margin = new Thickness(0, 7, 0, 0);
                    naruciNescafe.Height = 30;
                    naruciNescafe.Width = 100;
                    naruciNescafe.Background = new SolidColorBrush(Color.FromRgb(76, 20, 19));
                    naruciNescafe.Foreground = new SolidColorBrush(Color.FromRgb(254, 245, 228));
                    naruciNescafe.FontSize = 18;
                    naruciNescafe.FontWeight = FontWeights.Bold;

                    //dodavanje u glavni Stack panel

                   
                    nescafeStackPanel.Children.Add(coffeName);
                    nescafeStackPanel.Children.Add(coffeImage);
                    nescafeStackPanel.Children.Add(priceStackPanel);
                    nescafeStackPanel.Children.Add(naruciNescafe);

                }
            }
        }

        private void BottomPart()
        {

            //dodavanje Plati
            btnPlati = new Button();
            btnPlati.Content = "PLATI";
            btnPlati.Name = "btnPlati";
            btnPlati.Click += btnPlati_Click;
            btnPlati.Margin = new Thickness(70,110,0,0);
            btnPlati.Height = 30;
            btnPlati.Width = 100;
            btnPlati.FontSize = 20;
            btnPlati.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnPlati.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnPlati.FontFamily = new FontFamily("Segoe UI Black");
            btnPlati.FontWeight = FontWeights.Bold;
            btnPlati.BorderThickness = new Thickness(0,0,0,0);
            //dodavanje Izmeni
            btnIzmeni = new Button();
            btnIzmeni.Content = "IZMENI";
            btnIzmeni.Name = "btnIzmeni";
            btnIzmeni.Click += izmeni_Click;
            btnIzmeni.Margin = new Thickness(70, 110, 0, 0);
            btnIzmeni.Height = 30;
            btnIzmeni.Width = 100;
            btnIzmeni.FontSize = 20;
            btnIzmeni.Background = new SolidColorBrush(Color.FromRgb(254, 245, 228));
            btnIzmeni.Foreground = new SolidColorBrush(Color.FromRgb(76, 20, 19));
            btnIzmeni.FontFamily = new FontFamily("Segoe UI Black");
            btnIzmeni.FontWeight = FontWeights.Bold;
            btnIzmeni.BorderThickness = new Thickness(0, 0, 0, 0);

            //dodavanje u stack panel
            bottomPartStackPanel.Children.Add(btnPlati);
            bottomPartStackPanel.Children.Add(btnIzmeni);
        }
       

        private void DodajUCombobox()
        {
           cbVelicina.Items.Add("Mala");
           cbVelicina.Items.Add("Srednja");
           cbVelicina.Items.Add("Velika");
        }
        private string BrojCasaCombobox() 
        {
            string Velicinaquery = string.Empty;

            if ((string)cbVelicina.SelectedItem == "Mala")
            {
                Velicinaquery = "update Zalihe set Mala = Mala - @br where Mala = Mala";
                brMalih++;
            }
            else if((string)cbVelicina.SelectedItem == "Srednja")
            {
                Velicinaquery = "update Zalihe set Srednja = Srednja - @br where Srednja = Srednja";
                brSrednjih++;
            }
            else {
                Velicinaquery = "update Zalihe set Velika = Velika - @br where Velika = Velika";
                brVelikih++;
            }
            return Velicinaquery;
        }

        private void btnMlekoPlus_Click(object sender, RoutedEventArgs e)
        {
            brojMleka++;
            brMleka.Content = brojMleka;
            btnMlekoMinus.IsEnabled = true;

        }

        private void btnMlekoMinus_Click(object sender, RoutedEventArgs e)
        {
            if(brojMleka <= 0)
            {
                btnMlekoMinus.IsEnabled = false;
            }
            else
            {
                brojMleka--;
                brMleka.Content = brojMleka;
            }
        }

        private void btnSecerPlus_Click(object sender, RoutedEventArgs e)
        {
            brojSecera++;
            brSecera.Content = brojSecera;
            btnSecerMinus.IsEnabled = true;
        }

        private void btnSecerMinus_Click(object sender, RoutedEventArgs e)
        {
            if (brojSecera <= 0)
            {
                btnSecerMinus.IsEnabled = false;
            }
            else
            {
                brojSecera--;
                brSecera.Content = brojSecera;
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelMakijato.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            

            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Voda, [Krem za kafu] from Makijato";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query4 = "update Zalihe set [Krem za kafu] = [Krem za kafu] - @kremZaKafu where [Krem za kafu] = [Krem za kafu]";
                string query5 = "select Kafa, Voda, Mleko, Secer, [Krem za kafu] from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                  
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka",brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br",brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br",brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                 
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int mleko = reader.GetInt32(2);
                        int secer = reader.GetInt32(3);
                        int kremZaKafu = reader.GetInt32(4);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciMakijato.IsEnabled = false;
                            MessageBox.Show("Makijato se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (voda == 0)
                        {
                            query3 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciMakijato.IsEnabled = false;
                            MessageBox.Show("Makijato se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (kremZaKafu == 0)
                        {
                            query4 = "update Zalihe set [Krem za kafu] = 0 where [Krem za kafu] = [Krem za kafu]";
                            naruciMakijato.IsEnabled = false;
                            MessageBox.Show("Makijato se ne moze napraviti jer nema krema za kafu u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }
                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }

                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int kremZaKafu = reader.GetInt32(2);
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kremZaKafu", kremZaKafu);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelEspreso.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Voda from Espreso";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query4 = "select Kafa, Voda, Mleko, Secer from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }

                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int mleko = reader.GetInt32(2);
                        int secer = reader.GetInt32(3);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciEspreso.IsEnabled = false;
                            MessageBox.Show("Espreso se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (voda == 0) 
                        {
                            query3 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciEspreso.IsEnabled = false;
                            MessageBox.Show("Espreso se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }
                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);

                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelEspresoSaMlekom.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Voda, Mleko from [Espreso sa mlekom]";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query4 = "update Zalihe set Mleko = Mleko - @mleko where Mleko = Mleko";
                string query5 = "select Kafa, Voda, Mleko, Secer from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int mleko = reader.GetInt32(2);
                        int secer = reader.GetInt32(3);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciEspresoSaMlekom.IsEnabled = false;
                            MessageBox.Show("Espreso sa mlekom se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query4 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            naruciEspresoSaMlekom.IsEnabled = false;
                            MessageBox.Show("Espreso sa mlekom se ne moze napraviti jer nema mleka u zalihama");
                        }
                        else if (voda == 0)
                        {
                            query3 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciEspresoSaMlekom.IsEnabled = false;
                            MessageBox.Show("Espreso sa mlekom se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }
                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int voda = reader.GetInt32(2);

                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@mleko", mleko);
                            updateCommand.ExecuteNonQuery();
                        }

                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelKapucino.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Mleko, [Krem za kafu] from Kapucino";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set [Krem za kafu] = [Krem za kafu] - @kremZaKafu where [Krem za kafu] = [Krem za kafu]";
                string query4 = "update Zalihe set Mleko = Mleko - @mleko where Mleko = Mleko";
                string query5 = "select Kafa, Mleko, [Krem za kafu], Mleko, Secer from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int secer = reader.GetInt32(2);
                        int kremZaKafu = reader.GetInt32(3);
                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciKapucino.IsEnabled = false;
                            MessageBox.Show("Kapucino se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query4 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            naruciKapucino.IsEnabled = false;
                            MessageBox.Show("Kapucino se ne moze napraviti jer nema mleka u zalihama");
                        }
                        else if (kremZaKafu == 0)
                        {
                            query3 = "update Zalihe set [Krem za kafu] = 0 where [Krem za kafu] = [Krem za kafu]";
                            naruciKapucino.IsEnabled = false;
                            MessageBox.Show("Kapucino se ne moze napraviti jer nema krema za kafu u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }
                    }
                    reader.Close();
                }
                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int kremZaKafu = reader.GetInt32(2);

                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kremZaKafu", kremZaKafu);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@mleko", mleko);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelToplaCokolada.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Cokolada, Mleko from [Topla cokolada]";
                string query2 = "update Zalihe set Cokolada = Cokolada - @cokolada where Cokolada = Cokolada";
                string query3 = "update Zalihe set Mleko = Mleko - @mleko where Mleko = Mleko";
                string query4 = "select Mleko, Secer, Cokolada from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int mleko = reader.GetInt32(0);
                        int secer = reader.GetInt32(1);
                        int cokolada = reader.GetInt32(2);

                        if (mleko == 0)
                        {
                            query3 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            naruciToplaCokolada.IsEnabled = false;
                            MessageBox.Show("Topla cokolada se ne moze napraviti jer nema mleka u zalihama");
                        }
                        else if (cokolada == 0)
                        {
                            query2 = "update Zalihe set Cokolada = 0 where Cokolada = Cokolada";
                            naruciToplaCokolada.IsEnabled = false;
                            MessageBox.Show("Topla cokolada se ne moze napraviti jer nema cokolade u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }


                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int cokolada = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);

                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@cokolada", cokolada);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@mleko", mleko);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelIrskiKapucino.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Voda, [Krem za kafu] from [Irski kapucino]";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query4 = "update Zalihe set [Krem za kafu] = [Krem za kafu] - @kremZaKafu where [Krem za kafu] = [Krem za kafu]";
                string query5 = "select Kafa, Voda, Mleko, Secer, [Krem za kafu] from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int mleko = reader.GetInt32(2);
                        int secer = reader.GetInt32(3);
                        int kremZaKafu = reader.GetInt32(4);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciIrskiKapucino.IsEnabled = false;
                            MessageBox.Show("Irski kapucino se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (voda == 0)
                        {
                            query3 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciIrskiKapucino.IsEnabled = false;
                            MessageBox.Show("Irski kapucino se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (kremZaKafu == 0)
                        {
                            query4 = "update Zalihe set [Krem za kafu] = 0 where [Krem za kafu] = [Krem za kafu]";
                            naruciIrskiKapucino.IsEnabled = false;
                            MessageBox.Show("Irski kapucino se ne moze napraviti jer nema krema za kafu u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }


                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int kremZaKafu = reader.GetInt32(2);
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kremZaKafu", kremZaKafu);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelTurskaKafa.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, Voda from [Turska kafa]";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query4 = "select Kafa, Voda, Mleko, Secer from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        int mleko = reader.GetInt32(2);
                        int secer = reader.GetInt32(3);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciTurskaKafa.IsEnabled = false;
                            MessageBox.Show("Turska kafa se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (voda == 0)
                        {
                            query3 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciTurskaKafa.IsEnabled = false;
                            MessageBox.Show("Turska kafa se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }
                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);

                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelNescafe.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Kafa, [Krem za kafu] from Nescafe";
                string query2 = "update Zalihe set Kafa = Kafa - @kafa where Kafa = Kafa";
                string query3 = "update Zalihe set [Krem za kafu] = [Krem za kafu] - @kremZaKafu where [Krem za kafu] = [Krem za kafu]";
                string query4 = "select Kafa, Mleko, Secer, [Krem za kafu] from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int secer = reader.GetInt32(2);
                        int kremZaKafu = reader.GetInt32(3);

                        if (kafa == 0)
                        {
                            query2 = "update Zalihe set Kafa = 0 where Kafa = Kafa";
                            naruciNescafe.IsEnabled = false;
                            MessageBox.Show("Nescafe se ne moze napraviti jer nema kafe u zalihama");
                        }
                        else if (kremZaKafu == 0)
                        {
                            query3 = "update Zalihe set [Krem za kafu] = 0 where [Krem za kafu] = [Krem za kafu]";
                            naruciNescafe.IsEnabled = false;
                            MessageBox.Show("Nescafe se ne moze napraviti jer nema krema za kafu u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }


                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int kafa = reader.GetInt32(0);
                        int kremZaKafu = reader.GetInt32(1);
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kafa", kafa);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@kremZaKafu", kremZaKafu);
                            updateCommand.ExecuteNonQuery();
                        }

                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelMalinada.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Malina, Limun, Voda from Malinada";
                string query2 = "update Zalihe set Malina = Malina - @malina where Malina = Malina";
                string query3 = "update Zalihe set Limun = Limun - @limun where Limun = Limun";
                string query4 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query5 = "select Voda, Mleko, Secer, Malina, Limun from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int voda = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int secer = reader.GetInt32(2);
                        int malina = reader.GetInt32(3);
                        int limun = reader.GetInt32(4);

                        if (voda == 0)
                        {
                            query4 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciMalinada.IsEnabled = false;
                            MessageBox.Show("Malinada se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (malina == 0)
                        {
                            query2 = "update Zalihe set Malina = 0 where Malina = Malina";
                            naruciMalinada.IsEnabled = false;
                            MessageBox.Show("Malinada se ne moze napraviti jer nema malina u zalihama");
                        }
                        else if (limun == 0)
                        {
                            query3 = "update Zalihe set Limun = 0 where Limun = Limun";
                            naruciMalinada.IsEnabled = false;
                            MessageBox.Show("Malinada se ne moze napraviti jer nema limuna u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }


                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int malina = reader.GetInt32(0);
                        int limun = reader.GetInt32(1);
                        int voda = reader.GetInt32(2);
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@malina",malina);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@limun",limun);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query4, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }

                    }
                    reader.Close();
                }
            }
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            string cenaStr = valueLabelCaj.Content.ToString();
            int cenaInt = int.Parse(cenaStr);
            konacnaCena.Text = cenaInt.ToString();
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "select Biljka, Voda from Caj";
                string query2 = "update Zalihe set Voda = Voda - @voda where Voda = Voda";
                string query3 = "update Zalihe set Biljka = Biljka - @biljka where Biljka = Biljka";
                string query5 = "select Voda, Mleko, Secer, Biljka from Zalihe";
                string query6 = "update Zalihe set Mleko = Mleko - @brojMleka where Mleko = Mleko";
                string query7 = "update Zalihe set Secer = Secer - @brojSecera where Secer = Secer";
                string velicinaquery = BrojCasaCombobox();

                using (SQLiteCommand command = new SQLiteCommand(query5, (SQLiteConnection)cnn))
                {
                    using (SQLiteCommand updateCommand = new SQLiteCommand(query6, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojMleka", brojMleka);
                        updateCommand.ExecuteNonQuery();
                    }

                    using (SQLiteCommand updateCommand = new SQLiteCommand(query7, (SQLiteConnection)cnn))
                    {
                        updateCommand.Parameters.AddWithValue("@brojSecera", brojSecera);
                        updateCommand.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand1 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand1.Parameters.AddWithValue("@br", brMalih);
                        updateCommand1.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand2 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand2.Parameters.AddWithValue("@br", brSrednjih);
                        updateCommand2.ExecuteNonQuery();
                    }
                    using (SQLiteCommand updateCommand3 = new SQLiteCommand(velicinaquery, (SQLiteConnection)cnn))
                    {
                        updateCommand3.Parameters.AddWithValue("@br", brVelikih);
                        updateCommand3.ExecuteNonQuery();
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int voda = reader.GetInt32(0);
                        int mleko = reader.GetInt32(1);
                        int secer = reader.GetInt32(2);
                        int biljka = reader.GetInt32(3);

                        if (voda == 0)
                        {
                            query2 = "update Zalihe set Voda = 0 where Voda = Voda";
                            naruciMalinada.IsEnabled = false;
                            MessageBox.Show("Caj se ne moze napraviti jer nema vode u zalihama");
                        }
                        else if (biljka == 0)
                        {
                            query3 = "update Zalihe set Biljka = 0 where Biljka = Biljka";
                            naruciMalinada.IsEnabled = false;
                            MessageBox.Show("Caj se ne moze napraviti jer nema biljke u zalihama");
                        }
                        else if (mleko == 0)
                        {
                            query6 = "update Zalihe set Mleko = 0 where Mleko = Mleko";
                            MessageBox.Show("Nema dovoljno mleka na stanju");
                        }

                        else if (secer == 0)
                        {
                            query7 = "update Zalihe set Secer = 0 where Secer = Secer";
                            MessageBox.Show("Nema dovoljno secera na stanju");
                        }
                    }
                    reader.Close();
                }

                using (SQLiteCommand command = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    SQLiteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int biljka = reader.GetInt32(0);
                        int voda = reader.GetInt32(1);
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query3, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@biljka", biljka);
                            updateCommand.ExecuteNonQuery();
                        }
                        using (SQLiteCommand updateCommand = new SQLiteCommand(query2, (SQLiteConnection)cnn))
                        {
                            updateCommand.Parameters.AddWithValue("@voda", voda);
                            updateCommand.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                }
            }
        }
        private void izmeni_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin(btnIzmeni,btnPlati,bottomPartStackPanel,this,cbIzbrisi,StackPanelId,meniLabel);
            adminLogin.Show();
            this.Hide();
            
        }
        private void btnPlati_Click(object sender, RoutedEventArgs e)
        {
            Placanje p = new Placanje();
            p.Show();
            this.Hide();
        }

        private void cbVelicina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int procenat = 0;
            if(cbVelicina.SelectedIndex == 0)
            {
               
                cenaMakijata = 40;
                cenaEspresa = 30;
                cenaEspresaSaMlekom = 40;
                cenaKapucina = 50;
                cenaTopleCokolade = 50;
                cenaIrskogKapucina = 50;
                cenaTurskeKafe = 40;
                cenaNescafe = 30;
                cenaMalinada = 40;
                cenaCaj = 30;
                valueLabelMakijato.Content = cenaMakijata;
                valueLabelEspreso.Content = cenaEspresa;
                valueLabelEspresoSaMlekom.Content = cenaEspresaSaMlekom;
                valueLabelKapucino.Content = cenaKapucina;
                valueLabelToplaCokolada.Content = cenaTopleCokolade;
                valueLabelIrskiKapucino.Content = cenaIrskogKapucina;
                valueLabelTurskaKafa.Content = cenaTurskeKafe;
                valueLabelNescafe.Content = cenaNescafe;
                if (selektovanaMalinada == 1)
                {

                    valueLabelMalinada.Content = cenaMalinada;
                }
                if (selektovaniCaj == 1)
                {

                    valueLabelCaj.Content = cenaCaj;
                }
            }
          else if (cbVelicina.SelectedIndex == 1)
            {
               
                //Za Makijato
                cenaMakijata = 40;
                procenat = (cenaMakijata * 10) / 100;
                cenaMakijata = procenat + cenaMakijata;
                valueLabelMakijato.Content = cenaMakijata;
                //Za espreso
                cenaEspresa = 30;
                procenat = 0;
                procenat = (cenaEspresa * 10) / 100;
                cenaEspresa = procenat + cenaEspresa;
                valueLabelEspreso.Content = cenaEspresa;
                //Za espreso sa mlekom
                cenaEspresaSaMlekom = 40;
                procenat = 0;
                procenat = (cenaEspresaSaMlekom * 10) / 100;
                cenaEspresaSaMlekom = procenat + cenaEspresaSaMlekom;
                valueLabelEspresoSaMlekom.Content = cenaEspresaSaMlekom;
                //Za Kapucino
                cenaKapucina = 50;
                procenat = 0;
                procenat = (cenaKapucina * 10) / 100;
                cenaKapucina = procenat + cenaKapucina;
                valueLabelKapucino.Content = cenaKapucina;
                //Za Toplu Cokoladu
                cenaTopleCokolade = 50;
                procenat = 0;
                procenat = (cenaTopleCokolade * 10) / 100;
                cenaTopleCokolade = procenat + cenaTopleCokolade;
                valueLabelToplaCokolada.Content = cenaTopleCokolade;
                //Za Irski kapucino
                cenaIrskogKapucina = 50;
                procenat = 0;
                procenat = (cenaIrskogKapucina * 10) / 100;
                cenaIrskogKapucina = procenat + cenaIrskogKapucina;
                valueLabelIrskiKapucino.Content = cenaIrskogKapucina;
                //Za Tursku kafu
                cenaTurskeKafe = 40;
                procenat = 0;
                procenat = (cenaTurskeKafe * 10) / 100;
                cenaTurskeKafe = procenat + cenaTurskeKafe;
                valueLabelTurskaKafa.Content = cenaTurskeKafe;
                //Za Nescafe
                cenaNescafe = 30;
                procenat = 0;
                procenat = (cenaNescafe * 10) / 100;
                cenaNescafe = procenat + cenaNescafe;
                valueLabelNescafe.Content = cenaNescafe;
                //Za Maliadu
                cenaMalinada = 40;
                procenat = 0;
                procenat = (cenaMalinada * 10) / 100;
                cenaMalinada = procenat + cenaMalinada;
                if(selektovanaMalinada == 1)
                {
                   
                    valueLabelMalinada.Content = cenaMalinada;
                }
                //Za Caj
                cenaCaj = 30;
                procenat = 0;
                procenat = (cenaCaj * 10) / 100;
                cenaCaj = procenat + cenaCaj;
                if (selektovaniCaj == 1)
                {

                    valueLabelCaj.Content = cenaCaj;
                }



            }
            else if(cbVelicina.SelectedIndex == 2)
            {
               
                //Za Makijato
                cenaMakijata = 40;
                procenat = (cenaMakijata * 20) / 100;
                cenaMakijata = procenat + cenaMakijata;
                valueLabelMakijato.Content = cenaMakijata;
                //Za espreso
                cenaEspresa = 30;
                procenat = 0;
                procenat = (cenaEspresa * 20) / 100;
                cenaEspresa = procenat + cenaEspresa;
                valueLabelEspreso.Content = cenaEspresa;
                //Za espreso sa mlekom
                cenaEspresaSaMlekom = 40;
                procenat = 0;
                procenat = (cenaEspresaSaMlekom * 20) / 100;
                cenaEspresaSaMlekom = procenat + cenaEspresaSaMlekom;
                valueLabelEspresoSaMlekom.Content = cenaEspresaSaMlekom;
                //Za Kapucino
                cenaKapucina = 50;
                procenat = 0;
                procenat = (cenaKapucina * 20) / 100;
                cenaKapucina = procenat + cenaKapucina;
                valueLabelKapucino.Content = cenaKapucina;
                //Za Toplu Cokoladu
                cenaTopleCokolade = 50;
                procenat = 0;
                procenat = (cenaTopleCokolade * 20) / 100;
                cenaTopleCokolade = procenat + cenaTopleCokolade;
                valueLabelToplaCokolada.Content = cenaTopleCokolade;
                //Za Irski kapucino
                cenaIrskogKapucina = 50;
                procenat = 0;
                procenat = (cenaIrskogKapucina * 20) / 100;
                cenaIrskogKapucina = procenat + cenaIrskogKapucina;
                valueLabelIrskiKapucino.Content = cenaIrskogKapucina;
                //Za Tursku kafu
                cenaTurskeKafe = 40;
                procenat = 0;
                procenat = (cenaTurskeKafe * 20) / 100;
                cenaTurskeKafe = procenat + cenaTurskeKafe;
                valueLabelTurskaKafa.Content = cenaTurskeKafe;
                //Za Nescafe
                cenaNescafe = 30;
                procenat = 0;
                procenat = (cenaNescafe * 20) / 100;
                cenaNescafe = procenat + cenaNescafe;
                valueLabelNescafe.Content = cenaNescafe;
                //Za Malinadu
                cenaMalinada = 40;
                procenat = 0;
                procenat = (cenaMalinada * 20) / 100;
                cenaMalinada = procenat + cenaMalinada;
                if (selektovanaMalinada == 1)
                {
                 
                    valueLabelMalinada.Content = cenaMalinada;
                }
                //Za Caj
                cenaCaj = 30;
                procenat = 0;
                procenat = (cenaCaj * 20) / 100;
                cenaCaj = procenat + cenaCaj;
                if (selektovaniCaj == 1)
                {

                    valueLabelCaj.Content = cenaCaj;
                }

            }
        }
    }
}
