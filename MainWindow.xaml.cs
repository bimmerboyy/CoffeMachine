
using Microsoft.TeamFoundation.Dashboards.WebApi;
using Microsoft.VisualStudio.Services.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Timers.Timer timer { get; set; } = default!;
        public MainWindow()
        {
            InitializeComponent();
            SetTimer();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            System.Windows.Application.Current.Shutdown();
        }

        private void TextBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow fm = new MainWindow();
            this.Hide();
            fm.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PlaceOrder po = new PlaceOrder();
            po.Show();
            this.Hide();
        }

        private void SetTimer()
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += TimerTick;
        }
        static int num = 0;


        private void TimerTick(object? sender, ElapsedEventArgs e)
        {


            if (num == 0)
            {
                Dispatcher.Invoke(() => {
                    Thickness margin = new Thickness(200, 0, 100, 0);
                    imgCoffeShop.Margin = margin;

                });
                num++;


            }
            else if (num == 1)
            {
                Dispatcher.Invoke(() => {
                    Thickness margin = new Thickness(0, 0, 200, 0);
                    imgCoffeShop.Margin = margin;

                });
                num++;

            }
            else if (num == 2)
            {
                Dispatcher.Invoke(() => {
                    Thickness margin = new Thickness(0, 0, 400, 0);
                    imgCoffeShop.Margin = margin;
                });

                num = 0;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }


    }
        
    }

