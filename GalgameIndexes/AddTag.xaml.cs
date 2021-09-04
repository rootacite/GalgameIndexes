using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// AddTag.xaml 的交互逻辑
    /// </summary>
    public partial class AddTag : Window
    {
        List<string> Tags = new List<string>();
        public string Text = "";
        public AddTag()
        {
            InitializeComponent();

            foreach (XElement i in MainWindow.Root.Nodes())
            {
                string[] tags = new string[i.Element("Tags").Nodes().Count()];
                int nds = 0;
                foreach (XElement q in i.Element("Tags").Nodes())
                {
                    tags[nds] = q.Value;
                    nds++;
                }

                bool nones = true;
                foreach (var j in tags)
                {
                    nones = true;
                    foreach (var p in Tags)
                    {
                        if (j == p)
                        {
                            nones = false;
                            break;
                        }
                    }

                    if (nones)
                    {
                        Tags.Add(j);
                    }
                }

            }

            foreach (var i in Tags)
            {
                var tool = new Button();
                tool.Content = i;
                tool.Click += (e, v) =>
                {
                    Text = i;
                    Close();
                };
                TagList.Children.Add(tool);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Text = Txt.Text;
                Close();
            }
        }
    }
}
