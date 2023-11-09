import { Component, OnInit } from '@angular/core';
import { DatasearchService } from 'src/app/services/datasearch.service';
import * as  XLSX from 'xlsx';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bulk-upload',
  templateUrl: './bulk-upload.component.html',
  styleUrls: ['./bulk-upload.component.scss'],
})
export class BulkUploadComponent implements OnInit {

  file:any;
  arraylist:any;
  disabled:boolean= true;
  obj: any = {};
  excelUploadedData:any=[];
  bulkModal:any={
    
  }
  constructor(
    private dataSearch:DatasearchService,
    private router:Router
  ) { }

  ngOnInit() {}

  onFileSelected(event:any){
    if (event.target.files.length > 0) {
      this.file = event.target.files[0];
      this.ReadExcelData();
      if (this.file.type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
        {
          // this.myFileInput.nativeElement.value = '';
          // return this.alertify.error("This file format is not allowed.");
          alert("This file format is not allowed.");
        }
        //this.ReadExcelData();
      }
    }
  }

  ReadExcelData() {
    debugger;
    var reader = new FileReader();
    var data = reader.result;
    reader.readAsBinaryString(this.file);
    reader.onload = (event) => {
      var data = reader.result;
      //var workbook = XLSX.read(data, { type: 'binary', cellDates: true });
      var workbook = XLSX.read(data, { type: 'binary' });
      var first_sheet_name = workbook.SheetNames[0];
      var worksheet = workbook.Sheets[first_sheet_name];
      //console.log(XLSX.utils.sheet_to_json(worksheet, { raw: true }));
      this.arraylist = XLSX.utils.sheet_to_json(worksheet, { raw: true });
      debugger;
      if (this.arraylist.length > 0) {
        // this.spinner.show(); 
        this.obj = (this.arraylist)[0];
        if (Object.keys(this.obj)[0] != "Customer_Name" && 
        (this.obj)[1] != "Father_Name" && 
        (this.obj)[2] != "BirthDate" && 
        (this.obj)[3] != "Local_Address" && 
        (this.obj)[4] != "Permanent_Address" && 
        (this.obj)[5] != "Local_PinCode" && 
        (this.obj)[6] != "Permanent_PinCode" && 
        (this.obj)[7] != "Gender" && 
        (this.obj)[8] != "VotingCardNo" && 
        (this.obj)[9] != "AdharCardNo" && 
        (this.obj)[10] != "PanCard" && 
        (this.obj)[11] != "MobileNo" && 
        (this.obj)[12] != "AltMobile_Number" && 
        (this.obj)[13] != "Profession" && 
        (this.obj)[14] != "Email" && 
        (this.obj)[15] != "City" && 
        (this.obj)[16] != "State"&& 
        (this.obj)[17] != "Operator" && 
        (this.obj)[18] != "Category" && 
        (this.obj)[19] != "IsActive" && 
        (this.obj)[20] != "CustomerDate" && 
        (this.obj)[21] != "Source_of_Data" ) {
          alert("Only provided template file is allowed. Please download the provided template only!")
          this.disabled = true;
        }
        else {
          this.disabled = false;
        }
      }
      else {
        alert("List is Empty!")
        this.disabled = true; 
      }
      
      //arraylist = arraylist.map((u: any) => ({ value: u.vin }));
      if (this.arraylist.length > 0) {
        for (var i = 0; i < this.arraylist.length; i++) {
          debugger;
          if(this.arraylist[i].BirthDate != undefined){
            var rawdob = this.arraylist[i].BirthDate;
            var DB = new Date((rawdob - 25569) * 86400000);
            if(DB.toString() !== 'Invalid Date'){
              var Dob = DB.toISOString().replace(/.\d+Z$/g, "");
            }
            else{
              var Dob = '1900-01-01T00:00:00'
            }
          }
          else{
            var Dob = '1900-01-01T00:00:00'
          }


          //for birthdate if excel column is empty
          if (this.arraylist[i].BirthDate == undefined) {
            this.arraylist[i].BirthDate = '1900-01-01T00:00:00';
          }
          else {
            this.arraylist[i].BirthDate = Dob;
          }

           //for Local_PinCode if excel column is empty
           if (this.arraylist[i].Local_PinCode !== undefined) {
            this.arraylist[i].Local_PinCode = this.arraylist[i].Local_PinCode.toString();
          }
          else {
            this.arraylist[i].Local_PinCode = null;
          }
   

          //for Permanent_PinCode if excel column is empty
          if (this.arraylist[i].Permanent_PinCode !== undefined) {
            this.arraylist[i].Permanent_PinCode = this.arraylist[i].Permanent_PinCode.toString();
          }
          else {
            this.arraylist[i].Permanent_PinCode = null;
          }
          

           //for MobileNo if excel column is empty
           if (this.arraylist[i].MobileNo == undefined) {
            this.arraylist[i].MobileNo = '';
          }
          else {
            this.arraylist[i].MobileNo = this.arraylist[i].MobileNo.toString();
          }

          //for AltMobile_Number if excel column is empty
          if (this.arraylist[i].AltMobile_Number == undefined) {
            this.arraylist[i].AltMobile_Number = '';
          }
          else {
            this.arraylist[i].AltMobile_Number = this.arraylist[i].AltMobile_Number.toString();
          }

          //for AdharCardNo if excel column is empty
          if (this.arraylist[i].AdharCardNo == undefined) {
            this.arraylist[i].AdharCardNo = '';
          }
          else {
            this.arraylist[i].AdharCardNo = this.arraylist[i].AdharCardNo.toString();
          }

          //for VotingCardNo if excel column is empty
          if (this.arraylist[i].VotingCardNo == undefined) {
            this.arraylist[i].VotingCardNo = '';
          }
          else {
            this.arraylist[i].VotingCardNo = this.arraylist[i].VotingCardNo.toString();
          }

          //for PanCard if excel column is empty
          if (this.arraylist[i].PanCard == undefined) {
            this.arraylist[i].PanCard = '';
          }
          else {
            this.arraylist[i].PanCard = this.arraylist[i].PanCard.toString();
          }
        
          var obj = {
            Customer_Name: this.arraylist[i].Customer_Name,
            Father_Name: this.arraylist[i].Father_Name,
            BirthDate: Dob,
            Local_Address: this.arraylist[i].Local_Address,
            Permanent_Address: this.arraylist[i].Permanent_Address,
            Local_PinCode: this.arraylist[i].Local_PinCode,
            Permanent_PinCode: this.arraylist[i].Permanent_PinCode,
            Gender: this.arraylist[i].Gender,
            VotingCardNo: this.arraylist[i].VotingCardNo,
            AdharCardNo: this.arraylist[i].AdharCardNo,
            PanCard: this.arraylist[i].PanCard,
            MobileNo: this.arraylist[i].MobileNo,
            AltMobile_Number: this.arraylist[i].AltMobile_Number,
            Profession: this.arraylist[i].Profession,
            Email: this.arraylist[i].Email,
            City: this.arraylist[i].City,
            State: this.arraylist[i].State,
            Operator: this.arraylist[i].Operator,
            Category: this.arraylist[i].Category,
            IsActive: this.arraylist[i].IsActive,
            CustomerDate: this.arraylist[i].CustomerDate,
            Source_of_Data: this.arraylist[i].Source_of_Data,
          }
          console.log(this.excelUploadedData);
          this.excelUploadedData.push(obj);
       
        }
      }
      //this.excelUploadedData = arraylist;
    };
    reader.onerror = function (event) { console.error("File could not be read! Code "); };
  }

  upload(){
    debugger;
    this.dataSearch.bulkUpload(this.excelUploadedData).subscribe(data=>{
      if(data){
        this.router.navigate(['/search']);
        alert("File uploaded succesfully!")
      }
      else{

      }
    },(err)=>{

    })
  }

}
