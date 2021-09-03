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
        List<GameData> Games = new List<GameData>();
        List<GameData> Results = new List<GameData>();
        List<string> Companys = new List<string>();
        List<string> Dates = new List<string>();
        List<string> Trs = new List<string>();

        XDocument Data;
        XElement Root;
        void FlushList()
        {
            Result.Items.Clear();
            foreach(var i in Results)
            {
                var item = new ListViewItem() { Content = i.Name + "("+ i.TranslatedName + ")"};
                Result.Items.Add(item);
            }
            ResultCount.Text = "找到了" + Results.Count.ToString() + "个结果。";
        }


        public MainWindow()
        {
           
               InitializeComponent();

            Loaded += (e,v) => 
            {
                System.Reflection.Assembly Asmb = System.Reflection.Assembly.GetExecutingAssembly();
                string strName = Asmb.GetName().Name + ".Data.xml";
                Stream? ManifestStream = Asmb.GetManifestResourceStream(strName);
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
                        if (j == tr)
                        {
                            nones = false;
                            break;
                        }
                    }
                    if (nones)
                    {
                        Trs.Add(tr);
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
                FlushList();
            };
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       

    }
}
