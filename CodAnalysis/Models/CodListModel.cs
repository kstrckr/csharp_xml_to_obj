using System;
using System.Collections.Generic;
using System.Text;


namespace CodAnalysis.Models
{
    class CodListModel
    {
        public List<CodRecord> CodRecords { get; set; }

        public HashSet<string> UniqueStates { get; set; }

        public HashSet<string> UniqueCauses { get; set; }

        public HashSet<int> YearsCovered { get; set; }

        public int TotalDeathsRecorded { get; set; }

        public CodListModel()
        {
            this.CodRecords = new List<CodRecord>();
            this.UniqueStates = new HashSet<string>();
            this.UniqueCauses = new HashSet<string>();
            this.YearsCovered = new HashSet<int>();
        }

        public void BuildTotalsValues()
        {
            foreach (CodRecord record in this.CodRecords)
            {
                this.UniqueStates.Add(record.State);
                this.UniqueCauses.Add(record.CauseName);
                this.YearsCovered.Add(record.Year);
                this.TotalDeathsRecorded += record.Deaths;
            }
        }
    }
}
