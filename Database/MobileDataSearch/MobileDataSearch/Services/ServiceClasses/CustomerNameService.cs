using MobileDataSearch.Model;
using MobileDataSearch.Repository.Interface;
using MobileDataSearch.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Services.ServiceClasses
{
    public class CustomerNameService : ICustomerNameService
    {
        private readonly ICustomerNameRepository _customerNameRepository;

        public CustomerNameService(ICustomerNameRepository customerNameRepository)
        {
            _customerNameRepository = customerNameRepository;
        }
        public IEnumerable<MobileMatch> GetMobileMatch(string District, string Name, string MobileNo)
        {
            return _customerNameRepository.GetMobileMatch(District,Name,MobileNo);
        }

        public IEnumerable<CustomerCount> GetMobileMatchCount(string District, string Name, string MobileNo)
        {
            return _customerNameRepository.GetMobileMatchCount(District,Name,MobileNo);
        }

        public IEnumerable<CustomerCount> GetTotalCount()
        {
            return _customerNameRepository.GetTotalCount();
        }

        public int InsertBulkCustomerName(List<CustomerName> customerNames)
        {
            return _customerNameRepository.InsertBulkCustomerName(customerNames);
        }
    }
}
