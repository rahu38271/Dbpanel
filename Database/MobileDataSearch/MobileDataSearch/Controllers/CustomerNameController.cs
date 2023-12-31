﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MobileDataSearch.Model;
using MobileDataSearch.Services.Interface;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileDataSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerNameController : ControllerBase
    {
        private readonly ICustomerNameService _customerNameService;

        public CustomerNameController(ICustomerNameService customerNameService)
        {
            _customerNameService = customerNameService;
        }

        [HttpGet("GetCustomerNameCount")]
        public IActionResult GetCustomerNameCount()
        {
            try
            {
                return Ok(_customerNameService.GetTotalCount());
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetMobileMatchCount")]
        public IActionResult GetMobileMatchCount(string District, string Name, string MobileNo)
        {
            try
            {
                return Ok(_customerNameService.GetMobileMatchCount(District, Name, MobileNo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpGet("GetMobileMatch")]
        public IActionResult GetMobileMatch(string District, string Name, string MobileNo)
        {
            try
            {
                var filecontent = _customerNameService.GetMobileMatch(District, Name, MobileNo).ToList();
                var fileNamecsv = Path.GetFileName($"{District}.xlsx");
                var filePathcsv = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Upload");
                if (!Directory.Exists(filePathcsv))
                    Directory.CreateDirectory(filePathcsv);

                var fullpath = Path.Combine(filePathcsv, fileNamecsv);
                FileInfo filecsv = new FileInfo(fullpath);
                if (filecsv.Exists)
                {
                    filecsv.Delete();
                    filecsv = new FileInfo(Path.Combine(filePathcsv, fileNamecsv));
                }
                var dt = filecontent.ToDataTable();
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

        [HttpPost("InsertCustomerName")]
        public IActionResult InsertCustomerName(IFormFile file)
        {
            try
            {
                var fullpath = Path.Combine(Environment.CurrentDirectory, @"wwwroot\Upload");

                if (!System.IO.Directory.Exists(fullpath))
                    Directory.CreateDirectory(fullpath);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Upload", file.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    // The formFile is the method parameter which type is IFormFile
                    // Saves the files to the local file system using a file name generated by the app.
                    file.CopyTo(stream);
                }
                List<CustomerName> customerNames = new List<CustomerName>();
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        CustomerName customerName = new CustomerName();
                        if (worksheet.Cells[row, 1].Value != null)
                            customerName.Name = worksheet.Cells[row, 1].Value.ToString().Trim();
                        if (worksheet.Cells[row, 2].Value != null)
                            customerName.MobileNo = worksheet.Cells[row, 2].Value.ToString().Trim();
                        customerNames.Add(customerName);
                    }
                }
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                return Ok(_customerNameService.InsertBulkCustomerName(customerNames));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("InsertBulkCustomerName")]
        public IActionResult InsertBulkCustomerName(List<CustomerName> customerNames)
        {
            try
            {
                return Ok(_customerNameService.InsertBulkCustomerName(customerNames));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
