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

namespace CoffeMachine
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class Loading : Window
    {
        private System.Timers.Timer timer { get; set; } = default!;
        public Loading()
        {
            InitializeComponent();
            SetTimer();
        }
        private void SetTimer()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += TimerTick;
        }

        private void TimerTick(object sender,EventArgs e) 
        {
            this.Dispatcher.Invoke(() => {
                if (progressBar.Value < 100)
                {
                    progressBar.Value++;

                    lbProcent.Content = progressBar.Value.ToString() + "%";
                }
                else
                {
                    timer.Stop();
                    MessageBox.Show("Vaše piće je spremno.Uživajte!");
                    this.Close();
                }

            });
          
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }
    }
}
