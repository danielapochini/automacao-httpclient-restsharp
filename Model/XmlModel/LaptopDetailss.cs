using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace WebServiceAutomation.Model.XmlModel
{
    [XmlRoot(ElementName = "laptopDetailss")]
    public class LaptopDetailss
    {
        [XmlElement(ElementName = "Laptop")]
        public List <Laptop> Laptop { get; set; }
    }
}
