using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Cities
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class NewDataSet
        {
            private NewDataSetTable[] tableField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Table")]
            public NewDataSetTable[] Table
            {
                get
                {
                    return tableField;
                }
                set
                {
                    tableField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class NewDataSetTable
        {
            private string countryField;

            private string cityField;

            ///<remarks/>
            public string Country
            {
                get
                {
                    return countryField;
                }
                set
                {
                    countryField = value;
                }
            }

            ///<remarks/>
            public string City
            {
                get
                {
                    return cityField;
                }
                set
                {
                    cityField = value;
                }
            }
        }
    }
}
