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
using System.Xml;

namespace Proiect_bun
{
    /// <summary>
    /// Interaction logic for Finish.xaml
    /// </summary>
    public partial class Finish : Window
    {
        public string UserName { get; set; }
        public string Difficulty { get; set; }


        public Finish()
        {
            InitializeComponent();
            DataContext = this;
        }
        private void Finish_Window_Loaded(object sender, RoutedEventArgs e)
        {
            labelUserName.Content = UserName;
            labelDifficulty.Content = Difficulty;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
       

    }
}
