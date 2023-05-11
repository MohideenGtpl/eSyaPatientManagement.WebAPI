using System;
using System.Collections.Generic;
using System.Text;

namespace eSyaPatientManagement.DO
{
    public class DO_AddressIN
    {
        public int IsdCode { get; set; }
        public int AreaCode { get; set; }
        public string AreaName { get; set; }
        public int StateCode { get; set; }
        public int CityCode { get; set; }
        public int District { get; set; }
        public string Pincode { get; set; }
        public string Taluk { get; set; }
    }

    public class DO_Place
    {
        public int IsdCode { get; set; }
        public int PlaceId { get; set; }
        public string PlaceType { get; set; }
        public string PlaceName { get; set; }
    }
}