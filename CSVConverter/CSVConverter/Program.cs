using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace CSVConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path where you have the xml file");
            string path = Console.ReadLine();
            //string inputdir = @"D:\DevopsTestResults\result.xml";
            string outputdir = @"D:\DevopsTestResults\result3.csv";
            StringBuilder sb = new StringBuilder();
            string delimit = ",";
            XDocument.Load(path).Descendants("test-run").ToList().ForEach(
                element => sb.Append(element.Attribute("engine-version").Value + delimit +
                                   element.Attribute("asserts").Value + delimit +
                                   element.Attribute("failed").Value + delimit +
                                   element.Attribute("passed").Value + delimit +
                                   element.Attribute("total").Value + delimit +
                                   element.Attribute("result").Value + delimit +"\r\n"));
            StreamWriter sw = new StreamWriter(outputdir);
            sw.WriteLine(sb.ToString());
            sw.Close();

        }
    }
}
