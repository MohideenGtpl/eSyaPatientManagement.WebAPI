using System;
using System.Collections.Generic;

namespace eSyaPatientManagement.DL.Entities
{
    public partial class GtEpdrcd
    {
        public int DrugCode { get; set; }
        public int Isdcode { get; set; }
        public int GenericId { get; set; }
        public int DrugFormulaId { get; set; }
        public int ManufacturerId { get; set; }
        public string DrugBrand { get; set; }
        public string DrugPrintDesc { get; set; }
        public int PackSize { get; set; }
        public int Packing { get; set; }
        public string DrugVolume { get; set; }
        public decimal ReferenceMrp { get; set; }
        public string BarcodeId { get; set; }
        public bool UsageStatus { get; set; }
        public bool ActiveStatus { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedTerminal { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedTerminal { get; set; }
    }
}
