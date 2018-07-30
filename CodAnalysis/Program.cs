using System;
using System.Collections.Generic;
using System.Xml;
using CodAnalysis.Models;

namespace CodAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlPath = "../../../causes_of_death.xml";

            XmlDocument xmlDoc = new XmlDocument();
            
            xmlDoc.Load(xmlPath);

            CodListModel allCodRecords = new CodListModel();

            List<CodRecord> tempRecords = new List<CodRecord>();

            foreach (XmlNode xmlNode in xmlDoc.DocumentElement.ChildNodes[0])
            {                
                int year;
                int deaths;
                float aadr;

                try
                {
                    year = Int32.Parse(xmlNode["year"].InnerText);
                }
                catch(NullReferenceException)
                {
                    year = 0;
                }

                try
                {
                    deaths = Int32.Parse(xmlNode["deaths"].InnerText);
                }
                catch (NullReferenceException)
                {
                    deaths = 0;
                }

                try
                {
                    aadr = float.Parse(xmlNode["aadr"].InnerText);
                }
                catch(NullReferenceException)
                {
                    aadr = 0;
                }
                
                CodRecord cod = new CodRecord
                {
                    Year = year,
                    CauseName = xmlNode.ChildNodes[1].InnerText,
                    State = xmlNode["state"].InnerText,
                    Deaths = deaths,
                    Aadr = aadr
                };

                tempRecords.Add(cod);
            }

            allCodRecords.CodRecords = tempRecords;

            HashSet<int> allStates = new HashSet<int>();

            foreach (CodRecord values in allCodRecords.CodRecords)
            {
                allStates.Add(values.Year);
            }

            foreach (int state in allStates)
            {
                Console.WriteLine(state);
            }

            // Keep console open until any key is pressed
            
            Console.WriteLine("\nPress any key to close Consle");
            Console.ReadKey();
        }
    }
}
