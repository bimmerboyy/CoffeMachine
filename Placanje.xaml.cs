using System;
using System.Collections.Generic;
using System.Configuration;
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
using Dapper;

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for Placanje.xaml
    /// </summary>
    public partial class Placanje : Window
    {
      
       
        public Placanje()
        {
            InitializeComponent();
            
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        private void btnKes_Click(object sender, RoutedEventArgs e)
        {
         
            UplataKesom uk = new UplataKesom();
            uk.Show();
            this.Hide();


        }
       

        private void btnKartica_Click(object sender, RoutedEventArgs e)
        {
          
            UplatiKarticom uplatiKarticom = new UplatiKarticom();
            uplatiKarticom.Show();
            this.Hide();
        }

        private void btnMobilnoPlacanje_Click(object sender, RoutedEventArgs e)
        {
            MobilnoPlacanje mp = new MobilnoPlacanje();
            mp.Show();
            this.Hide();
        }
    }
}
