using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace GalgameIndexes
{
    /// <summary>
    /// Details.xaml 的交互逻辑
    /// </summary>
    public partial class Details : Window
    {
        public GameData self_data;
        public Details()
        {
            InitializeComponent();

            Loaded += (e, v) =>
            {
              
            };
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Clipboard.SetDataObject(TName.Text, true);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        { 
#if DEBUG
            if (e.Key == Key.F12)
            {
                AddTag addTag = new AddTag();
                addTag.ShowDialog();

                if (addTag.Text != "")
                {
                    var Root = MainWindow.Root;

                    foreach(XElement i in Root.Nodes())
                    {
                        string name = i.Element("Name").Value;
                        string tname = i.Element("TranslatedName").Value;

                        if (name == self_data.Name && tname == self_data.TranslatedName)
                        {
                            i.Element("Tags").Add(new XElement("Tag") { Value = addTag.Text });
                            break;
                        }
                    }
                    this.TagList.Children.Add(new Chip() { Content = addTag.Text, Margin = new Thickness(3, 3, 3, 3) });
                    MainWindow.Data.Save(@"C:\Users\14980\source\repos\GalgameIndexes\GalgameIndexes\Data.xml");
                    MainWindow.Self.Reload();
                }
            }
#endif
        }
    }
}
