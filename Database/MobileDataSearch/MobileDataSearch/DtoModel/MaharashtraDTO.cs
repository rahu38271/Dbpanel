using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.DtoModel
{
    public class MaharashtraDTO
    {
        public string MobileNo { get; set; }
        public string Customer_Name { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Father_Name { get; set; }
        public string Local_Address { get; set; }
        public string Permanent_Address { get; set; }
        public string AltMobile_Number { get; set; }
        public string Email { get; set; }
        public string Source_of_Data { get; set; }
        public string VotingCardNo { get; set; }
    }
}
