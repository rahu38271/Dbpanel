﻿using MobileDataSearch.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Services.Interface
{
    public interface ICustomerNameService
    {
        int InsertBulkCustomerName(List<CustomerName> customerNames);
        IEnumerable<CustomerCount> GetTotalCount();
        IEnumerable<MobileMatch> GetMobileMatch(string District, string Name, string MobileNo);

        IEnumerable<CustomerCount> GetMobileMatchCount(string District, string Name, string MobileNo);
    }
}
