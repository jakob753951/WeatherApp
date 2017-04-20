using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
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
using System.Xml.Serialization;
using WeatherApp.ServiceReference;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Cities.NewDataSet cn;
        public MainWindow()
        {
            InitializeComponent();

            BasicHttpBinding binding = new BasicHttpBinding();
            binding.MaxReceivedMessageSize = 20000000;

            EndpointAddress address = new EndpointAddress("http://www.webservicex.com/globalweather.asmx?WSDL");

            GlobalWeatherSoapClient gwsc = new ServiceReference.GlobalWeatherSoapClient(binding, address);

            string cities = gwsc.GetCitiesByCountry("");

            XmlSerializer result = new XmlSerializer(typeof(Cities.NewDataSet));
            cn = (Cities.NewDataSet)result.Deserialize(new StringReader(cities));

            var Countries = cn.Table.Select(m => m.Country).Distinct();
            foreach(string Country in Countries.ToArray())
            {
                CBCountries.Items.Add(Country);
            }
        }

        private void CBCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rr = cn.Table.Where(m => m.Country == CBCountries.Text).Select(c => c.City);

            CBCities.Items.Clear();
            foreach (var item in rr.ToArray())
            {
                CBCities.Items.Add(item);
            }
        }

        private void CBCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
