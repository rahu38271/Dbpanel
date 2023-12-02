using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MobileDataSearch.Model;
using MobileDataSearch.Model.Data;
using MobileDataSearch.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MobileDataSearch.Model;
using Microsoft.EntityFrameworkCore;

namespace MobileDataSearch.Repository.RepositoryClasses
{
    public class CustomerNameRepository : ICustomerNameRepository
    {
        private CustomContext _custonContext = new CustomContext();
        public IEnumerable<MobileMatch> GetMobileMatch(string District, string Name, string MobileNo)
        {
            try
            {
               return _custonContext.Set<MobileMatch>().FromSqlRaw("Exec CustomerDetailsfromName {0},{1},{2}", District, Name, MobileNo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerCount> GetMobileMatchCount(string District, string Name, string MobileNo)
        {
            try
            {
                return _custonContext.Set<CustomerCount>().FromSqlRaw("Exec GetMobileMatchCount {0},{1},{2}", District, Name, MobileNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerCount> GetTotalCount()
        {
            try
            {
                return _custonContext.Set<CustomerCount>().FromSqlRaw("Exec GetCustomerCount");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public int InsertBulkCustomerName(List<CustomerName> customerNames)
        {
            var partitions = customerNames.partition(200000);

            foreach (List<CustomerName> customerName in partitions)
            {
                try
                {
                    DataTable dt = new DataTable();
                    PropertyInfo[] Props = typeof(CustomerName).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        //Setting column names as Property names
                        dt.Columns.Add(prop.Name);
                    }
                    foreach (CustomerName item in customerNames)
                    {
                        var values = new object[Props.Length];
                        for (int i = 0; i < Props.Length; i++)
                        {
                            values[i] = Props[i].GetValue(item, null);
                        }
                        dt.Rows.Add(values);
                    }

                    var d = dt;

                    if (dt.Rows.Count > 0)
                    {
                        using (SqlConnection con = new SqlConnection(_custonContext.Database.GetConnectionString()))
                        {
                            using (SqlCommand cmd = new SqlCommand("InsertCustomerName"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@CustomerName", dt);
                                cmd.CommandTimeout = 0;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return 1;
        }
    }
}
