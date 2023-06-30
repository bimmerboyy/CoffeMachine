using Microsoft.VisualStudio.Services.Users;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
using System.Configuration;

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for UplatiKarticom.xaml
    /// </summary>
    public partial class UplatiKarticom : Window
    {
        private int cenaPica;
        private int kusur;
        private int unetaCena;
        public UplatiKarticom()
        {
            InitializeComponent();
            cenaPica = int.Parse(PlaceOrder.instance.cena.Text);
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }


        private void btnPlati_Click(object sender, RoutedEventArgs e)
        {
            
            if(!string.IsNullOrEmpty(tbUbaciNovac.Text) && !string.IsNullOrEmpty(tbPin.Password))
            {
                unetaCena = int.Parse(tbUbaciNovac.Text);
            }
            else
            {
                MessageBox.Show("Niste uneli novac ili pin");
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
