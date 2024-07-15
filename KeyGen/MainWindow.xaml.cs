using KeyGen.Classes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace KeyGen
{

    public partial class MainWindow : Window
    {
        MongoClientSettings settings;
        IMongoCollection<Key> collection;
        List<Key> keys;
        public MainWindow()
        {
            DBInfo db = new DBInfo();
            db.ShowDialog();
            InitializeComponent();
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            String key = RandomString(8);
            Key obj = new Key(key, CompanyName.Text);
            await collection.InsertOneAsync(obj);
            Clipboard.SetText(key);
            MessageBox.Show("Key generated and copied to clipboard!");
            keys = await collection.Find(Builders<Key>.Filter.Empty).ToListAsync();
            KeyGrid.ItemsSource = null;
            KeyGrid.ItemsSource = keys;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #region Class_Key

        internal class Key
        {
            public ObjectId Id { get; set; }
            public string key { get; set; }
            public string companyName { get; set; }
            public bool used { get; set; }

            public Key(string key, string companyName)
            {
                this.key = key;
                this.companyName = companyName;
                used = false;
            }
        }

        #endregion

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                settings = MongoClientSettings.FromConnectionString(DBParams.GetConnString());
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                var client = new MongoClient(settings);
                var database = client.GetDatabase(DBParams.GetDBName());
                collection = database.GetCollection<Key>(DBParams.GetCollName());
                keys = await collection.Find(Builders<Key>.Filter.Empty).ToListAsync();
                KeyGrid.ItemsSource = keys;
            } catch(Exception err)
            {
                MessageBox.Show(err.Message, "Error!");
            }
        }

        private void KeyGrid_CopyingRowClipboardContent(object sender, System.Windows.Controls.DataGridRowClipboardEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String key = (((Button)sender).CommandParameter).ToString();
            Clipboard.SetText(key);
        }

        private void Window_Initialized(object sender, EventArgs e)
        {

        }
    }

    public class BooleanToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((bool)value) return "Yes";
            return "No";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
