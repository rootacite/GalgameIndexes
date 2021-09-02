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

namespace GalgameIndexes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (e,v) => 
            {
                for (int i = 0; i < 1000; i++)
                {
                    var cont = new ListViewItem() { Content = "rr" };
                    cont.MouseLeftButtonUp += (s, c) =>
                    {
                        MessageBox.Show("");
                    };
                    Result.Items.Add(cont);
                }
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       

    }
}
