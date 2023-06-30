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
using System.Timers;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using Microsoft.TeamFoundation.Work.WebApi;

namespace CoffeMachine
{
   
    public partial class MobilnoPlacanje : Window
    {
        private System.Timers.Timer timer { get; set; } = default!;
        int br = 0;
        private int cenaPica;
        private int kusur;
        private int unetaCena;
        
        
        public MobilnoPlacanje()
        {
            InitializeComponent();
            SetTimer();
            cenaPica = int.Parse(PlaceOrder.instance.cena.Text);
            
        }
        private void SetTimer()
        {
            timer = new System.Timers.Timer(3000);
            timer.Elapsed += TimerTick;
        }

        private void TimerTick(object? sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (br == 0)
                {
                    imgQR.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\qr code1.png", UriKind.Absolute));
                }
                else if (br == 1)
                {
                    imgQR.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\qr code2.png", UriKind.Absolute));
                }
                else if (br == 2)
                {
                    imgQR.Source = new BitmapImage(new Uri(@"C:\Users\PC\source\repos\CoffeMachine\Images\qr code3.png", UriKind.Absolute));
                }

                
                br = (br + 1) % 3;
            });

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private void btnPlati_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbUbaciNovac.Text))
            {
                unetaCena = int.Parse(tbUbaciNovac.Text);
            }
            else
            {
                MessageBox.Show("Niste uneli novac");
            }

            int lastDigit = unetaCena % 10;
            if (unetaCena > 200)
            {
                MessageBox.Show("Aparat ne prima novcanice vece od 200din");
            }
            else
            {
                if (lastDigit > 0)
                {
                    unetaCena = unetaCena - lastDigit;
                }
                if (unetaCena < cenaPica && unetaCena > 0)
                {
                    MessageBox.Show("Niste uneli dovoljno novca unesite jos " + (cenaPica - unetaCena) + " din");
                }
                else
                {
                    kusur = unetaCena - cenaPica;
                    if (lastDigit > 0)
                    {
                        MessageBox.Show("Aparat vam nece vratiti " + lastDigit + " din");
                    }
                    if(unetaCena > 0)
                    {
                        MessageBox.Show("Vas kusur je " + kusur + " din");
                    }
                    
                }
            }
            
            
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                string query = "update Zaradjeno set [Ukupna zarada] = [Ukupna zarada] + @cenaPica where [Ukupna zarada] = [Ukupna zarada]";
                using (SQLiteCommand updateCommand = new SQLiteCommand(query, (SQLiteConnection)cnn))
                {
                    updateCommand.Parameters.AddWithValue("@cenaPica", cenaPica);
                    updateCommand.ExecuteNonQuery();
                }

            }
            Loading loading = new Loading();
            loading.Show();
            this.Hide();


        }
    }
}
