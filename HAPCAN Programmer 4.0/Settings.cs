using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hapcan.Programmer
{
    public class Settings
    {
        [XmlAttribute("TimeFormat")]
        public string TimeFormat { get; set; }

        public Settings()
        {
            TimeFormat = "yyyy-MM-dd HH:mm:ss.fff";
        }
    }
}
