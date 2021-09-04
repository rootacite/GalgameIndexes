using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

using System.Xml.Linq;

namespace GalgameIndexes
{
    public struct GameData
    {
        public string Company;

        public string Subsidiary;

        public string Name;

        public string TranslatedName;

        public string Date;

        public string Translater;

        public string Progress;

        public string NRank;

        public string Rank;

        public string SRank;

        public string[] Tags;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ing_text = "";

        static public List<GameData> Games = new List<GameData>();
        List<GameData> Results = new List<GameData>();
        List<string> Companys = new List<string>();
        List<string> Dates = new List<string>();
        List<string> Trs = new List<string>();
        List<string> Tags = new List<string>();

        static public XDocument Data;
        static public XElement Root;
        
        void AddResultItem(GameData Item, int mode)
        {
            switch (mode)
            {
                case 0:
                    {
                        var item = new ListViewItem() { Content = Item.Name };
                        item.MouseUp += (m, n) => 
                        {
                            Details ds = new Details();
                            ds.TName.Text = Item.TranslatedName;
                            ds.Name.Text = "原名：" + Item.Name;
                            ds.Date.Text = "发售日期：" + Item.Date;
                            ds.Company.Text = "公司：" + Item.Company;
                            ds.Tran.Text = "汉化组：" + Item.Translater;
                            ds.Rank.Text = "评分：" + Item.Rank;

                            foreach (var i in Item.Tags)
                            {
                                ds.TagList.Children.Add(new Chip() { Content = i, Margin = new Thickness(3, 3, 3, 3) });
                            }

                            ds.self_data = Item;
                            ds.ShowDialog();
                        };
                        Result.Items.Add(item);
                        break;
                    }
                case 1:
                    {
                        var item = new ListViewItem() { Content = Item.Name + "  (" + Item.Date + ")" };
                        item.MouseUp += (m, n) =>
                        {
                            Details ds = new Details();
                            ds.TName.Text = Item.TranslatedName;
                            ds.Name.Text = "原名：" + Item.Name;
                            ds.Date.Text = "发售日期：" + Item.Date;
                            ds.Company.Text = "公司：" + Item.Company;
                            ds.Tran.Text = "汉化组：" + Item.Translater;
                            ds.Rank.Text = "评分：" + Item.Rank;

                            foreach (var i in Item.Tags)
                            {
                                ds.TagList.Children.Add(new Chip() { Content = i, Margin = new Thickness(3, 3, 3, 3) });
                            }

                            ds.self_data = Item;
                            ds.ShowDialog();
                        };
                        Result.Items.Add(item);
                        break;
                    }
                case 2:
                    {
                        var item = new ListViewItem() { Content = Item.Name + "  (" + Item.Rank + ")" };
                        item.MouseUp += (m, n) =>
                        {
                            Details ds = new Details();
                            ds.TName.Text = Item.TranslatedName;
                            ds.Name.Text = "原名：" + Item.Name;
                            ds.Date.Text = "发售日期：" + Item.Date;
                            ds.Company.Text = "公司：" + Item.Company;
                            ds.Tran.Text = "汉化组：" + Item.Translater;
                            ds.Rank.Text = "评分：" + Item.Rank;

                            foreach (var i in Item.Tags)
                            {
                                ds.TagList.Children.Add(new Chip() { Content = i, Margin = new Thickness(3, 3, 3, 3) });
                            }
                            ds.self_data = Item;
                            ds.ShowDialog();
                        };
                        Result.Items.Add(item);
                        break;
                    }
                case 3:
                    {
                        var item = new ListViewItem() { Content = Item.Name + "  (" + Item.TranslatedName + ")" };
                        item.MouseUp += (m, n) =>
                        {
                            Details ds = new Details();
                            ds.TName.Text = Item.TranslatedName;
                            ds.Name.Text = "原名：" + Item.Name;
                            ds.Date.Text = "发售日期：" + Item.Date;
                            ds.Company.Text = "公司：" + Item.Company;
                            ds.Tran.Text = "汉化组：" + Item.Translater;
                            ds.Rank.Text = "评分：" + Item.Rank;

                            foreach (var i in Item.Tags)
                            {
                                ds.TagList.Children.Add(new Chip() { Content = i, Margin = new Thickness(3, 3, 3, 3) });
                            }
                            ds.self_data = Item;
                            ds.ShowDialog();
                        };
                        if (ing_text.Length > 0)
                        {
                            int index_sr = (item.Content as string).IndexOf(ing_text);
                            if (index_sr != -1)
                            {
                                if (index_sr == 0)
                                {
                                    Run c_str = new Run((item.Content as string).Substring(0, ing_text.Length));
                                    c_str.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                                    Run c_str2 = new Run((item.Content as string).Substring(ing_text.Length, (item.Content as string).Length - ing_text.Length));
                                    c_str2.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                                    item.Content = new TextBlock();

                                    (item.Content as TextBlock).Inlines.Add(c_str);
                                    (item.Content as TextBlock).Inlines.Add(c_str2);
                                }
                                else

                                if (index_sr + ing_text.Length == (item.Content as string).Length - 1)
                                {
                                    Run c_str = new Run((item.Content as string).Substring(0, (item.Content as string).Length - ing_text.Length -1));
                                    c_str.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                                    Run c_str2 = new Run((item.Content as string).Substring(index_sr, ing_text.Length));
                                    c_str2.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                                    item.Content = new TextBlock();

                                    (item.Content as TextBlock).Inlines.Add(c_str);
                                    (item.Content as TextBlock).Inlines.Add(c_str2);
                                }
                                else
                                {
                                    Run c_str = new Run((item.Content as string).Substring(0, index_sr));
                                    c_str.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                                    Run c_str2 = new Run((item.Content as string).Substring(index_sr, ing_text.Length));
                                    c_str2.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));

                                    Run c_str3 = new Run((item.Content as string).Substring(index_sr + ing_text.Length, (item.Content as string).Length - ing_text.Length - index_sr));
                                    c_str3.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                                    item.Content = new TextBlock();

                                    (item.Content as TextBlock).Inlines.Add(c_str);
                                    (item.Content as TextBlock).Inlines.Add(c_str2);
                                    (item.Content as TextBlock).Inlines.Add(c_str3);
                                }
                            }
                        }
                        Result.Items.Add(item);
                        break;
                    }
            }
        }

        void FlushList()
        {

            switch (SortMode.SelectedIndex)
            {
                case 0:
                    Results.Sort((a, b) => {

                        return a.TranslatedName.CompareTo(b.TranslatedName);
                    });
                    break;
                case 1:
                    Results.Sort((a, b) => {

                        return a.Date.CompareTo(b.Date);
                    });
                    Results.Reverse();
                    break;
                case 2:
                    Results.Sort((a, b) => {

                        return a.Rank.CompareTo(b.Rank);
                    });
                    Results.Reverse();
                    break;
                case 3:
                    Results.Sort((a, b) => {

                        return a.Name.CompareTo(b.Name);
                    });
                    break;
            }

            
            if (ClassicModeCheckBox.IsChecked == true)
            {
                Results.Reverse();
            }

            Result.Items.Clear();
            foreach (var i in Results)
            {
                if (i.TranslatedName.Contains("暂无译名"))
                {
                    switch (SortMode.SelectedIndex)
                    {
                        case 0:
                            {
                                AddResultItem(i, 0);
                                break;
                            }
                        case 1:
                            {
                                AddResultItem(i, 1);
                                break;
                            }
                        case 2:
                            {
                                AddResultItem(i, 2);
                                break;
                            }
                        case 3:
                            {
                                AddResultItem(i, 3);
                                break;
                            }
                    }
                }
                else
                {
                    switch (SortMode.SelectedIndex)
                    {
                        case 0:
                            {
                                AddResultItem(i, 0);
                                break;
                            }
                        case 1:
                            {
                                AddResultItem(i, 1);
                                break;
                            }
                        case 2:
                            {
                                AddResultItem(i, 2);
                                break;
                            }
                        case 3:
                            {
                                AddResultItem(i, 3);
                                break;
                            }
                    }
                }
            }
            ResultCount.Text = "找到了" + Results.Count.ToString() + "个结果。";
        }
        static public MainWindow Self;
        public void Reload()
        {
            var Load_Results = new List<GameData>();
            Load_Results.AddRange(Results);

            Games.Clear();
            Results.Clear();
            Companys.Clear();
            Dates.Clear();
            Trs.Clear();
            Tags.Clear();
            FromCompany.Items.Clear();
            FromDate.Items.Clear();
            FromTags.Items.Clear();
            FromTran.Items.Clear();

            foreach (XElement i in Root.Nodes())
            {
                string company = i.Element("Company").Value;
                string date = i.Element("Date").Value;
                string tr = i.Element("Translater").Value;
                string[] tags = new string[i.Element("Tags").Nodes().Count()];
                int nds = 0;
                foreach (XElement q in i.Element("Tags").Nodes())
                {
                    tags[nds] = q.Value;
                    nds++;
                }

                date = date.Split('/')[0];
                bool nones = true;
                foreach (var j in Companys)
                {
                    if (j == company)
                    {
                        nones = false;
                        break;
                    }
                }
                if (nones)
                {
                    Companys.Add(company);
                }
                nones = true;
                foreach (var j in Dates)
                {
                    if (j == date)
                    {
                        nones = false;
                        break;
                    }
                }
                if (nones)
                {
                    Dates.Add(date);
                }
                nones = true;
                foreach (var j in Trs)
                {
                    if (j == tr || tr.Contains("默示") || tr.Contains("心愿屋") || tr.Contains("胖次") || tr.Contains("黙示"))
                    {
                        nones = false;
                        break;
                    }
                }
                if (nones)
                {
                    Trs.Add(tr);
                }



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


                var nData = new GameData()
                {
                    Company = i.Element("Company").Value,
                    Date = i.Element("Date").Value,
                    TranslatedName = i.Element("TranslatedName").Value,
                    Name = i.Element("Name").Value,
                    NRank = i.Element("NRank").Value,
                    Rank = i.Element("Rank").Value,
                    Tags = new string[i.Element("Tags").Nodes().Count()],
                    Progress = i.Element("Progress").Value,
                    Subsidiary = i.Element("Subsidiary").Value,
                    SRank = i.Element("SRank").Value,
                    Translater = i.Element("Translater").Value
                };
                int nd = 0;
                foreach (XElement q in i.Element("Tags").Nodes())
                {
                    nData.Tags[nd] = q.Value;
                    nd++;
                }
                Games.Add(nData);


            }

            Results.AddRange(Load_Results);
            Companys.Sort(string.CompareOrdinal);
            Dates.Sort(string.CompareOrdinal);
            Trs.Sort(string.CompareOrdinal);

            foreach (var i in Companys)
            {
                var item = new ListViewItem() { Content = i };
                item.MouseUp += (s, c) =>
                {
                    Results.Clear();
                    foreach (var j in Games)
                    {
                        if (j.Company == i)
                            Results.Add(j);
                    }
                    FlushList();
                };
                FromCompany.Items.Add(item);
            }
            foreach (var i in Dates)
            {
                var item = new ListViewItem() { Content = i };
                item.MouseUp += (s, c) =>
                {
                    Results.Clear();
                    foreach (var j in Games)
                    {
                        if (j.Date.Split('/')[0] == i)
                            Results.Add(j);
                    }
                    FlushList();
                };
                FromDate.Items.Add(item);
            }
            foreach (var i in Trs)
            {
                var item = new ListViewItem() { Content = i };
                item.MouseUp += (n, m) =>
                {
                    Results.Clear();
                    foreach (var j in Games)
                    {
                        if (j.Translater == i)
                            Results.Add(j);
                    }
                    FlushList();
                };
                FromTran.Items.Add(item);
            }

            foreach (var i in Tags)
            {
                var item = new ListViewItem() { Content = i };
                item.MouseUp += (n, m) =>
                {
                    Results.Clear();
                    foreach (var j in Games)
                    {
                        foreach (var v in j.Tags)
                        {
                            if (i == v)
                            {
                                Results.Add(j);
                                break;
                            }
                        }
                    }
                    FlushList();
                };
                FromTags.Items.Add(item);
            }


            FlushList();
        }

        public MainWindow()
        {
           
               InitializeComponent();

            Loaded += (e,v) => 
            {
                Self = this;
                System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
                string strName = Asmb.GetName().Name + ".Data.xml";
                Stream ManifestStream = Asmb.GetManifestResourceStream(strName);
                if (ManifestStream == null)
                {
                    MessageBox.Show("Invaild Data");
                    Close();
                    return;
                }
                Data = XDocument.Load(ManifestStream);
                Root = Data.Root;



                foreach (XElement i in Root.Nodes())
                {
                    string company = i.Element("Company").Value;
                    string date = i.Element("Date").Value;
                    string tr = i.Element("Translater").Value;
                    string[] tags = new string[i.Element("Tags").Nodes().Count()];
                    int nds = 0;
                    foreach (XElement q in i.Element("Tags").Nodes())
                    {
                        tags[nds] = q.Value;
                        nds++;
                    }

                    date = date.Split('/')[0];
                    bool nones = true;
                    foreach (var j in Companys)
                    {
                        if (j == company) { 
                            nones = false;
                            break;
                        }
                    }
                    if (nones)
                    {
                        Companys.Add(company);
                    }
                    nones = true;
                    foreach (var j in Dates)
                    {
                        if (j == date)
                        {
                            nones = false;
                            break;
                        }
                    }
                    if (nones)
                    {
                        Dates.Add(date);
                    }
                    nones = true;
                    foreach (var j in Trs)
                    {
                        if (j == tr || tr.Contains("默示") || tr.Contains("心愿屋") || tr.Contains("胖次") || tr.Contains("黙示"))
                        {
                            nones = false;
                            break;
                        }
                    }
                    if (nones)
                    {
                        Trs.Add(tr);
                    }


                   
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


                    var nData = new GameData()
                    {
                        Company = i.Element("Company").Value,
                        Date = i.Element("Date").Value,
                        TranslatedName = i.Element("TranslatedName").Value,
                        Name = i.Element("Name").Value,
                        NRank = i.Element("NRank").Value,
                        Rank = i.Element("Rank").Value,
                        Tags = new string[i.Element("Tags").Nodes().Count()],
                        Progress = i.Element("Progress").Value,
                        Subsidiary = i.Element("Subsidiary").Value,
                        SRank = i.Element("SRank").Value,
                        Translater = i.Element("Translater").Value
                    };
                    int nd = 0;
                    foreach (XElement q in i.Element("Tags").Nodes())
                    {
                        nData.Tags[nd] = q.Value;
                        nd++;
                    }
                    Games.Add(nData);


                }

                Results.AddRange(Games);
                Companys.Sort(string.CompareOrdinal);
                Dates.Sort(string.CompareOrdinal);
                Trs.Sort(string.CompareOrdinal);

                foreach (var i in Companys)
                {
                    var item = new ListViewItem() { Content = i };
                    item.MouseUp += (s, c) =>
                    {
                        Results.Clear();
                        foreach (var j in Games)
                        {
                            if (j.Company == i)
                                Results.Add(j);
                        }
                        FlushList();
                    };
                    FromCompany.Items.Add(item);
                }
                foreach (var i in Dates)
                {
                    var item = new ListViewItem() { Content = i };
                    item.MouseUp += (s, c) =>
                    {
                        Results.Clear();
                        foreach (var j in Games)
                        {
                            if (j.Date.Split('/')[0] == i)
                                Results.Add(j);
                        }
                        FlushList();
                    };
                    FromDate.Items.Add(item);
                }
                foreach (var i in Trs)
                {
                    var item = new ListViewItem() { Content = i };
                    item.MouseUp += (n, m) =>
                    {
                        Results.Clear();
                        foreach (var j in Games)
                        {
                            if (j.Translater == i)
                                Results.Add(j);
                        }
                        FlushList();
                    };
                    FromTran.Items.Add(item);
                }

                foreach (var i in Tags)
                {
                    var item = new ListViewItem() { Content = i };
                    item.MouseUp += (n, m) =>
                    {
                        Results.Clear();
                        foreach (var j in Games)
                        {
                            foreach (var v in j.Tags)
                            {
                                if (i == v)
                                {
                                    Results.Add(j);
                                    break;
                                }
                            }
                        }
                        FlushList();
                    };
                    FromTags.Items.Add(item);
                }
                FlushList();
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        bool first = true;
        private void ClassicModeCheckBox_Click(object sender, RoutedEventArgs e)
        {
            FlushList();
        }
        bool ingore_sel = false;
        private void SortMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ingore_sel)
            { ingore_sel = false; return; }
            if (SortMode.SelectedIndex == 2 && first)
            {
                first = false;
                MessageBox.Show("部分作品的评分为0是因为样本数不足(无人评分)，并不能说明这个作品本身很糟糕！\n评分数据来源于网络，并非作者主观评价。", "温馨提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            FlushList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SortMode.SelectedIndex != 3) 
            ingore_sel = true;

            ing_text = SarchIndex.Text;
            SortMode.SelectedIndex = 3;

            Results.Clear();
            foreach (var i in Games)
            {
                if(i.Name.Contains(ing_text)|| i.TranslatedName.Contains(ing_text))
                {
                    Results.Add(i);
                }
            }

            FlushList();
        }
    }
}
