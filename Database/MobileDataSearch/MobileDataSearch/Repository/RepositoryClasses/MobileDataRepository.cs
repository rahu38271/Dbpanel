using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MobileDataSearch.DtoModel;
using MobileDataSearch.Model;
using MobileDataSearch.Model.Data;
using MobileDataSearch.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MobileDataSearch.Repository.RepositoryClasses
{
    public class MobileDataRepository : IMobileDataRepository
    {
        private CustomContext _custonContext = new CustomContext();

        public IEnumerable<TotalCountbyPincode> GetCountbyPinCode(string PinCode)
        {
            try
            {
                return _custonContext.Set<TotalCountbyPincode>().FromSqlRaw("Exec GetCountbyPinCode {0}", PinCode);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address(string Address)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Address {0}", Address);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address_Name(string Address, string Name)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Address_Name {0},{1}", Address,Name);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Address_PinCode(string Address, string PinCode)
        {
           try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Address_PostalCode {0},{1}", Address, PinCode);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Alt_MobileNo(string AltMobileNo)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_ALt_Mobile {0}", AltMobileNo);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_MobileNo(string MobileNo)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Mobile {0}", MobileNo);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_Name(string Name)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Name {0}", Name);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchBy_PinCode(string PinCode)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Pincode {0}", PinCode);
            }
            catch(Exception ex)

            {
                throw;
            }
        }
        public int InsertBulkCustomer(List<MaharasthraNew> maharasthraNew)
        {
            var partitions = maharasthraNew.partition(200000);

            foreach (List<MaharasthraNew> maharasthraNews in partitions)
            {
                try
                {
                    DataTable dt = new DataTable();
                    PropertyInfo[] Props = typeof(MaharasthraNew).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        //Setting column names as Property names
                        dt.Columns.Add(prop.Name);
                    }
                    foreach (MaharasthraNew item in maharasthraNews)
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
                            using (SqlCommand cmd = new SqlCommand("InsertCustomer"))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = con;
                                cmd.Parameters.AddWithValue("@CustomerType", dt);
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

        public IEnumerable<District> GetAllDistrict()
        {
            try
            {
                return _custonContext.Set<District>().FromSqlRaw("Exec USP_GetAllDistricts");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MaharashtraDTO> SearchByPincodeName(string Pincode, string Name)
        {
            try
            {
                return _custonContext.Set<MaharashtraDTO>().FromSqlRaw("Exec Search_by_Pincode_Name {0},{1}",Pincode,Name);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<TotalCountbyPincode> GetCount_SearchByPincodeName(string Pincode, string Name)
        {
            try
            {
                return _custonContext.Set<TotalCountbyPincode>().FromSqlRaw("Exec GetCustomerbyPinCodeName {0},{1}", Pincode, Name);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
