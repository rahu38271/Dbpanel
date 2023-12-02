using MobileDataSearch.DtoModel;
using MobileDataSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Repository.Interface
{
     public interface IMobileDataRepository
     {
         IEnumerable<MaharashtraDTO> SearchBy_MobileNo(string MobileNo);
         IEnumerable<MaharashtraDTO> SearchBy_PinCode(string PinCode);
         IEnumerable<MaharashtraDTO> SearchBy_Name(string Name);
         IEnumerable<MaharashtraDTO> SearchBy_Alt_MobileNo(string AltMobileNo);
         IEnumerable<MaharashtraDTO> SearchBy_Address(string Address);
         IEnumerable<MaharashtraDTO> SearchBy_Address_PinCode(string Address, string PinCode);
         IEnumerable<MaharashtraDTO> SearchBy_Address_Name(string Address, string Name);
         IEnumerable<TotalCountbyPincode> GetCountbyPinCode(string PinCode);
         int InsertBulkCustomer(List<MaharasthraNew> maharasthraNews);
         IEnumerable<District> GetAllDistrict();
         IEnumerable<MaharashtraDTO> SearchByPincodeName(string Pincode, string Name);
         IEnumerable<TotalCountbyPincode> GetCount_SearchByPincodeName(string Pincode, string Name);

    }
}
