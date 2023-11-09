using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Model
{

    public class Maharashtra
    {
        public int MID { get; set; }
        public string Mobile_Number { get; set; }
        public string Customer_Name_Company_Name { get; set; }
        public string Father_Name { get; set; }
        public DateTime? Date_OF_Birth { get; set; }
        public string Customer_Address { get; set; }
        public string Alternate_MobileNo { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Profession { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Operator { get; set; }
        public string Catagory { get; set; }
        public string VoterID { get; set; }
    }
}
