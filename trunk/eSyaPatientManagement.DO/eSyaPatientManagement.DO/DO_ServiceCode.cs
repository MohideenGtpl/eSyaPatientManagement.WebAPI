using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_ServiceCode
    {
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public int ServiceGroupId { get; set; }
        public int ServiceClassId { get; set; }
        public string ServiceDesc { get; set; }
        public string ServiceShortDesc { get; set; }
        public string Gender { get; set; }
        public bool IsServiceBillable { get; set; }
        public decimal ServiceCost { get; set; }
        public bool ActiveStatus { get; set; }
        public string InternalServiceCode { get; set; }

        public string ServiceTypeDesc { get; set; }
        public string ServiceGroupDesc { get; set; }
        public string ServiceClassDesc { get; set; }

        public bool BusinessLinkStatus { get; set; }

        public List<DO_eSyaParameter> l_ServiceParameter { get; set; }
    }
    public class DO_eSyaParameter
    {
        public int ParameterID { get; set; }
        public string ParameterValue { get; set; }
        public bool ParmAction { get; set; }
        public decimal ParmValue { get; set; }
        public decimal ParmPerc { get; set; }
        public string ParmDesc { get; set; }
        public bool ActiveStatus { get; set; }
    }
}
