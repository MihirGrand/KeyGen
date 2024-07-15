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
using KeyGen.Classes;

namespace KeyGen
{
    /// <summary>
    /// Interaction logic for DBInfo.xaml
    /// </summary>
    public partial class DBInfo : Window
    {
        public DBInfo()
        {
            InitializeComponent();
            ConnString.Text = Properties.Settings.Default.ConnString;
            DBName.Text = Properties.Settings.Default.DBName;
            CollName.Text = Properties.Settings.Default.CollName;
            Struct.Text = "Object Structure -\n{\n\tkey: <string>,\n\tcompanyName: <string>,\n\tused: <bool>\n}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.ConnString = ConnString.Text;
            Properties.Settings.Default.DBName = DBName.Text;
            Properties.Settings.Default.CollName = CollName.Text;
            Properties.Settings.Default.Save();
            DBParams.SetParams(ConnString.Text, DBName.Text, CollName.Text);
            Close();
        }
    }
}
