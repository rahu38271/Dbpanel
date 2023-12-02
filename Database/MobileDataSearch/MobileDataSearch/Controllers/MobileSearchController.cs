using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MobileDataSearch.DtoModel;
using MobileDataSearch.Model;
using MobileDataSearch.Services.Interface;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;

namespace MobileDataSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MobileSearchController : ControllerBase
    {
        private readonly IMobileDataService _mobileDataService;

        public MobileSearchController(IMobileDataService mobileDataService)
        {
            _mobileDataService = mobileDataService;
        }
      
        [HttpGet("SearchMobileNo")]
        public IActionResult SearchMobileNo(string MobileNo)
        {
            try
            {
                return Ok(_mobileDataService.SearchBy_MobileNo(MobileNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCount_SearchByPincodeName")]
        public IActionResult GetCount_SearchByPincodeName(string PinCode, string Name)
        {
            try
            {
                return Ok(_mobileDataService.GetCount_SearchByPincodeName(PinCode, Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("SearchByPincodeName")]
        public IActionResult SearchByPincodeName(string PinCode, string Name)
        {
            try
            {
               var maharashtraDTO=_mobileDataService.SearchByPincodeName(PinCode, Name).ToList();

                var fileNamecsv = Path.GetFileName($"{PinCode}.csv");
                var filePathcsv = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Upload");

                if (!System.IO.File.Exists(filePathcsv))
                    Directory.CreateDirectory(filePathcsv);

                var fullpath = Path.Combine(filePathcsv, fileNamecsv);

                FileInfo filecsv = new FileInfo(fullpath);
                if (filecsv.Exists)
                {
                    filecsv.Delete();
                    filecsv = new FileInfo(Path.Combine(filePathcsv, fileNamecsv));
                }

                var dt = maharashtraDTO.ToDataTable();

                dt.ToCSV(fullpath);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fullpath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = System.IO.File.ReadAllBytes(fullpath);
                if (System.IO.File.Exists(fullpath))
                    System.IO.File.Delete(fullpath);

                return File(bytes, contentType, fullpath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("SearchBy_PinCode")]
        public IActionResult SearchBy_PinCode(string PinCode)
        {
            try
            {
                var maharashtraDTO1 = _mobileDataService.SearchBy_PinCode(PinCode).ToList();
                var fileNamecsv = Path.GetFileName($"{PinCode}.csv");
                var filePathcsv = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot","Upload");

                if (!System.IO.File.Exists(filePathcsv))
                    Directory.CreateDirectory(filePathcsv);

                var fullpath = Path.Combine(filePathcsv, fileNamecsv);

                FileInfo filecsv = new FileInfo(fullpath);
                if (filecsv.Exists)
                {
                    filecsv.Delete();
                    filecsv = new FileInfo(Path.Combine(filePathcsv, fileNamecsv));
                }
                var dt = maharashtraDTO1.ToDataTable();
                dt.ToCSV(fullpath);

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(fullpath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var bytes = System.IO.File.ReadAllBytes(fullpath);
                if(System.IO.File.Exists(fullpath))
                    System.IO.File.Delete(fullpath);
                return File(bytes,contentType,fullpath);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("SearchBy_Name")]
        public IActionResult SearchBy_Name(string Name)            
        {
            try
            {
                //var name=_mobileDataService.SearchBy_Name(Name);
                return Ok(_mobileDataService.SearchBy_Name(Name));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("SearchbyAltMobileNo")]
        public IActionResult SearchbyAltMobileNo(string AltMobileNo)
        {
            try
            {
                return Ok(_mobileDataService.SearchBy_Alt_MobileNo(AltMobileNo));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("Search_by_Address")]
        public IActionResult Search_by_Address(string Address)
        {
            try
            {
                return Ok(_mobileDataService.SearchBy_Address(Address));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("SearchBy_Address_PinCode")]
        public IActionResult SearchbyAddress_Pincode(string Address, string PinCode)
        {
            try
            {
                return Ok(_mobileDataService.SearchBy_Address_PinCode(Address, PinCode));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("SearchBy_Address_Name")]
        public IActionResult SearchBy_Name_Address(string Address,string Name)
        {
            try
            {
                return Ok(_mobileDataService.SearchBy_Address_Name(Address, Name));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetCountbyPinCode")]
        public IActionResult GetCountbyPinCode(string PinCode)
        {
            try
            {
                return Ok(_mobileDataService.GetCountbyPinCode(PinCode));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(IFormFile file)
        {
            string[] dateformat = { "dd-MM-yyyy", "yyyy-MM-dd HH:mm:ss", "MM/dd/yyyy hh:mm tt", "dd-MMM-yyyy h:mm tt", "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm: ss", "dd-MM-yyyy HH:mm", "yyyy/MM/dd", "dddd, MMMM d, yyyy", "MM-dd-yy", "yyyy-MM-ddTHH:mm:ssZ", "dd MMMM yyyy", "MMM dd, yyyy", "MM/yyyy" };
            try
            {
                var fullpath = Path.Combine(Environment.CurrentDirectory, @"wwwroot\Upload");

                if (!System.IO.File.Exists(fullpath))
                    Directory.CreateDirectory(fullpath);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload", file.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    // The formFile is the method parameter which type is IFormFile
                    // Saves the files to the local file system using a file name generated by the app.
                    file.CopyTo(stream);
                }
                List<MaharasthraNew> maharasthraNews = new List<MaharasthraNew>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        MaharasthraNew maharasthra = new MaharasthraNew();
                        if (worksheet.Cells[row, 2].Value != null)
                            maharasthra.Customer_Name = worksheet.Cells[row, 2].Value.ToString().Trim();
                        if (worksheet.Cells[row, 3].Value != null)
                            maharasthra.Father_Name = worksheet.Cells[row, 3].Value.ToString().Trim();
                        if (worksheet.Cells[row, 4].Value != null)
                        {
                            var valDate = worksheet.Cells[row, 4].Value.ToString().Split(' ');
                            const DateTimeStyles style = DateTimeStyles.RoundtripKind;
                            DateTime dt;
                            var result = DateTime.TryParseExact(valDate[0].ToString(), dateformat, CultureInfo.InvariantCulture, style, out dt);
                            if (result)
                            {
                                maharasthra.BirthDate = dt.ToString("yyyy-MM-dd");
                            }
                        }
                        if (worksheet.Cells[row, 5].Value != null)
                            maharasthra.Local_Address = worksheet.Cells[row, 5].Value.ToString().Trim();
                        if (worksheet.Cells[row, 6].Value != null)
                            maharasthra.Permanent_Address = worksheet.Cells[row, 6].Value.ToString().Trim();
                        if (worksheet.Cells[row, 7].Value != null)
                            maharasthra.Local_PinCode = worksheet.Cells[row,7].Value.ToString().Trim();
                        if (worksheet.Cells[row, 8].Value != null)
                            maharasthra.Permanent_PinCode = worksheet.Cells[row,8].Value.ToString().Trim();
                        if (worksheet.Cells[row, 9].Value != null)
                            maharasthra.Gender = worksheet.Cells[row, 9].Value.ToString().Trim();
                        if (worksheet.Cells[row, 10].Value != null)
                            maharasthra.VotingCardNo = worksheet.Cells[row, 10].Value.ToString().Trim();
                        if (worksheet.Cells[row, 11].Value != null)
                            maharasthra.AdharCardNo = worksheet.Cells[row, 11].Value.ToString().Trim();
                        if (worksheet.Cells[row, 12].Value != null)
                            maharasthra.PanCard = worksheet.Cells[row, 12].Value.ToString().Trim();
                        if (worksheet.Cells[row, 13].Value != null)
                            maharasthra.MobileNo = worksheet.Cells[row, 13].Value.ToString().Trim();
                        if (worksheet.Cells[row, 14].Value != null)
                            maharasthra.AltMobile_Number = worksheet.Cells[row, 14].Value.ToString().Trim();
                        if (worksheet.Cells[row, 15].Value != null)
                            maharasthra.Profession = worksheet.Cells[row, 15].Value.ToString().Trim();
                        if (worksheet.Cells[row, 16].Value != null)
                            maharasthra.Email = worksheet.Cells[row, 16].Value.ToString().Trim();
                        if (worksheet.Cells[row, 17].Value != null)
                            maharasthra.City = worksheet.Cells[row, 17].Value.ToString().Trim();
                        if (worksheet.Cells[row, 18].Value != null)
                            maharasthra.State = worksheet.Cells[row, 18].Value.ToString().Trim();
                        if (worksheet.Cells[row, 19].Value != null)
                            maharasthra.Operator = worksheet.Cells[row, 19].Value.ToString().Trim();
                        if (worksheet.Cells[row, 20].Value != null)
                            maharasthra.Catagory = worksheet.Cells[row, 20].Value.ToString().Trim();
                        if (worksheet.Cells[row, 21].Value != null)
                            maharasthra.IsActive = worksheet.Cells[row, 21].Value.ToString().Trim();
                        if (worksheet.Cells[row, 22].Value != null)
                            maharasthra.CustomerDate = worksheet.Cells[row, 22].Value.ToString().Trim();
                        if (worksheet.Cells[row, 23].Value != null)
                            maharasthra.Source_of_Data = worksheet.Cells[row, 23].Value.ToString().Trim();

                        maharasthraNews.Add(maharasthra);
                    }
                }
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Ok(_mobileDataService.InsertBulkCustomer(maharasthraNews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("AddBulkCustomer")]
        public IActionResult AddBulkCustomer(List<MaharasthraNew> maharasthraNews)
        {
            try
            {
                return Ok(_mobileDataService.InsertBulkCustomer(maharasthraNews));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPincodewiseCount")]
        public IActionResult GetPincodewiseCount(string pincode)
        {
            try
            {              
                List<MobilePinCodeCount> pinCodeCounts = new List<MobilePinCodeCount>();
                var arr = pincode.Split(",");
                foreach (var item in arr)
                {
                    MobilePinCodeCount pinCodeCount = new MobilePinCodeCount();
                    pinCodeCount.PinCode = item;
                    pinCodeCount.TotalCount=_mobileDataService.GetCountbyPinCode(item).FirstOrDefault().TotalCount;
                    pinCodeCounts.Add(pinCodeCount);
                }
                return Ok(pinCodeCounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllDistrict")]
        public IActionResult GetAllDistrict()
        {
            try
            {
                return Ok(_mobileDataService.GetAllDistrict());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }


    public static class ExtensionMethod
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static void ToCSV(this DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
