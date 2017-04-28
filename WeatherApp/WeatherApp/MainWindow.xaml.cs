using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
using System.Xml;
using System.Xml.Serialization;
using WeatherApp.ServiceReference;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_KEY = "a380e0de53c4d1f1666fc347af9c8dd9";
        private const string CurrentUrl = "http://api.openweathermap.org/data/2.5/weather?q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;
        private const string ForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Get current conditions.
        private void ButtonGetForecast_Click(object sender, EventArgs e)
        {
            // Compose the query URL.
            string url = CurrentUrl.Replace("@LOC@", TextBoxCity.Text);
            try
            {
                ClearRichTextBox(RTBWeatherDetails);
                InsertIntoRichTextBox(RTBWeatherDetails, GetFormattedXml(url));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Get a forecast.
        private void ButtonGetWeather_Click(object sender, RoutedEventArgs e)
        {
            // Compose the query URL.
            string url = ForecastUrl.Replace("@LOC@", TextBoxCity.Text);
            try
            {
                ClearRichTextBox(RTBWeatherDetails);
                InsertIntoRichTextBox(RTBWeatherDetails, GetFormattedXml(url));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        // Return the XML result of the URL.
        private string GetFormattedXml(string url)
        {
            using (WebClient client = new WebClient())
            {
                XmlTextReader reader = new XmlTextReader(url);

                string output = "";
                while (reader.Read())
                {
                    for (int i = 0; i < reader.AttributeCount; i++)
                    {
                        output += $"{reader.Name}: {reader.GetAttribute(i)} {reader.Value} \n";
                    }
                }

                /*
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Attribute:
                            {
                                output += reader.GetAttribute(0);
                                break;
                            }
                        case XmlNodeType.Element:
                            {
                                output += reader.ToString();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                */
                return output;
            }

            /*
            // Create a web client.
            using (WebClient client = new WebClient())
            {
                // Get the response string from the URL.
                string xml = client.DownloadString(url);

                // Load the response into an XML document.
                XmlDocument xml_document = new XmlDocument();
                xml_document.LoadXml(xml);

                // Format the XML.
                using (StringWriter string_writer = new StringWriter())
                {
                    XmlTextWriter xml_text_writer = new XmlTextWriter(string_writer);
                    xml_text_writer.Formatting = Formatting.Indented;
                    xml_document.WriteTo(xml_text_writer);

                    // Return the result.
                    return string_writer.ToString();
                }
            }
            */
        }



        private void InsertIntoRichTextBox(RichTextBox rtb, string input)
        {
            Paragraph para = new Paragraph();
            para.Margin = new Thickness(0);
            para.Inlines.Add(input);
            rtb.Document.Blocks.Add(para);
        }
        private void ClearRichTextBox(RichTextBox rtb)
        {
            rtb.Document.Blocks.Clear();
        }
    }
}
