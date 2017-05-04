using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Xml;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string API_KEY = "a380e0de53c4d1f1666fc347af9c8dd9";
        private const string CurrentUrl = "http://api.openweathermap.org/data/2.5/weather?q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;
        private const string ForecastUrl = "http://api.openweathermap.org/data/2.5/forecast?q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;
        public MainWindow()
        {
            InitializeComponent();
        }

        // Get current conditions.
        private void ButtonGetForecast_Click(object sender, RoutedEventArgs e)
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

        // Get a forecast.
        private void ButtonGetWeather_Click(object sender, RoutedEventArgs e)
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

        // Return the XML result of the URL.
        private string GetFormattedXml(string url)
        {
            using (WebClient client = new WebClient())
            {
                XmlTextReader reader = new XmlTextReader(url);
                string output = "";
                while (reader.Read())
                {
                    if(reader.NodeType == XmlNodeType.Element)
                    {
                        output += $"{reader.Name}\n";
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "value")
                            {
                                output += $"{reader.Value}\n";
                            }
                            else
                            {
                                output += $"{reader.Name} = {reader.Value}\n";
                            }
                        }
                        output += $"\n";
                    }
                }
                return output;
            }
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
