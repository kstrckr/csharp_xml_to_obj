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

            int deathSums = 0;
            
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

                // This file includes entries for Nation-Wide Sums for each
                // COD every year, this ignores those, since we're doing our own sum

                if (cod.State == "United States")
                {
                    deathSums += cod.Deaths;
                    continue;
                }

                allCodRecords.CodRecords.Add(cod);
            }

            allCodRecords.BuildTotalsValues();

            //Console.WriteLine("{0:N0}", deathSums);

            Console.WriteLine("\nThis XML DataSet contains {0:N0} records covering:\n" +
                "\n {1} Years" +
                "\n {2:N0} Deaths" +
                "\n in {3} States (Including Washington DC)" +
                "\n by {4} Unique Causes",
                allCodRecords.CodRecords.Count,
                allCodRecords.YearsCovered.Count,
                allCodRecords.TotalDeathsRecorded,
                allCodRecords.UniqueStates.Count,
                allCodRecords.UniqueCauses.Count);
            
            Console.WriteLine("\nPress any key to close Consle");
            Console.ReadKey();
        }
    }
}
