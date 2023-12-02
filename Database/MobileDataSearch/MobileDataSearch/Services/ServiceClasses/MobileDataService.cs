using MobileDataSearch.DtoModel;
using MobileDataSearch.Model;
using MobileDataSearch.Repository.Interface;
using MobileDataSearch.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Services.ServiceClasses
{
    public class MobileDataService : IMobileDataService
    {
        private readonly IMobileDataRepository _mobileDataRepository;

        public MobileDataService(IMobileDataRepository mobileDataRepository)
        {
            _mobileDataRepository = mobileDataRepository;
        }

        public IEnumerable<District> GetAllDistrict()
        {
            return _mobileDataRepository.GetAllDistrict();
        }

        public IEnumerable<TotalCountbyPincode> GetCountbyPinCode(string PinCode)
        {
            return _mobileDataRepository.GetCountbyPinCode(PinCode);
        }

        public IEnumerable<TotalCountbyPincode> GetCount_SearchByPincodeName(string Pincode, string Name)
        {
            return _mobileDataRepository.GetCount_SearchByPincodeName(Pincode, Name);
        }

        public int InsertBulkCustomer(List<MaharasthraNew> maharasthraNews)
        {
            return _mobileDataRepository.InsertBulkCustomer(maharasthraNews);
        }

        public IEnumerable<MaharashtraDTO> SearchByPincodeName(string Pincode, string Name)
        {
            return _mobileDataRepository.SearchByPincodeName(Pincode, Name);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address(string Address)
        {
            return _mobileDataRepository.SearchBy_Address(Address);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address_Name(string Address, string Name)
        {
            return _mobileDataRepository.SearchBy_Address_Name(Address, Name);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address_PinCode(string Address, string PinCode)
        {
            return _mobileDataRepository.SearchBy_Address_PinCode(Address, PinCode);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Alt_MobileNo(string AltMobileNo)
        {
            return _mobileDataRepository.SearchBy_Alt_MobileNo(AltMobileNo);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_MobileNo(string MobileNo)
        {
            return _mobileDataRepository.SearchBy_MobileNo(MobileNo);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Name(string Name)
        {
            return _mobileDataRepository.SearchBy_Name(Name);
        }

        public IEnumerable<MaharashtraDTO> SearchBy_PinCode(string PinCode)
        {
            return _mobileDataRepository.SearchBy_PinCode(PinCode);
        }
    }
}
